using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.AspNetCore.Identity.UI.Services;
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
	.AddDefaultUI()
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

	var roles = new[] { "Super", "Team Leader", "Manager", "User" };

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
	var userManager = scope.ServiceProvider.GetRequiredService<UserManager<IdentityUser>>();
	var password = "Test123!";

	var basic = new IdentityUser
	{
		UserName = "user@user.com",
		Email = "user@user.com"
	};

	var manager = new IdentityUser
	{
		UserName = "manager@manager.com",
		Email = "manager@manager.com"
	};

	var teamLeader = new IdentityUser
	{
		UserName = "teamLeader@teamLeader.com",
		Email = "teamLeader@teamLeader.com"
	};

	var super = new IdentityUser
	{
		UserName = "super@super.com",
		Email = "super@super.com"
	};

	var users = new Dictionary<IdentityUser, string>
	{
		{ basic, "User" },
		{ manager, "Manager" },
		{ teamLeader, "Team Leader" },
		{ super, "Super" }
	};

	foreach (var user in users)
	{
		if (await userManager.FindByEmailAsync(user.Key.Email) == null)
		{
			await userManager.CreateAsync(user.Key, password);
			await userManager.AddToRoleAsync(user.Key, user.Value);
		}
	}
}

app.Run();

