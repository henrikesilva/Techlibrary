using TechLibrary.Domain.Entities;

namespace TechLibrary.Domain.Repositories.Books;
public interface IBookReadOnlyRepository
{
    Task<Tuple<List<Entities.Book>, Pagination>> GetFiltrededBooks(int pageNumber, string? title, int pageSize = 10);
    Task<Entities.Book?> GetById(Guid id);
}
