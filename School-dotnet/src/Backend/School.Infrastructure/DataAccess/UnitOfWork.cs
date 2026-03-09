using School.Domain.Repositories;

namespace School.Infrastructure.DataAccess;

public class UnitOfWork : IUnitOfWork
{
    private readonly SchoolDbContext _dbContext;

    public UnitOfWork(SchoolDbContext dbContext) => _dbContext = dbContext;

    public async Task Commit() => await _dbContext.SaveChangesAsync();
}