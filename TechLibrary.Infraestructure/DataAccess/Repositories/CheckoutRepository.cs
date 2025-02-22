using Microsoft.EntityFrameworkCore;
using TechLibrary.Domain.Entities;
using TechLibrary.Domain.Repositories.Checkouts;

namespace TechLibrary.Infraestructure.DataAccess.Repositories;
public class CheckoutRepository : ICheckoutReadOnlyRepository, ICheckoutWriteOnlyRepository
{
    private readonly TechLibraryDbContext _context;

    public CheckoutRepository(TechLibraryDbContext context)
    {
        _context = context;
    }

    public async Task<int> AmountBooksReserved(Guid bookId)
    {
        return await _context.Checkouts.CountAsync(checkout => checkout.BookId == bookId &&
                                                    checkout.ReturnedDate == null);
    }

    public async Task Register(Checkout checkout)
    {
        await _context.Checkouts.AddAsync(checkout);
    }
}
