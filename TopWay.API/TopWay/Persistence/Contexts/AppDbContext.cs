using Microsoft.EntityFrameworkCore;
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
        
        // Apply Naming Conventions
        builder.UseSnakeCaseNamingConvention();
        
    }
}