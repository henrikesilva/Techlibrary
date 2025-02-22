namespace TechLibrary.Application.UseCases.Checkouts.Register;
public interface IRegisterBookCheckoutUseCase
{
    public Task Execute(Guid bookId);
}
