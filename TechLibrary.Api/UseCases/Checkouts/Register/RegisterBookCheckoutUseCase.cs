using TechLibrary.Api.Infraestructure.DataAccess;
using TechLibrary.Api.Services.LoggedUser;
using TechLibrary.Exception;

namespace TechLibrary.Api.UseCases.Checkouts.Register;

public class RegisterBookCheckoutUseCase
{
    private const int MAX_LOAN_DAYS = 7;

    private readonly LoggedUserService _loggedService;

    public RegisterBookCheckoutUseCase(LoggedUserService loggedUser)
    {
        _loggedService = loggedUser;
    }

    public void Execute(Guid bookId)
    {
        var dbcontext = new TechLibraryDbContext();

        Validate(dbcontext, bookId);

        var user = _loggedService.GetLoggedUser(dbcontext);

        var entity = dbcontext.Checkouts.Add(new Domain.Entities.Checkout
        {
            UserId = user.Id,
            BookId = bookId,
            ExpectedReturnDate = DateTime.UtcNow.AddDays(MAX_LOAN_DAYS)
        });

        dbcontext.SaveChanges();
    }

    private void Validate(TechLibraryDbContext dbContext, Guid bookId)
    {
        var book = dbContext.Books.FirstOrDefault(book => book.Id == bookId);
        if (book is null)
            throw new NotFoundException("Livro não encontrado.");

        var amountBooksNotReturned = dbContext.Checkouts
                                              .Count(checkout => checkout.BookId == bookId && 
                                                    checkout.ReturnedDate == null);

        if (amountBooksNotReturned == book.Amount)
            throw new ConflictException("Livro não está disponível para empréstimo.");
    }
}
