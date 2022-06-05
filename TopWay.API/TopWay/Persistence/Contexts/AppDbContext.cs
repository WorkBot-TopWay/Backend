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
    public DbSet<Category> Categories { get; set; }
    public DbSet<CategoryGym> CategoriesGyms { get; set; }
    public DbSet<Images> Images { get; set; }
    public DbSet<CompetitionGym> CompetitionGyms { get; set; }
    public DbSet<Comment> Comments { get; set; }
    public DbSet<CompetitionReservationClimber> CompetitionReservationClimbers { get; set; }
    public DbSet<CompetitionGymRanking> CompetitionGymRankings { get; set; }
    public DbSet<League> Leagues { get; set; }
    public DbSet<Request> Requests { get; set; }
    public DbSet<ClimbersLeague> ClimbersLeagues { get; set; }
    public DbSet<CompetitionLeague> CompetitionLeagues { get; set; }

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
        
        builder.Entity<Scaler>()
            .HasMany(p=>p.Comments)
            .WithOne(p=>p.Scaler)
            .HasForeignKey(p=>p.ScalerId);
        
        builder.Entity<Scaler>()
            .HasMany(p=>p.CompetitionReservationClimbers)
            .WithOne(p=>p.Scaler)
            .HasForeignKey(p=>p.ScalerId);
        
        builder.Entity<Scaler>()
            .HasMany(p=>p.CompetitionGymRankings)
            .WithOne(p=>p.Scaler)
            .HasForeignKey(p=>p.ScalerId);
        
        builder.Entity<Scaler>()
            .HasMany(p=>p.Leagues)
            .WithOne(p=>p.Scaler)
            .HasForeignKey(p=>p.ScalerId);
        
        builder.Entity<Scaler>()
            .HasMany(p=>p.Requests)
            .WithOne(p=>p.Scaler)
            .HasForeignKey(p=>p.ScalerId);
        
        builder.Entity<Scaler>()
            .HasMany(p=>p.ClimbersLeagues)
            .WithOne(p=>p.Scaler)
            .HasForeignKey(p=>p.ScalerId);

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
        
        builder.Entity<ClimbingGym>()
            .HasMany(p => p.CategoryGyms)
            .WithOne(p => p.ClimbingGym)
            .HasForeignKey(p => p.ClimbingGymId);
        
        builder.Entity<ClimbingGym>()
            .HasMany(p => p.Images)
            .WithOne(p => p.ClimbingGym)
            .HasForeignKey(p => p.ClimbingGymId);

        builder.Entity<ClimbingGym>()
            .HasMany(p => p.CompetitionGyms)
            .WithOne(p => p.ClimbingGym)
            .HasForeignKey(p => p.ClimberGymId);
        
        builder.Entity<ClimbingGym>()
            .HasMany(p => p.Comments)
            .WithOne(p => p.ClimbingGym)
            .HasForeignKey(p => p.ClimbingGymId);
        
        builder.Entity<ClimbingGym>()
            .HasMany(p=>p.Leagues)
            .WithOne(p=>p.ClimbingGym)
            .HasForeignKey(p=>p.ClimbingGymId);
        
        builder.Entity<ClimbingGym>()
            .HasMany(p=>p.ClimbersLeagues)
            .WithOne(p=>p.ClimbingGym)
            .HasForeignKey(p=>p.ClimbingGymId);

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
        
        // Category entity
        builder.Entity<Category>().ToTable("Categories");
        builder.Entity<Category>().HasKey(p => p.Id);
        builder.Entity<Category>().Property(p => p.Id).IsRequired().ValueGeneratedOnAdd();
        builder.Entity<Category>().Property(p => p.Name).IsRequired().HasMaxLength(50);
        
        // relationships
        
        builder.Entity<Category>()
            .HasMany(p => p.CategoryGym)
            .WithOne(p => p.Category)
            .HasForeignKey(p => p.CategoryId);
        
        // CategoryGym entity
        builder.Entity<CategoryGym>().ToTable("CategoryGym");
        builder.Entity<CategoryGym>().HasKey(p => p.Id);
        builder.Entity<CategoryGym>().Property(p => p.Id).IsRequired().ValueGeneratedOnAdd();
        builder.Entity<CategoryGym>().Property(p => p.CategoryId).IsRequired();
        builder.Entity<CategoryGym>().Property(p => p.ClimbingGymId).IsRequired();
        
        
        // relationships
        
        builder.Entity<CategoryGym>()
            .HasOne(p => p.ClimbingGym)
            .WithMany(p => p.CategoryGyms)
            .HasForeignKey(p => p.ClimbingGymId);
        
        builder.Entity<CategoryGym>()
            .HasOne(p => p.Category)
            .WithMany(p => p.CategoryGym)
            .HasForeignKey(p => p.CategoryId);
        
        // Images entity
        builder.Entity<Images>().ToTable("Images");
        builder.Entity<Images>().HasKey(p => p.Id);
        builder.Entity<Images>().Property(p => p.Id).IsRequired().ValueGeneratedOnAdd();
        builder.Entity<Images>().Property(p => p.ImageUrl).IsRequired().HasMaxLength(500);
        builder.Entity<Images>().Property(p => p.ClimbingGymId).IsRequired();
        builder.Entity<Images>().Property(p => p.Alt).IsRequired().HasMaxLength(100);
        
        // relationships
        
        builder.Entity<Images>()
            .HasOne(p => p.ClimbingGym)
            .WithMany(p => p.Images)
            .HasForeignKey(p => p.ClimbingGymId);

        // CompetitionGym entity
        builder.Entity<CompetitionGym>().ToTable("CompetitionGym");
        builder.Entity<CompetitionGym>().HasKey(p => p.Id);
        builder.Entity<CompetitionGym>().Property(p => p.Id).IsRequired().ValueGeneratedOnAdd();
        builder.Entity<CompetitionGym>().Property(p => p.Name).IsRequired().HasMaxLength(50);
        builder.Entity<CompetitionGym>().Property(p => p.Price).IsRequired();
        builder.Entity<CompetitionGym>().Property(p => p.Date).IsRequired();
        builder.Entity<CompetitionGym>().Property(p => p.ClimberGymId).IsRequired();
        builder.Entity<CompetitionGym>().Property(p => p.type).IsRequired().HasMaxLength(50);

        // relationships

        builder.Entity<CompetitionGym>()
            .HasOne(p => p.ClimbingGym)
            .WithMany(p => p.CompetitionGyms)
            .HasForeignKey(p => p.ClimberGymId);
        
        builder.Entity<CompetitionGym>()
            .HasMany(p => p.CompetitionGymRankings)
            .WithOne(p => p.CompetitionGym)
            .HasForeignKey(p => p.CompetitionGymId);

        // Comments entity
        builder.Entity<Comment>().ToTable("Comment");
        builder.Entity<Comment>().HasKey(p => p.Id);
        builder.Entity<Comment>().Property(p => p.Id).IsRequired().ValueGeneratedOnAdd();
        builder.Entity<Comment>().Property(p => p.Description).IsRequired().HasMaxLength(250);
        builder.Entity<Comment>().Property(p => p.ClimbingGymId).IsRequired();
        builder.Entity<Comment>().Property(p => p.ScalerId).IsRequired();
        builder.Entity<Comment>().Property(p => p.Date).IsRequired();
        builder.Entity<Comment>().Property(p=>p.Score).IsRequired();
        
        // relationships
        
        builder.Entity<Comment>()
            .HasOne(p => p.ClimbingGym)
            .WithMany(p => p.Comments)
            .HasForeignKey(p => p.ClimbingGymId);
        
        builder.Entity<Comment>()
            .HasOne(p=>p.Scaler)
            .WithMany(p=>p.Comments)
            .HasForeignKey(p=>p.ScalerId);
        
        // CompetitionReservationClimber entity
        builder.Entity<CompetitionReservationClimber>().ToTable("CReservationClimber");
        builder.Entity<CompetitionReservationClimber>().HasKey(p => p.Id);
        builder.Entity<CompetitionReservationClimber>().Property(p => p.Id).IsRequired().ValueGeneratedOnAdd();
        builder.Entity<CompetitionReservationClimber>().Property(p => p.CompetitionGymId).IsRequired();
        builder.Entity<CompetitionReservationClimber>().Property(p => p.ScalerId).IsRequired();
        builder.Entity<CompetitionReservationClimber>().Property(p => p.Status).IsRequired().HasMaxLength(50);
        
        // relationships
        
        builder.Entity<CompetitionReservationClimber>()
            .HasOne(p => p.Scaler)
            .WithMany(p => p.CompetitionReservationClimbers)
            .HasForeignKey(p => p.ScalerId);
        
        builder.Entity<CompetitionReservationClimber>()
            .HasOne(p => p.CompetitionGym)
            .WithMany(p => p.CompetitionReservationClimbers)
            .HasForeignKey(p => p.CompetitionGymId);
        
        // CompetitionGymRankings entity
        builder.Entity<CompetitionGymRanking>().ToTable("CompetitionGymRankings");
        builder.Entity<CompetitionGymRanking>().HasKey(p => p.Id);
        builder.Entity<CompetitionGymRanking>().Property(p => p.Id).IsRequired().ValueGeneratedOnAdd();
        builder.Entity<CompetitionGymRanking>().Property(p => p.CompetitionGymId).IsRequired();
        builder.Entity<CompetitionGymRanking>().Property(p => p.ScalerId).IsRequired();
        builder.Entity<CompetitionGymRanking>().Property(p => p.Score).IsRequired();
        
        // relationships
        
        builder.Entity<CompetitionGymRanking>()
            .HasOne(p => p.Scaler)
            .WithMany(p => p.CompetitionGymRankings)
            .HasForeignKey(p => p.ScalerId);
        
        builder.Entity<CompetitionGymRanking>()
            .HasOne(p=>p.CompetitionGym)
            .WithMany(p=>p.CompetitionGymRankings)
            .HasForeignKey(p=>p.CompetitionGymId);
        
        // League entity
        builder.Entity<League>().ToTable("League");
        builder.Entity<League>().HasKey(p => p.Id);
        builder.Entity<League>().Property(p => p.Id).IsRequired().ValueGeneratedOnAdd();
        builder.Entity<League>().Property(p => p.Name).IsRequired().HasMaxLength(100);
        builder.Entity<League>().Property(p => p.Description).IsRequired().HasMaxLength(250);
        builder.Entity<League>().Property(p => p.UrlLogo).IsRequired().HasMaxLength(500);
        builder.Entity<League>().Property(p => p.AdminName).IsRequired().HasMaxLength(100);
        builder.Entity<League>().Property(p => p.NumberParticipants).IsRequired();
        builder.Entity<League>().Property(p => p.ScalerId).IsRequired();
        builder.Entity<League>().Property(p => p.ClimbingGymId).IsRequired();
        
        // relationships
        
        builder.Entity<League>()
            .HasOne(p => p.Scaler)
            .WithMany(p => p.Leagues)
            .HasForeignKey(p => p.ScalerId);
        
        builder.Entity<League>()
            .HasOne(p=>p.ClimbingGym)
            .WithMany(p=>p.Leagues)
            .HasForeignKey(p=>p.ClimbingGymId);
        
        builder.Entity<League>()
            .HasMany(p => p.Requests)
            .WithOne(p => p.League)
            .HasForeignKey(p => p.LeagueId);
        
        builder.Entity<League>()
            .HasMany(p => p.ClimbersLeagues)
            .WithOne(p => p.League)
            .HasForeignKey(p => p.LeagueId);
        
        builder.Entity<League>()
            .HasMany(p => p.CompetitionLeagues)
            .WithOne(p => p.League)
            .HasForeignKey(p => p.LeagueId);

        // Request entity
        builder.Entity<Request>().ToTable("Request");
        builder.Entity<Request>().HasKey(p => p.Id);
        builder.Entity<Request>().Property(p => p.Id).IsRequired().ValueGeneratedOnAdd();
        builder.Entity<Request>().Property(p => p.LeagueId).IsRequired();
        builder.Entity<Request>().Property(p => p.ScalerId).IsRequired();
        builder.Entity<Request>().Property(p => p.Status).IsRequired().HasMaxLength(50);
        
        
        // relationships
        
        builder.Entity<Request>()
            .HasOne(p => p.Scaler)
            .WithMany(p => p.Requests)
            .HasForeignKey(p => p.ScalerId);
        
        builder.Entity<Request>()
            .HasOne(p=>p.League)
            .WithMany(p=>p.Requests)
            .HasForeignKey(p=>p.LeagueId);
        
        // ClimbersLeagues entity
        
        builder.Entity<ClimbersLeague>().ToTable("ClimbersLeagues");
        builder.Entity<ClimbersLeague>().HasKey(p => p.Id);
        builder.Entity<ClimbersLeague>().Property(p => p.Id).IsRequired().ValueGeneratedOnAdd();
        builder.Entity<ClimbersLeague>().Property(p => p.LeagueId).IsRequired();
        builder.Entity<ClimbersLeague>().Property(p => p.ScalerId).IsRequired();
        builder.Entity<ClimbersLeague>().Property(p => p.ClimbingGymId).IsRequired();
        
        // relationships
        
        builder.Entity<ClimbersLeague>()
            .HasOne(p => p.Scaler)
            .WithMany(p => p.ClimbersLeagues)
            .HasForeignKey(p => p.ScalerId);
        
        builder.Entity<ClimbersLeague>()
            .HasOne(p=>p.League)
            .WithMany(p=>p.ClimbersLeagues)
            .HasForeignKey(p=>p.LeagueId);
        
        builder.Entity<ClimbersLeague>()
            .HasOne(p=>p.ClimbingGym)
            .WithMany(p=>p.ClimbersLeagues)
            .HasForeignKey(p=>p.ClimbingGymId);
        
        // CompetitionLeagues entity
        builder.Entity<CompetitionLeague>().ToTable("CompetitionLeagues");
        builder.Entity<CompetitionLeague>().HasKey(p => p.Id);
        builder.Entity<CompetitionLeague>().Property(p => p.Id).IsRequired().ValueGeneratedOnAdd();
        builder.Entity<CompetitionLeague>().Property(p => p.LeagueId).IsRequired();
        builder.Entity<CompetitionLeague>().Property(p => p.Name).IsRequired().HasMaxLength(100);
        builder.Entity<CompetitionLeague>().Property(p => p.Date).IsRequired();
        builder.Entity<CompetitionLeague>().Property(p => p.type).IsRequired().HasMaxLength(50);
        
        // relationships
        
        builder.Entity<CompetitionLeague>()
            .HasOne(p => p.League)
            .WithMany(p => p.CompetitionLeagues)
            .HasForeignKey(p => p.LeagueId);

        // Apply Naming Conventions

        builder.UseSnakeCaseNamingConvention();
        
    }
}