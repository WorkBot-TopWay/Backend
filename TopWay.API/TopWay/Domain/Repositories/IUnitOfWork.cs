namespace TopWay.API.TopWay.Domain.Repositories;

public interface IUnitOfWork
{
    Task CompleteAsync();
}