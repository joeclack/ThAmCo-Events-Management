using Microsoft.EntityFrameworkCore;
using ThAmCo.Venues.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();


// Register database context with the framework
builder.Services.AddDbContext<VenuesDbContext>();


var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
