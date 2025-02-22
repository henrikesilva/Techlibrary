using Microsoft.EntityFrameworkCore;
using TechLibrary.Domain.Entities;
using TechLibrary.Domain.Repositories.User;

namespace TechLibrary.Infraestructure.DataAccess.Repositories;
public class UserRepository : IUserWriteOnlyRepository, IUserReadOnlyRepository
{
    private readonly TechLibraryDbContext _context;
    public UserRepository(TechLibraryDbContext context)
    {
        _context = context;
    }

    public async Task<bool> ExistsUserWithEmail(string email)
    {
        return await _context.Users.AnyAsync(user => user.Email.Equals(email));
    }

    public async Task<User?> GetUserByEmail(string email)
    {
        return await _context.Users.AsNoTracking().FirstOrDefaultAsync(user => user.Email.Equals(email));
    }

    public async Task<User?> GetUserById(Guid id)
    {
        return await _context.Users.AsNoTracking().FirstOrDefaultAsync(user => user.Id.Equals(id));
    }

    public async Task Register(User user)
    {
        await _context.Users.AddAsync(user);
    }
}
