using TechLibrary.Communication.Requests;
using TechLibrary.Communication.Responses;

namespace TechLibrary.Application.UseCases.Users.Register;
public interface IRegisterUserUseCase
{
    Task<ResponseRegisteredUserJson> Execute(RequestUserJson request);
}
