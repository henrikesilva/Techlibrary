using TechLibrary.Domain.Repositories;
using TechLibrary.Domain.Repositories.Books;
using TechLibrary.Domain.Repositories.Checkouts;
using TechLibrary.Domain.Services.LoggedUser;
using TechLibrary.Exception;

namespace TechLibrary.Application.UseCases.Checkouts.Register;
public class RegisterBookCheckoutUseCase : IRegisterBookCheckoutUseCase
{
    private const int MAX_LOAN_DAYS = 7;

    private readonly ICheckoutWriteOnlyRepository _checkoutWriteOnlyRepository;
    private readonly ICheckoutReadOnlyRepository _checkoutReadOnlyRepository;
    private readonly IBookReadOnlyRepository _bookReadOnlyRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILoggedUserServices _loggedUser;
    
    public RegisterBookCheckoutUseCase(ICheckoutWriteOnlyRepository checkoutWriteOnlyRepository,
        ICheckoutReadOnlyRepository checkoutReadOnlyRepository,
        IBookReadOnlyRepository bookReadOnlyRepository,
        IUnitOfWork unitOfWork,
        ILoggedUserServices loggedUser)
    {
        _checkoutWriteOnlyRepository = checkoutWriteOnlyRepository;
        _checkoutReadOnlyRepository = checkoutReadOnlyRepository;
        _bookReadOnlyRepository = bookReadOnlyRepository;
        _unitOfWork = unitOfWork;
        _loggedUser = loggedUser;
    }

    public async Task Execute(Guid bookId)
    {
        await Validate(bookId);

        var user = await _loggedUser.Get();

        var entity = new Domain.Entities.Checkout
        {
            UserId = user.Id,
            BookId = bookId,
            ExpectedReturnDate = DateTime.UtcNow.AddDays(MAX_LOAN_DAYS)
        };

        await _checkoutWriteOnlyRepository.Register(entity);
        await _unitOfWork.Commit();
    }

    private async Task Validate(Guid bookId)
    {
        var book = await _bookReadOnlyRepository.GetById(bookId);

        if (book is null)
            throw new NotFoundException("Livro não encontrado.");

        var amountBooksNotReturned = await _checkoutReadOnlyRepository.AmountBooksReserved(bookId);
        if (amountBooksNotReturned == book.Amount)
            throw new ConflictException("Livro não está disponível para empréstimo.");
    }
}
