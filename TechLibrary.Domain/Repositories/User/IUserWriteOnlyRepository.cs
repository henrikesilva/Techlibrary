namespace TechLibrary.Domain.Repositories.User;
public interface IUserWriteOnlyRepository
{
    Task Register(Entities.User user);
}
