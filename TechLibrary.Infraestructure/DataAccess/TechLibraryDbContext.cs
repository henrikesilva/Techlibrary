using Microsoft.EntityFrameworkCore;
using TechLibrary.Domain.Entities;

namespace TechLibrary.Infraestructure.DataAccess;
public class TechLibraryDbContext : DbContext
{
    public TechLibraryDbContext(DbContextOptions options) : base(options) { }

    public DbSet<User> Users { get; set; }
    public DbSet<Book> Books { get; set; }
    public DbSet<Checkout> Checkouts { get; set; }
}
