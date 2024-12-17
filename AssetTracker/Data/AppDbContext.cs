using AssetTracker.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace AssetTracker.Data;

public class AppDbContext : IdentityDbContext<AppUser>
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    public DbSet<Employee> Employees { get; set; }
    public DbSet<FixedAsset> FixedAssets { get; set; }
    public DbSet<AssetHistory> AssetHistories { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Employee>()
            .HasMany(a => a.FixedAssets)
            .WithOne(e => e.Employee)
            .OnDelete(DeleteBehavior.SetNull);

        modelBuilder.Entity<Employee>()
            .HasIndex(e => e.Email)
            .IsUnique();

        modelBuilder.Entity<FixedAsset>()
            .HasIndex(fa => fa.SerialNumber)
            .IsUnique();

        modelBuilder.Entity<FixedAsset>()
            .HasIndex(fa => fa.AssetCode)
            .IsUnique();

        modelBuilder.Entity<AssetHistory>()
            .HasOne(ah => ah.Asset)
            .WithMany(ah => ah.AssetHistories)
            .HasForeignKey(ah => ah.AssetId);

       
    }
}
