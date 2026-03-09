namespace School.Domain.Repositories;

public interface IUnitOfWork
{
    public Task Commit();
}