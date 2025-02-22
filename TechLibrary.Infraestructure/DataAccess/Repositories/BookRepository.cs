using Microsoft.EntityFrameworkCore;
using TechLibrary.Domain.Entities;
using TechLibrary.Domain.Repositories.Books;

namespace TechLibrary.Infraestructure.DataAccess.Repositories;
public class BookRepository : IBookReadOnlyRepository, IBookWriteOnlyRepository
{
    private readonly TechLibraryDbContext _context;
    
    public BookRepository(TechLibraryDbContext context)
    {
        _context = context;
    }

    public async Task<Book?> GetById(Guid id)
    {
        return await _context.Books.FirstOrDefaultAsync(book => book.Id == id);
    }

    public async Task<Tuple<List<Book>, Pagination>> GetFiltrededBooks(int pageNumber, string? title, int pageSize = 10)
    {
        var query = _context.Books.AsQueryable();

        if (string.IsNullOrWhiteSpace(title) == false)
        {
            query = query.Where(book => book.Title.Contains(title));
        }

        var books = await query.OrderBy(book => book.Title)
            .ThenBy(book => book.Author)
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();

        var totalCount = 0;
        if (string.IsNullOrWhiteSpace(title))
            totalCount = await _context.Books.CountAsync();
        else
            totalCount = await _context.Books.Where(book => book.Title.Contains(title)).CountAsync();

        var pagination = new Pagination
        {
            PageNumber = pageNumber,
            TotalCount = totalCount
        };

        return new Tuple<List<Book>, Pagination>(books, pagination);
    }

    public async Task Register(Book book)
    {
        await _context.Books.AddAsync(book);
    }
}
