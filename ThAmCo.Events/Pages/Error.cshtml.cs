namespace ThAmCo.Events.Pages
{
	using Microsoft.AspNetCore.Mvc;
	using Microsoft.AspNetCore.Mvc.RazorPages;
	using System.Diagnostics;

	/// <summary>
	/// Defines the <see cref="ErrorModel" />
	/// </summary>
	[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
	[IgnoreAntiforgeryToken]
	public class ErrorModel : PageModel
	{
		/// <summary>
		/// Gets or sets the RequestId
		/// </summary>
		public string? RequestId { get; set; }

		/// <summary>
		/// Gets a value indicating whether ShowRequestId
		/// </summary>
		public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);

		/// <summary>
		/// Defines the _logger
		/// </summary>
		private readonly ILogger<ErrorModel> _logger;

		/// <summary>
		/// Initializes a new instance of the <see cref="ErrorModel"/> class.
		/// </summary>
		/// <param name="logger">The logger<see cref="ILogger{ErrorModel}"/></param>
		public ErrorModel(ILogger<ErrorModel> logger)
		{
			_logger = logger;
		}

		/// <summary>
		/// The OnGet
		/// </summary>
		public void OnGet()
		{
			RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier;
		}
	}

}
