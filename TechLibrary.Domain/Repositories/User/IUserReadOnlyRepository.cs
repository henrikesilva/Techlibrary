namespace TechLibrary.Domain.Repositories.User;
public interface IUserReadOnlyRepository
{
    public Task<Entities.User?> GetUserById(Guid id);
    public Task<Entities.User?> GetUserByEmail(string email);
    public Task<bool> ExistsUserWithEmail(string email);
}
