using Microsoft.EntityFrameworkCore;
using unismos.Common.Entities;

namespace unismos.Data;

public class DataContext : DbContext
{
    public DataContext(DbContextOptions<DataContext> options) : base(options)
    {
    }

    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        return (await base.SaveChangesAsync(true, cancellationToken));
    }
    public DbSet<User> Users { get; set; } 
}