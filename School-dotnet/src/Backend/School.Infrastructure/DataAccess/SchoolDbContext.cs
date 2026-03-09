using Microsoft.EntityFrameworkCore;
using School.Domain.Entities;

namespace School.Infrastructure.DataAccess;

public class SchoolDbContext : DbContext
{
    public SchoolDbContext(DbContextOptions options) : base(options)
    {
    }

    public DbSet<User> Users { get; set; }
    public DbSet<Academy> Academys { get; set; }
    public DbSet<Student> Students { get; set; }
    public DbSet<RefreshToken> RefreshTokens { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(SchoolDbContext).Assembly);
    }
}