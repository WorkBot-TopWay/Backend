using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using TopWay.API.Shared.Extensions;
using TopWay.API.TopWay.Domain.Models;

namespace TopWay.API.TopWay.Persistence.Contexts;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions options) : base(options)
    {
    }
    // Declare DbSet of the entity
    
    public DbSet<Scaler> Scalers { get; set; }
    public DbSet<ClimbingGym> ClimbingGyms { get; set; }
    public DbSet<Notification> Notifications { get; set; }

    // Structure of the database
    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        // Scaler entity
        builder.Entity<Scaler>().ToTable("Scalers");
        builder.Entity<Scaler>().HasKey(p => p.Id);
        builder.Entity<Scaler>().Property(p => p.Id).IsRequired().ValueGeneratedOnAdd();
        builder.Entity<Scaler>().Property(p => p.FirstName).IsRequired().HasMaxLength(50);
        builder.Entity<Scaler>().Property(p => p.LastName).IsRequired().HasMaxLength(50);
        builder.Entity<Scaler>().Property(p => p.Email).IsRequired().HasMaxLength(150);
        builder.Entity<Scaler>().Property(p => p.Password).IsRequired().HasMaxLength(150);
        builder.Entity<Scaler>().Property(p => p.Phone).IsRequired().HasMaxLength(20);
        builder.Entity<Scaler>().Property(p => p.Address).IsRequired().HasMaxLength(250);
        builder.Entity<Scaler>().Property(p => p.City).IsRequired().HasMaxLength(50);
        builder.Entity<Scaler>().Property(p => p.District).IsRequired().HasMaxLength(50);
        builder.Entity<Scaler>().Property(p => p.UrlPhoto).IsRequired().HasMaxLength(1000);
        builder.Entity<Scaler>().Property(p => p.Type).IsRequired().HasMaxLength(50);
        
        // relationships
        
        builder.Entity<Scaler>()
            .HasMany(p => p.Notifications)
            .WithOne(p => p.Scaler)
            .HasForeignKey(p => p.ScalerId);
        
        // ClimbingGyms entity
        
        builder.Entity<ClimbingGym>().ToTable("ClimbingGyms");
        builder.Entity<ClimbingGym>().HasKey(p => p.Id);
        builder.Entity<ClimbingGym>().Property(p => p.Id).IsRequired().ValueGeneratedOnAdd();
        builder.Entity<ClimbingGym>().Property(p => p.Name).IsRequired().HasMaxLength(50);
        builder.Entity<ClimbingGym>().Property(p => p.Password).IsRequired().HasMaxLength(150);
        builder.Entity<ClimbingGym>().Property(p => p.Email).IsRequired().HasMaxLength(150);
        builder.Entity<ClimbingGym>().Property(p => p.City).IsRequired().HasMaxLength(50);
        builder.Entity<ClimbingGym>().Property(p => p.District).IsRequired().HasMaxLength(50);
        builder.Entity<ClimbingGym>().Property(p => p.Address).IsRequired().HasMaxLength(250);
        builder.Entity<ClimbingGym>().Property(p => p.Phone).IsRequired().HasMaxLength(20);
        builder.Entity<ClimbingGym>().Property(p => p.LogoUrl).IsRequired().HasMaxLength(1000);
        builder.Entity<ClimbingGym>().Property(p => p.type).HasMaxLength(50);
        
        // relationships
        
        
        // Notifications entity
        builder.Entity<Notification>().ToTable("Notifications");
        builder.Entity<Notification>().HasKey(p => p.Id);
        builder.Entity<Notification>().Property(p => p.Id).IsRequired().ValueGeneratedOnAdd();
        builder.Entity<Notification>().Property(p => p.Message).IsRequired().HasMaxLength(250);
        builder.Entity<Notification>().Property(p => p.ScalerId).HasMaxLength(250);
        
        // relationships
        builder.Entity<Notification>()
            .HasOne(p => p.Scaler)
            .WithMany(p => p.Notifications)
            .HasForeignKey(p => p.ScalerId);
        
        // Apply Naming Conventions
        
        builder.UseSnakeCaseNamingConvention();
        
    }
}