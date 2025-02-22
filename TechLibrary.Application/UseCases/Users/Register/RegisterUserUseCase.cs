using AutoMapper;
using FluentValidation.Results;
using TechLibrary.Api.UseCases.Users.Register;
using TechLibrary.Communication.Requests;
using TechLibrary.Communication.Responses;
using TechLibrary.Domain.Repositories;
using TechLibrary.Domain.Repositories.User;
using TechLibrary.Domain.Security.Cryptography;
using TechLibrary.Domain.Security.Tokens.Access;
using TechLibrary.Exception;

namespace TechLibrary.Application.UseCases.Users.Register;
public class RegisterUserUseCase : IRegisterUserUseCase
{
    private readonly IUserWriteOnlyRepository _userWriteOnlyRepository;
    private readonly IUserReadOnlyRepository _userReadOnlyRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IBCryptAlgorithm _bCryptAlgorithm;
    private readonly IJwtTokenGenerator _jwtTokenGenerator;
    private readonly IMapper _mapper;

    public RegisterUserUseCase(IUserWriteOnlyRepository userWriteOnlyRepository, 
        IUserReadOnlyRepository userReadOnlyRepository,
        IUnitOfWork unitOfWork,
        IBCryptAlgorithm bCryptAlgorithm,
        IJwtTokenGenerator jwtTokenGenerator,
        IMapper mapper)
    {
        _userWriteOnlyRepository = userWriteOnlyRepository;
        _userReadOnlyRepository = userReadOnlyRepository;
        _unitOfWork = unitOfWork;
        _bCryptAlgorithm = bCryptAlgorithm;
        _jwtTokenGenerator = jwtTokenGenerator;
        _mapper = mapper;
    }

    public async Task<ResponseRegisteredUserJson> Execute(RequestUserJson request)
    {
        Validate(request);

        request.Password = _bCryptAlgorithm.HashPassword(request.Password);

        var user = _mapper.Map<Domain.Entities.User>(request);
        
        await _userWriteOnlyRepository.Register(user);
        await _unitOfWork.Commit();

        return new ResponseRegisteredUserJson
        {
            Name = user.Name,
            AccessToken = _jwtTokenGenerator.GenerateToken(user)
        };
    }

    private async void Validate(RequestUserJson request)
    {
        var validator = new RegisterUserValidator();

        var result = validator.Validate(request);

        var existsUserWithEmail = await _userReadOnlyRepository.ExistsUserWithEmail(request.Email);

        if (existsUserWithEmail)
            result.Errors.Add(new ValidationFailure("Email", "O email já registrado na plataforma."));

        if (result.IsValid is false)
        {
            var errorMesages = result.Errors.Select(e => e.ErrorMessage).ToList();

            throw new ErrorOnValidationException(errorMesages);
        }
    }
}
