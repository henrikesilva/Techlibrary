namespace TechLibrary.Domain.Repositories.Checkouts;
public interface ICheckoutWriteOnlyRepository
{
    public Task Register(Entities.Checkout checkout);
}
