namespace TechLibrary.Domain.Repositories;
public interface IUnitOfWork
{
    Task Commit();
}
