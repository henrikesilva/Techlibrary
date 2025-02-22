namespace TechLibrary.Domain.Repositories.Books;
public interface IBookWriteOnlyRepository
{
    Task Register(Entities.Book book);
}
