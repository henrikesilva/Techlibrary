using TechLibrary.Domain.Entities;

namespace TechLibrary.Domain.Services.LoggedUser;
public interface ILoggedUserServices
{
    Task<User> Get();
}
