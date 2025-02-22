using TechLibrary.Domain.Repositories;

namespace TechLibrary.Infraestructure.DataAccess.Repositories;
public class UnitOfWork : IUnitOfWork
{
    private readonly TechLibraryDbContext _context;

    public UnitOfWork(TechLibraryDbContext context)
    {
        _context = context;
    }

    public async Task Commit()
    {
        await _context.SaveChangesAsync();
    }
}
