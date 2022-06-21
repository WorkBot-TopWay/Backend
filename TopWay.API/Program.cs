using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using TopWay.API.Security.Authorization.Handlers.Implementations;
using TopWay.API.Security.Authorization.Handlers.Interfaces;
using TopWay.API.Security.Authorization.Middleware;
using TopWay.API.Security.Authorization.Settings;
using TopWay.API.Security.Domain.Repositories;
using TopWay.API.Security.Domain.Services;
using TopWay.API.Security.Persistence.Repositories;
using TopWay.API.Security.Services;
using TopWay.API.Shared.Domain.Repositories;
using TopWay.API.Shared.Persistence.Repositories;
using TopWay.API.TopWay.Domain.Repositories;
using TopWay.API.TopWay.Domain.Services;
using TopWay.API.TopWay.Mapping;
using TopWay.API.TopWay.Persistence.Contexts;
using TopWay.API.TopWay.Persistence.Repositories;
using TopWay.API.TopWay.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Add Database Connection
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<AppDbContext>(
    options => options.UseMySQL(connectionString)
        .LogTo(Console.WriteLine, LogLevel.Information)
        .EnableSensitiveDataLogging()
        .EnableDetailedErrors());

// AppSettings Configuration

builder.Services.Configure<AppSettings>(builder.Configuration.GetSection("AppSettings"));

// OpenAPI Configuration
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
    Version = "v1",
    Title = "TopWay API",
    Description = "TopWay Web Service",
    Contact = new OpenApiContact
    {
        Name = "TopWay",
        Url = new Uri("https://topway.pe")
    },
    License = new OpenApiLicense
    {
        Name = "TopWay Center Resource License",
        Url = new Uri("https://topway.pe/license")
    }
    });
    options.EnableAnnotations();
    options.AddSecurityDefinition("bearerAuth", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey,
        Scheme = "bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "JWT Authorization header using the Bearer scheme. Example: \"Authorization: Bearer {token}\""
    });
    options.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "bearerAuth"
                }
            },
            Array.Empty<string>()
        }
    });
});


//Add lower case routing

builder.Services.AddRouting(options => options.LowercaseUrls = true);

//Dependency Injection Configuration

//Shared injection configuration

builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddCors();

//TopWay injection configuration
builder.Services.AddScoped<IScalerRepository, ScalerRepository>();
builder.Services.AddScoped<IScalerService, ScalerService>();
builder.Services.AddScoped<IClimbingGymRepository, ClimbingGymRepository>();
builder.Services.AddScoped<IClimbingGymService, ClimbingGymService>();
builder.Services.AddScoped<INotificationRepository, NotificationRepository>();
builder.Services.AddScoped<INotificationService, NotificationService>();
builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
builder.Services.AddScoped<ICategoryService, CategoryService>();
builder.Services.AddScoped<ICategoryGymRepository, CategoryGymRepository>();
builder.Services.AddScoped<ICategoryGymService, CategoryGymService>();
builder.Services.AddScoped<IImagesRepository, ImagesRepository>();
builder.Services.AddScoped<IImagesService, ImagesService>();
builder.Services.AddScoped<ICompetitionGymRepository, CompetitionGymRepository>();
builder.Services.AddScoped<ICompetitionGymService, CompetitionGymService>();
builder.Services.AddScoped<ICommentRepository, CommentRepository>();
builder.Services.AddScoped<ICommentService, CommentService>();
builder.Services.AddScoped<ICompetitionReservationClimberRepository, CompetitionReservationClimberRepository>();
builder.Services.AddScoped<ICompetitionReservationClimberService, CompetitionReservationClimberService>();
builder.Services.AddScoped<ICompetitionGymRankingRepository, CompetitionGymRankingRepository>();
builder.Services.AddScoped<ICompetitionGymRankingService, CompetitionGymRankingService>();
builder.Services.AddScoped<ILeagueRepository, LeagueRepository>();
builder.Services.AddScoped<ILeagueService, LeagueService>();
builder.Services.AddScoped<IRequestRepository, RequestRepository>();
builder.Services.AddScoped<IRequestService, RequestService>();
builder.Services.AddScoped<IClimbersLeagueRepository, ClimbersLeagueRepository>();
builder.Services.AddScoped<IClimbersLeagueService, ClimbersLeagueService>();
builder.Services.AddScoped<ICompetitionLeagueRepository, CompetitionLeagueRepository>();
builder.Services.AddScoped<ICompetitionLeagueService, CompetitionLeagueService>();
builder.Services.AddScoped<ICompetitionLeagueRankingRepository, CompetitionLeagueRankingRepository>();
builder.Services.AddScoped<ICompetitionLeagueRankingService, CompetitionLeagueRankingService>();
builder.Services.AddScoped<IFavoriteRepository, FavoriteRepository>();
builder.Services.AddScoped<IFavoriteService, FavoriteService>();
builder.Services.AddScoped<IFeatureRepository, FeatureRepository>();
builder.Services.AddScoped<IFeatureService, FeatureService>();
builder.Services.AddScoped<INewsRepository, NewsRepository>();
builder.Services.AddScoped<INewsService, NewsService>();

//Security injection configuration
builder.Services.AddScoped<IJwtHandler, JwtHandler>();


//Auto Mapper Configuration
builder.Services.AddAutoMapper(
    typeof(TopWay.API.TopWay.Mapping.ModelToResourceProfile), 
    typeof(TopWay.API.TopWay.Mapping.ResourceToModelProfile),
    typeof(TopWay.API.Security.Mapping.ModelToResourceProfile), 
    typeof(TopWay.API.Security.Mapping.ResourceToModelProfile));

var app = builder.Build();

// Creating database of entities
using (var scope = app.Services.CreateScope())
using (var context = scope.ServiceProvider.GetService<AppDbContext>())
{
    context!.Database.EnsureCreated();
}


// Run the application for Swagger
//if (app.Environment.IsDevelopment()) 
//{
    app.UseSwagger();
    app.UseSwaggerUI();
//}

// Configure CORS
app.UseCors(x => x
    .SetIsOriginAllowed(origin => true)
    .AllowAnyMethod()
    .AllowAnyHeader()
    .AllowCredentials());

// Configure Error Handler Middleware

app.UseMiddleware<ErrorHandlerMiddleware>();

// Configure JWT Handling

app.UseMiddleware<JwtMiddleware>();

// Run the application

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();