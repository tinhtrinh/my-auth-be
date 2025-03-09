using Domain.Users;
using Microsoft.EntityFrameworkCore;

namespace Persistence;

public class MyAuthDbContext : DbContext
{
    public MyAuthDbContext(DbContextOptions<MyAuthDbContext> options)
        : base(options)
    { }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer();
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(MyAuthDbContext).Assembly);
    }
}