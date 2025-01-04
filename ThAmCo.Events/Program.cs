using Auth0.AspNetCore.Authentication;
using ThAmCo.Events.Data;
using ThAmCo.Events.Services;
using ThAmCo.Events.Support;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddAuth0WebAppAuthentication(options =>
{
	options.Domain = builder.Configuration["Auth0:Domain"];
	options.ClientId = builder.Configuration["Auth0:ClientId"];
});
// Add services to the container.
builder.Services.ConfigureSameSiteNoneCookies();

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
app.UseEndpoints(endpoints =>
	{
		// endpoints.MapGet("/", context =>
		// {
		// 	context.Response.Redirect("/Events");
		// 	return Task.CompletedTask;
		// });
		endpoints.MapDefaultControllerRoute();
	});

app.MapRazorPages();
app.Run();

