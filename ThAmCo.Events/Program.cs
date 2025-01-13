using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.EntityFrameworkCore;
using ThAmCo.Events.Data;
using ThAmCo.Events.Services;
using ThAmCo.Events.Support;

var builder = WebApplication.CreateBuilder(args);

builder.Services.ConfigureSameSiteNoneCookies();

builder.Services.AddDbContext<EventsDbContext>(options =>
	options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddDbContext<ApplicationDbContext>(options =>
		options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddIdentity<IdentityUser, IdentityRole>()
	.AddEntityFrameworkStores<ApplicationDbContext>()
	.AddDefaultTokenProviders();

builder.Services.AddRazorPages(options =>
{
	options.Conventions.AuthorizePage("/Events");
	options.Conventions.AuthorizePage("/Catering");
	options.Conventions.AuthorizePage("/Guests");
	options.Conventions.AuthorizePage("/Staff");
});
builder.Services.AddDbContext<EventsDbContext>();
builder.Services.AddTransient<StaffService>();
builder.Services.AddTransient<EventService>();
builder.Services.AddTransient<GuestService>();
builder.Services.AddHttpClient<CateringService>();
builder.Services.ConfigureApplicationCookie(options =>
{
	options.AccessDeniedPath = "/Identity/Account/AccessDenied";
});
var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseCookiePolicy();

app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

app.MapRazorPages();

using(var scope = app.Services.CreateScope())
{
	var roleManager =
		scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();

	var roles = new[] { "Super", "Team Leader", "Manager" };

	foreach(var role in roles)
	{
		if(!await roleManager.RoleExistsAsync(role))
		{
			await roleManager.CreateAsync(new IdentityRole(role));
		}
	}
}

using (var scope = app.Services.CreateScope())
{
	var userManager =
		scope.ServiceProvider.GetRequiredService<UserManager<IdentityUser>>();

	var email = "manager@manager.com";
	var password  = "Test123!";

	if (await userManager.FindByEmailAsync(email) == null)
	{
		var user = new IdentityUser();
		user.UserName = email;
		user.Email = email;

		await userManager.CreateAsync(user, password);

		await userManager.AddToRoleAsync(user, "Manager");
	}
}

app.Run();

