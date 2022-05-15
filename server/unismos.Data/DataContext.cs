using Microsoft.EntityFrameworkCore;
using unismos.Common.Entities;

namespace unismos.Data;

public class DataContext : DbContext
{
    public DataContext(DbContextOptions<DataContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>()
            .ToTable("Users")
            .HasDiscriminator<int>("UserType")
            .HasValue<Student>(1)
            .HasValue<Professor>(2)
            .HasValue<Secretary>(3);
        modelBuilder.Entity<Teaching>().Navigation(e => e.Subject).AutoInclude();
        modelBuilder.Entity<Teaching>().Navigation(e => e.Professor).AutoInclude();
        modelBuilder.Entity<Enrollment>().Navigation(e => e.Student).AutoInclude();
        modelBuilder.Entity<Student>().Navigation(e => e.TaxesPaid).AutoInclude();
        // modelBuilder.Entity<Tax>().Navigation(e => e.Payers).AutoInclude();
        // modelBuilder.Entity<Professor>().Navigation(e => e.Teachings).AutoInclude();
    }
    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        return (await base.SaveChangesAsync(true, cancellationToken));
    }
    public DbSet<User> Users { get; set; } 
    public DbSet<Subject> Subjects { get; set; }
    public DbSet<Teaching> Teachings { get; set; }
    public DbSet<Tax> Taxes { get; set; }
    public DbSet<Enrollment> Enrollments { get; set; }
    
}