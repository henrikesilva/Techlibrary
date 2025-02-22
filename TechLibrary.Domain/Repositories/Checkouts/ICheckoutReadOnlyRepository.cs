namespace TechLibrary.Domain.Repositories.Checkouts;
public interface ICheckoutReadOnlyRepository
{
    Task<int> AmountBooksReserved(Guid bookId);
}
