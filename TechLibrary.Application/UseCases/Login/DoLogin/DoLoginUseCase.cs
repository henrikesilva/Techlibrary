using TechLibrary.Communication.Requests;
using TechLibrary.Communication.Responses;
using TechLibrary.Domain.Repositories.User;
using TechLibrary.Domain.Security.Cryptography;
using TechLibrary.Domain.Security.Tokens.Access;
using TechLibrary.Exception;

namespace TechLibrary.Application.UseCases.Login.DoLogin;
public class DoLoginUseCase : IDoLoginUseCase
{
    private readonly IUserReadOnlyRepository _repository;
    private readonly IBCryptAlgorithm _bCryptAlgorithm;
    private readonly IJwtTokenGenerator _jwtTokenGenerator;

    public DoLoginUseCase(IUserReadOnlyRepository repository,
        IBCryptAlgorithm bCryptAlgorithm,
        IJwtTokenGenerator jwtTokenGenerator)
    {
        _repository = repository;
        _bCryptAlgorithm = bCryptAlgorithm;
        _jwtTokenGenerator = jwtTokenGenerator;
    }

    public async Task<ResponseRegisteredUserJson> Execute(RequestLoginJson request)
    {
        var user = await _repository.GetUserByEmail(request.Email);
        if (user is null)
            throw new InvalidLoginException();

        var passwordIsValid = _bCryptAlgorithm.Verify(request.Password, user);
        if (passwordIsValid is false)
            throw new InvalidLoginException();

        return new ResponseRegisteredUserJson
        {
            Name = user.Name,
            AccessToken = _jwtTokenGenerator.GenerateToken(user)
        };
    }
}
