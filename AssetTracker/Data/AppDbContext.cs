using AssetTracker.Models;
using Microsoft.EntityFrameworkCore;

namespace AssetTracker.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    public DbSet<Employee> Employees { get; set; }
    public DbSet<FixedAsset> FixedAssets { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Employee>()
            .HasMany(a => a.FixedAssets)
            .WithOne(e => e.Employee)
            .OnDelete(DeleteBehavior.SetNull);

        modelBuilder.Entity<FixedAsset>()
            .HasIndex(fa => fa.SerialNumber)
            .IsUnique();

        modelBuilder.Entity<FixedAsset>()
            .HasIndex(fa => fa.AssetCode)
            .IsUnique();
    }
}
