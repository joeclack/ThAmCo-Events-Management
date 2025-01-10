using Microsoft.EntityFrameworkCore;
using ThAmCo.Venues.Data;
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();


builder.Services.AddDbContext<VenuesDbContext>((serviceProvider, options) =>
{
	var configuration = serviceProvider.GetRequiredService<IConfiguration>();
	options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
});

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
