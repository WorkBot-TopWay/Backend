using Microsoft.EntityFrameworkCore;
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


//Add lower case routing

builder.Services.AddRouting(options => options.LowercaseUrls = true);

//Dependency Injection Configuration

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
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();


//Auto Mapper Configuration
builder.Services.AddAutoMapper(
    typeof(ModelToResourceProfile), 
    typeof(ResourceToModelProfile));

var app = builder.Build();

// Creating database of entities
using (var scope = app.Services.CreateScope())
using (var context = scope.ServiceProvider.GetService<AppDbContext>())
{
    context!.Database.EnsureCreated();
}

// Run the application for Swagger
if (app.Environment.IsDevelopment()) 
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();