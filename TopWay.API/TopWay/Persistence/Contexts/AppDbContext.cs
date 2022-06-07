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
    public DbSet<ClimbingGyms> ClimbingGyms { get; set; }
    public DbSet<Notification> Notifications { get; set; }
    public DbSet<Categories> Categories { get; set; }
    public DbSet<CategoryGyms> CategoriesGyms { get; set; }
    public DbSet<Images> Images { get; set; }
    public DbSet<CompetitionGyms> CompetitionGyms { get; set; }
    public DbSet<Comments> Comments { get; set; }
    public DbSet<CompetitionReservationClimber> CompetitionReservationClimbers { get; set; }
    public DbSet<CompetitionGymRankings> CompetitionGymRankings { get; set; }
    public DbSet<League> Leagues { get; set; }
    public DbSet<Request> Requests { get; set; }
    public DbSet<ClimberLeagues> ClimbersLeagues { get; set; }
    public DbSet<CompetitionLeague> CompetitionLeagues { get; set; }
    
    public DbSet<CompetitionLeagueRanking> CompetitionLeagueRankings { get; set; }
    
    public DbSet<Favorite> Favorites { get; set; }

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
        
        builder.Entity<Scaler>()
            .HasMany(p=>p.CompetitionLeagueRankings)
            .WithOne(p=>p.Scaler)
            .HasForeignKey(p=>p.ScalerId);

        // ClimbingGyms entity
        
        builder.Entity<ClimbingGyms>().ToTable("ClimbingGyms");
        builder.Entity<ClimbingGyms>().HasKey(p => p.Id);
        builder.Entity<ClimbingGyms>().Property(p => p.Id).IsRequired().ValueGeneratedOnAdd();
        builder.Entity<ClimbingGyms>().Property(p => p.Name).IsRequired().HasMaxLength(50);
        builder.Entity<ClimbingGyms>().Property(p => p.Password).IsRequired().HasMaxLength(150);
        builder.Entity<ClimbingGyms>().Property(p => p.Email).IsRequired().HasMaxLength(150);
        builder.Entity<ClimbingGyms>().Property(p => p.City).IsRequired().HasMaxLength(50);
        builder.Entity<ClimbingGyms>().Property(p => p.District).IsRequired().HasMaxLength(50);
        builder.Entity<ClimbingGyms>().Property(p => p.Address).IsRequired().HasMaxLength(250);
        builder.Entity<ClimbingGyms>().Property(p => p.Phone).IsRequired().HasMaxLength(20);
        builder.Entity<ClimbingGyms>().Property(p => p.LogoUrl).IsRequired().HasMaxLength(1000);
        builder.Entity<ClimbingGyms>().Property(p => p.type).HasMaxLength(50);
        
        // relationships
        
        builder.Entity<ClimbingGyms>()
            .HasMany(p => p.CategoryGyms)
            .WithOne(p => p.ClimbingGyms)
            .HasForeignKey(p => p.ClimbingGymId);
        
        builder.Entity<ClimbingGyms>()
            .HasMany(p => p.Images)
            .WithOne(p => p.ClimbingGyms)
            .HasForeignKey(p => p.ClimbingGymId);

        builder.Entity<ClimbingGyms>()
            .HasMany(p => p.CompetitionGyms)
            .WithOne(p => p.ClimbingGyms)
            .HasForeignKey(p => p.ClimberGymId);
        
        builder.Entity<ClimbingGyms>()
            .HasMany(p => p.Comments)
            .WithOne(p => p.ClimbingGyms)
            .HasForeignKey(p => p.ClimbingGymId);
        
        builder.Entity<ClimbingGyms>()
            .HasMany(p=>p.Leagues)
            .WithOne(p=>p.ClimbingGyms)
            .HasForeignKey(p=>p.ClimbingGymId);
        
        builder.Entity<ClimbingGyms>()
            .HasMany(p=>p.ClimbersLeagues)
            .WithOne(p=>p.ClimbingGyms)
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
        builder.Entity<Categories>().ToTable("Categories");
        builder.Entity<Categories>().HasKey(p => p.Id);
        builder.Entity<Categories>().Property(p => p.Id).IsRequired().ValueGeneratedOnAdd();
        builder.Entity<Categories>().Property(p => p.Name).IsRequired().HasMaxLength(50);
        
        // relationships
        
        builder.Entity<Categories>()
            .HasMany(p => p.CategoryGym)
            .WithOne(p => p.Categories)
            .HasForeignKey(p => p.CategoryId);
        
        // CategoryGym entity
        builder.Entity<CategoryGyms>().ToTable("CategoryGym");
        builder.Entity<CategoryGyms>().HasKey(p => p.Id);
        builder.Entity<CategoryGyms>().Property(p => p.Id).IsRequired().ValueGeneratedOnAdd();
        builder.Entity<CategoryGyms>().Property(p => p.CategoryId).IsRequired();
        builder.Entity<CategoryGyms>().Property(p => p.ClimbingGymId).IsRequired();
        
        
        // relationships
        
        builder.Entity<CategoryGyms>()
            .HasOne(p => p.ClimbingGyms)
            .WithMany(p => p.CategoryGyms)
            .HasForeignKey(p => p.ClimbingGymId);
        
        builder.Entity<CategoryGyms>()
            .HasOne(p => p.Categories)
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
            .HasOne(p => p.ClimbingGyms)
            .WithMany(p => p.Images)
            .HasForeignKey(p => p.ClimbingGymId);

        // CompetitionGym entity
        builder.Entity<CompetitionGyms>().ToTable("CompetitionGym");
        builder.Entity<CompetitionGyms>().HasKey(p => p.Id);
        builder.Entity<CompetitionGyms>().Property(p => p.Id).IsRequired().ValueGeneratedOnAdd();
        builder.Entity<CompetitionGyms>().Property(p => p.Name).IsRequired().HasMaxLength(50);
        builder.Entity<CompetitionGyms>().Property(p => p.Price).IsRequired();
        builder.Entity<CompetitionGyms>().Property(p => p.Date).IsRequired();
        builder.Entity<CompetitionGyms>().Property(p => p.ClimberGymId).IsRequired();
        builder.Entity<CompetitionGyms>().Property(p => p.type).IsRequired().HasMaxLength(50);

        // relationships

        builder.Entity<CompetitionGyms>()
            .HasOne(p => p.ClimbingGyms)
            .WithMany(p => p.CompetitionGyms)
            .HasForeignKey(p => p.ClimberGymId);
        
        builder.Entity<CompetitionGyms>()
            .HasMany(p => p.CompetitionGymRankings)
            .WithOne(p => p.CompetitionGyms)
            .HasForeignKey(p => p.CompetitionGymId);

        // Comments entity
        builder.Entity<Comments>().ToTable("Comment");
        builder.Entity<Comments>().HasKey(p => p.Id);
        builder.Entity<Comments>().Property(p => p.Id).IsRequired().ValueGeneratedOnAdd();
        builder.Entity<Comments>().Property(p => p.Description).IsRequired().HasMaxLength(250);
        builder.Entity<Comments>().Property(p => p.ClimbingGymId).IsRequired();
        builder.Entity<Comments>().Property(p => p.ScalerId).IsRequired();
        builder.Entity<Comments>().Property(p => p.Date).IsRequired();
        builder.Entity<Comments>().Property(p=>p.Score).IsRequired();
        
        // relationships
        
        builder.Entity<Comments>()
            .HasOne(p => p.ClimbingGyms)
            .WithMany(p => p.Comments)
            .HasForeignKey(p => p.ClimbingGymId);
        
        builder.Entity<Comments>()
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
            .HasOne(p => p.CompetitionGyms)
            .WithMany(p => p.CompetitionReservationClimbers)
            .HasForeignKey(p => p.CompetitionGymId);
        
        // CompetitionGymRankings entity
        builder.Entity<CompetitionGymRankings>().ToTable("CompetitionGymRankings");
        builder.Entity<CompetitionGymRankings>().HasKey(p => p.Id);
        builder.Entity<CompetitionGymRankings>().Property(p => p.Id).IsRequired().ValueGeneratedOnAdd();
        builder.Entity<CompetitionGymRankings>().Property(p => p.CompetitionGymId).IsRequired();
        builder.Entity<CompetitionGymRankings>().Property(p => p.ScalerId).IsRequired();
        builder.Entity<CompetitionGymRankings>().Property(p => p.Score).IsRequired();
        
        // relationships
        
        builder.Entity<CompetitionGymRankings>()
            .HasOne(p => p.Scaler)
            .WithMany(p => p.CompetitionGymRankings)
            .HasForeignKey(p => p.ScalerId);
        
        builder.Entity<CompetitionGymRankings>()
            .HasOne(p=>p.CompetitionGyms)
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
            .HasOne(p=>p.ClimbingGyms)
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
        
        builder.Entity<ClimberLeagues>().ToTable("ClimbersLeagues");
        builder.Entity<ClimberLeagues>().HasKey(p => p.Id);
        builder.Entity<ClimberLeagues>().Property(p => p.Id).IsRequired().ValueGeneratedOnAdd();
        builder.Entity<ClimberLeagues>().Property(p => p.LeagueId).IsRequired();
        builder.Entity<ClimberLeagues>().Property(p => p.ScalerId).IsRequired();
        builder.Entity<ClimberLeagues>().Property(p => p.ClimbingGymId).IsRequired();
        
        // relationships
        
        builder.Entity<ClimberLeagues>()
            .HasOne(p => p.Scaler)
            .WithMany(p => p.ClimbersLeagues)
            .HasForeignKey(p => p.ScalerId);
        
        builder.Entity<ClimberLeagues>()
            .HasOne(p=>p.League)
            .WithMany(p=>p.ClimbersLeagues)
            .HasForeignKey(p=>p.LeagueId);
        
        builder.Entity<ClimberLeagues>()
            .HasOne(p=>p.ClimbingGyms)
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
        
        builder.Entity<CompetitionLeague>()
            .HasMany(p => p.CompetitionLeagueRankings)
            .WithOne(p => p.CompetitionLeague)
            .HasForeignKey(p => p.CompetitionLeagueId);
        
        // CompetitionLeagueRanking entity
        builder.Entity<CompetitionLeagueRanking>().ToTable("CLeagueRankings");
        builder.Entity<CompetitionLeagueRanking>().HasKey(p => p.Id);
        builder.Entity<CompetitionLeagueRanking>().Property(p => p.Id).IsRequired().ValueGeneratedOnAdd();
        builder.Entity<CompetitionLeagueRanking>().Property(p => p.CompetitionLeagueId).IsRequired();
        builder.Entity<CompetitionLeagueRanking>().Property(p => p.ScalerId).IsRequired();
        builder.Entity<CompetitionLeagueRanking>().Property(p => p.Score).IsRequired();
        
        // relationships
        
        builder.Entity<CompetitionLeagueRanking>()
            .HasOne(p => p.Scaler)
            .WithMany(p => p.CompetitionLeagueRankings)
            .HasForeignKey(p => p.ScalerId);
        
        builder.Entity<CompetitionLeagueRanking>()
            .HasOne(p=>p.CompetitionLeague)
            .WithMany(p=>p.CompetitionLeagueRankings)
            .HasForeignKey(p=>p.CompetitionLeagueId);

        // Favorite entity
        builder.Entity<Favorite>().ToTable("Favorites");
        builder.Entity<Favorite>().HasKey(p => p.Id);
        builder.Entity<Favorite>().Property(p => p.Id).IsRequired().ValueGeneratedOnAdd();
        builder.Entity<Favorite>().Property(p => p.ScalerId).IsRequired();
        builder.Entity<Favorite>().Property(p => p.ClimbingGymId).IsRequired();
        
        // relationships
        
        builder.Entity<Favorite>()
            .HasOne(p => p.ClimbingGyms)
            .WithMany(p => p.Favorites)
            .HasForeignKey(p => p.ClimbingGymId);
        
        builder.Entity<Favorite>()
            .HasOne(p=>p.Scaler)
            .WithMany(p=>p.Favorites)
            .HasForeignKey(p=>p.ScalerId);
        
        // Apply Naming Conventions

        builder.UseSnakeCaseNamingConvention();
        
    }
}