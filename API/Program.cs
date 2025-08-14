using API.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddDbContext<AppDBContext>(options =>
{
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection"));

});

builder.Services.AddCors();

var app = builder.Build();

// Configure the Http request pipeline.

app.UseCors(
    options => options.AllowAnyMethod()
        .AllowAnyHeader()
        .WithOrigins("https://localhost:4200", "http://localhost:4200")
);
app.MapControllers();

app.Run();
