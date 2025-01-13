namespace ThAmCo.Events.Pages.Guests
{
	using Microsoft.AspNetCore.Authorization;
	using Microsoft.AspNetCore.Mvc;
	using Microsoft.AspNetCore.Mvc.RazorPages;
	using System;
	using System.Threading.Tasks;
	using ThAmCo.Events.Models;
	using ThAmCo.Events.Services;

	/// <summary>
	/// Defines the <see cref="CreateModel" />
	/// </summary>
	[Authorize(Roles = "Manager")]
	public class CreateModel : PageModel
	{
		/// <summary>
		/// Defines the _context
		/// </summary>
		private readonly ThAmCo.Events.Data.EventsDbContext _context;

		/// <summary>
		/// Defines the _guestService
		/// </summary>
		private GuestService _guestService;

		/// <summary>
		/// Initializes a new instance of the <see cref="CreateModel"/> class.
		/// </summary>
		/// <param name="context">The context<see cref="ThAmCo.Events.Data.EventsDbContext"/></param>
		/// <param name="serviceProvider">The serviceProvider<see cref="IServiceProvider"/></param>
		public CreateModel(ThAmCo.Events.Data.EventsDbContext context, IServiceProvider serviceProvider)
		{
			_context      = context;
			_guestService = serviceProvider.GetRequiredService<GuestService>();
		}

		/// <summary>
		/// The OnGet
		/// </summary>
		/// <returns>The <see cref="IActionResult"/></returns>
		public IActionResult OnGet()
		{
			return Page();
		}

		/// <summary>
		/// Gets or sets the Guest
		/// </summary>
		[BindProperty]
		public Guest Guest { get; set; } = default!;

		/// <summary>
		/// The OnPostAsync
		/// </summary>
		/// <returns>The <see cref="Task{IActionResult}"/></returns>
		public async Task<IActionResult> OnPostAsync()
		{
			if (!ModelState.IsValid)
			{
				return Page();
			}
			await _guestService.CreateGuest(Guest);

			return RedirectToPage("./Index");
		}
	}
}
