namespace ThAmCo.Events.Services
{
	using System.Text;
	using System.Text.Json;
	using ThAmCo.Events.DTOs;

	/// <summary>
	/// Defines the <see cref="CateringService" />
	/// </summary>
	public class CateringService
	{
		/// <summary>
		/// Defines the ServiceBaseUrl
		/// </summary>
		const string ServiceBaseUrl = "https://localhost:7012/api";

		/// <summary>
		/// Defines the MenuEndPoint
		/// </summary>
		const string MenuEndPoint = "/menus";

		/// <summary>
		/// Defines the FoodItemsEndPoint
		/// </summary>
		const string FoodItemsEndPoint = "/fooditems";

		/// <summary>
		/// Defines the FoodBookingsEndPoint
		/// </summary>
		const string FoodBookingsEndPoint = "/foodbookings";

		/// <summary>
		/// Defines the MenuFoodItemsEndpoint
		/// </summary>
		const string MenuFoodItemsEndpoint = "/menufooditems";

		/// <summary>
		/// Defines the _httpClient
		/// </summary>
		private readonly HttpClient _httpClient;

		/// <summary>
		/// Defines the _eventService
		/// </summary>
		private readonly EventService _eventService;

		/// <summary>
		/// Defines the jsonOptions
		/// </summary>
		JsonSerializerOptions jsonOptions = new JsonSerializerOptions
		{
			PropertyNameCaseInsensitive = true
		};

		/// <summary>
		/// Initializes a new instance of the <see cref="CateringService"/> class.
		/// </summary>
		/// <param name="httpClient">The httpClient<see cref="HttpClient"/></param>
		/// <param name="serviceProvider">The serviceProvider<see cref="IServiceProvider"/></param>
		public CateringService(HttpClient httpClient, IServiceProvider serviceProvider)
		{
			_httpClient   = httpClient;
			_eventService = serviceProvider.GetRequiredService<EventService>();
		}

		/// <summary>
		/// The GetMenus
		/// </summary>
		/// <returns>The <see cref="Task{List{MenuGetDTO}}"/></returns>
		public async Task<List<MenuGetDTO>> GetMenus()
		{
			var response = await _httpClient.GetAsync(ServiceBaseUrl + MenuEndPoint);
			response.EnsureSuccessStatusCode();

			var jsonResponse = await response.Content.ReadAsStringAsync();
			var items        = JsonSerializer.Deserialize<List<MenuGetDTO>>(jsonResponse, jsonOptions);

			if (items == null)
			{
				throw new ArgumentNullException(nameof(response), "The Menus response is null.");
			}
			return items;
		}

		/// <summary>
		/// The GetMenu
		/// </summary>
		/// <param name="id">The id<see cref="int"/></param>
		/// <returns>The <see cref="Task{MenuGetDTO}"/></returns>
		public async Task<MenuGetDTO> GetMenu(int id)
		{
			var response = await _httpClient.GetAsync(ServiceBaseUrl + MenuEndPoint + $"/{id}");
			response.EnsureSuccessStatusCode();

			var jsonResponse = await response.Content.ReadAsStringAsync();
			var item         = JsonSerializer.Deserialize<MenuGetDTO>(jsonResponse, jsonOptions);

			if (item == null)
			{
				throw new ArgumentNullException(nameof(response), "The Menu response is null");
			}
			return item;
		}

		/// <summary>
		/// The GetAvailableFoodItems
		/// </summary>
		/// <param name="menu">The menu<see cref="MenuGetDTO"/></param>
		/// <returns>The <see cref="Task{List{FoodItemGetDTO}}"/></returns>
		internal async Task<List<FoodItemGetDTO>> GetAvailableFoodItems(MenuGetDTO menu)
		{
			List<FoodItemGetDTO> availableItems = [];
			var allItems                        = await GetFoodItems();
			foreach (var f in allItems)
			{
				if (!menu.MenuFoodItems.Any(x => x.FoodItemId == f.FoodItemId))
				{
					availableItems.Add(f);
				}
			}
			return availableItems;
		}

		/// <summary>
		/// The GetFoodItems
		/// </summary>
		/// <returns>The <see cref="Task{List{FoodItemGetDTO}}"/></returns>
		public async Task<List<FoodItemGetDTO>> GetFoodItems()
		{
			var response = await _httpClient.GetAsync(ServiceBaseUrl + FoodItemsEndPoint);
			response.EnsureSuccessStatusCode();

			var jsonResponse = await response.Content.ReadAsStringAsync();
			var items        = JsonSerializer.Deserialize<List<FoodItemGetDTO>>(jsonResponse, jsonOptions);

			if (items == null)
			{
				throw new ArgumentNullException(nameof(response), "The Food Items response was null");
			}
			return items;
		}

		/// <summary>
		/// The CreateMenuFoodItem
		/// </summary>
		/// <param name="foodItemId">The foodItemId<see cref="int"/></param>
		/// <param name="menuId">The menuId<see cref="int"/></param>
		/// <returns>The <see cref="Task"/></returns>
		public async Task CreateMenuFoodItem(int foodItemId, int menuId)
		{
			var request = await _httpClient.PostAsync($"{ServiceBaseUrl}{MenuFoodItemsEndpoint}/{menuId}/{foodItemId}", null);
			request.EnsureSuccessStatusCode();
		}

		/// <summary>
		/// The DeleteMenuFoodItem
		/// </summary>
		/// <param name="foodItemId">The foodItemId<see cref="int"/></param>
		/// <param name="menuId">The menuId<see cref="int"/></param>
		/// <returns>The <see cref="Task"/></returns>
		public async Task DeleteMenuFoodItem(int foodItemId, int menuId)
		{
			var request = await _httpClient.DeleteAsync($"{ServiceBaseUrl}{MenuFoodItemsEndpoint}/{menuId}/{foodItemId}");
		}

		/// <summary>
		/// The UpdateMenu
		/// </summary>
		/// <param name="menu">The menu<see cref="MenuGetDTO"/></param>
		/// <returns>The <see cref="Task"/></returns>
		public async Task UpdateMenu(MenuGetDTO menu)
		{
			var json            = JsonSerializer.Serialize(menu);
			HttpContent content = new StringContent(json, Encoding.UTF8, "application/json");
			var request         = await _httpClient.PutAsync($"{ServiceBaseUrl}{MenuEndPoint}/{menu.MenuId}", content);
		}

		/// <summary>
		/// The CreateMenu
		/// </summary>
		/// <param name="menu">The menu<see cref="MenuPostDTO"/></param>
		/// <returns>The <see cref="Task"/></returns>
		public async Task CreateMenu(MenuPostDTO menu)
		{
			var json            = JsonSerializer.Serialize(menu);
			HttpContent content = new StringContent(json, Encoding.UTF8, "application/json");
			var request         = await _httpClient.PostAsync($"{ServiceBaseUrl}{MenuEndPoint}", content);
		}

		/// <summary>
		/// The FetchMenuInfoForBooking
		/// </summary>
		/// <param name="foodBookingId">The foodBookingId<see cref="int"/></param>
		/// <returns>The <see cref="Task{MenuInfoDTO}"/></returns>
		public async Task<MenuInfoDTO> FetchMenuInfoForBooking(int foodBookingId)
		{
			var foodBooking = await GetFoodBooking(foodBookingId);
			var menuId      = foodBooking.MenuId;
			var response    = await _httpClient.GetAsync($"{ServiceBaseUrl}{MenuEndPoint}/{menuId}");

			var jsonResponse = await response.Content.ReadAsStringAsync();
			var menuInfo     = JsonSerializer.Deserialize<MenuInfoDTO>(jsonResponse, jsonOptions);

			return menuInfo;
		}

		/// <summary>
		/// The GetMenuInfo
		/// </summary>
		/// <param name="menuId">The menuId<see cref="int"/></param>
		/// <returns>The <see cref="Task{MenuInfoDTO}"/></returns>
		public async Task<MenuInfoDTO> GetMenuInfo(int menuId)
		{
			var response     = await _httpClient.GetAsync($"{ServiceBaseUrl}{MenuEndPoint}/{menuId}");

			var jsonResponse = await response.Content.ReadAsStringAsync();
			var menuInfo     = JsonSerializer.Deserialize<MenuInfoDTO>(jsonResponse, jsonOptions);

			return menuInfo;
		}

		/// <summary>
		/// The DeleteMenu
		/// </summary>
		/// <param name="menuId">The menuId<see cref="int"/></param>
		/// <returns>The <see cref="Task"/></returns>
		public async Task DeleteMenu(int menuId)
		{
			var request = await _httpClient.DeleteAsync($"{ServiceBaseUrl}{MenuEndPoint}/{menuId}");
		}

		/// <summary>
		/// The GetFoodItem
		/// </summary>
		/// <param name="id">The id<see cref="int"/></param>
		/// <returns>The <see cref="Task{FoodItemGetDTO}"/></returns>
		public async Task<FoodItemGetDTO> GetFoodItem(int id)
		{
			var response = await _httpClient.GetAsync(ServiceBaseUrl + FoodItemsEndPoint + $"/{id}");
			response.EnsureSuccessStatusCode();

			var jsonResponse = await response.Content.ReadAsStringAsync();
			var item         = JsonSerializer.Deserialize<FoodItemGetDTO>(jsonResponse, jsonOptions);

			if (item == null)
			{
				throw new ArgumentNullException(nameof(response), "The Menu response is null");
			}
			return item;
		}

		/// <summary>
		/// The CreateFoodItem
		/// </summary>
		/// <param name="foodItem">The foodItem<see cref="FoodItemGetDTO"/></param>
		/// <returns>The <see cref="Task"/></returns>
		public async Task CreateFoodItem(FoodItemGetDTO foodItem)
		{
			var json            = JsonSerializer.Serialize(foodItem);
			HttpContent content = new StringContent(json, Encoding.UTF8, "application/json");
			var request         = await _httpClient.PostAsync($"{ServiceBaseUrl}{FoodItemsEndPoint}", content);
		}

		/// <summary>
		/// The UpdateFoodItem
		/// </summary>
		/// <param name="foodItem">The foodItem<see cref="FoodItemGetDTO"/></param>
		/// <returns>The <see cref="Task"/></returns>
		public async Task UpdateFoodItem(FoodItemGetDTO foodItem)
		{
			var json            = JsonSerializer.Serialize(foodItem);
			HttpContent content = new StringContent(json, Encoding.UTF8, "application/json");
			var request         = await _httpClient.PutAsync($"{ServiceBaseUrl}{FoodItemsEndPoint}/{foodItem.FoodItemId}", content);
		}

		/// <summary>
		/// The DeleteFoodItem
		/// </summary>
		/// <param name="foodItemId">The foodItemId<see cref="int"/></param>
		/// <returns>The <see cref="Task"/></returns>
		public async Task DeleteFoodItem(int foodItemId)
		{
			var request = await _httpClient.DeleteAsync($"{ServiceBaseUrl}{FoodItemsEndPoint}/{foodItemId}");
		}

		/// <summary>
		/// The GetFoodBookings
		/// </summary>
		/// <returns>The <see cref="Task{List{FoodBookingDTO}}"/></returns>
		public async Task<List<FoodBookingDTO>> GetFoodBookings()
		{
			var response = await _httpClient.GetAsync(ServiceBaseUrl + FoodBookingsEndPoint);
			response.EnsureSuccessStatusCode();

			var jsonResponse = await response.Content.ReadAsStringAsync();
			var items        = JsonSerializer.Deserialize<List<FoodBookingDTO>>(jsonResponse, jsonOptions);

			if (items == null)
			{
				throw new ArgumentNullException(nameof(response), "The FoodBookings response is null");
			}
			var menus       = await GetMenus();
			var events      = await _eventService.GetAllEvents();
			items.Select(fb => fb.MenuName = menus.FirstOrDefault(m => m.MenuId == fb.MenuId).MenuName).ToList();
			foreach (var booking in items)
			{
				booking.MenuName  = menus.FirstOrDefault(x => x.MenuId == booking.MenuId).MenuName;
				var matchedEvent  = events.FirstOrDefault(x => x.FoodBookingId == booking.FoodBookingId);
				if (matchedEvent != null)
				{
					booking.EventName = matchedEvent.Name;
				}
			}
			return items;
		}

		/// <summary>
		/// The GetFoodBooking
		/// </summary>
		/// <param name="id">The id<see cref="int"/></param>
		/// <returns>The <see cref="Task{FoodBookingDTO}"/></returns>
		internal async Task<FoodBookingDTO> GetFoodBooking(int id)
		{
			var response = await _httpClient.GetAsync(ServiceBaseUrl + FoodBookingsEndPoint + $"/{id}");
			if (response.IsSuccessStatusCode)
			{
				var jsonResponse = await response.Content.ReadAsStringAsync();
				var item         = JsonSerializer.Deserialize<FoodBookingDTO>(jsonResponse, jsonOptions);
				if (item         == null)
				{
					throw new ArgumentNullException(nameof(response), "The FoodBooking response is null");
				}
				var menu      = await GetMenu(item.MenuId);
				item.MenuName = menu.MenuName;
				return item;
			}

			return null;
		}

		/// <summary>
		/// The UpdateFoodBooking
		/// </summary>
		/// <param name="foodBooking">The foodBooking<see cref="FoodBookingDTO"/></param>
		/// <returns>The <see cref="Task"/></returns>
		public async Task UpdateFoodBooking(FoodBookingDTO foodBooking)
		{
			FoodBookingPostDTO foodBookingPost = new FoodBookingPostDTO().CreateDTO(foodBooking);
			var response                       = await _httpClient.PutAsJsonAsync(ServiceBaseUrl + FoodBookingsEndPoint + $"/{foodBooking.FoodBookingId}", foodBookingPost);
			response.EnsureSuccessStatusCode();
		}

		/// <summary>
		/// The DeleteFoodBooking
		/// </summary>
		/// <param name="foodBookingId">The foodBookingId<see cref="int"/></param>
		/// <returns>The <see cref="Task"/></returns>
		internal async Task DeleteFoodBooking(int foodBookingId)
		{
			var request = await _httpClient.DeleteAsync($"{ServiceBaseUrl}{FoodBookingsEndPoint}/{foodBookingId}");
			var events  = await _eventService.GetAllEvents();
			var _event  = events.FirstOrDefault(x => x.FoodBookingId == foodBookingId);
			if (_event != null)
			{
				_event.FoodBookingId = -1;
				await _eventService.UpdateEvent(_event);
			}
		}

		/// <summary>
		/// The CreateFoodBooking
		/// </summary>
		/// <param name="foodBooking">The foodBooking<see cref="FoodBookingDTO"/></param>
		/// <returns>The <see cref="Task{int}"/></returns>
		internal async Task<int> CreateFoodBooking(FoodBookingDTO foodBooking)
		{
			var response = await _httpClient.PostAsJsonAsync($"{ServiceBaseUrl}{FoodBookingsEndPoint}", foodBooking);
			if (response.IsSuccessStatusCode)
			{
				var createdDto = await response.Content.ReadFromJsonAsync<FoodBookingDTO>();
				return createdDto?.FoodBookingId ?? -1;
			}

			return -1;
		}

		/// <summary>
		/// The GetUpcomingFoodBookings
		/// </summary>
		/// <returns>The <see cref="Task{List{FoodBookingDTO}}"/></returns>
		internal async Task<List<FoodBookingDTO>> GetUpcomingFoodBookings()
		{
			var bookings = await GetFoodBookings();
			if (bookings.Count == 0)
			{
				return [];
			}
			var upcoming = new List<FoodBookingDTO>(bookings.Where(x => x.FoodBookingDate >= DateTime.Today));
			return upcoming;
		}

		/// <summary>
		/// The GetPastFoodBookings
		/// </summary>
		/// <returns>The <see cref="Task{List{FoodBookingDTO}}"/></returns>
		internal async Task<List<FoodBookingDTO>> GetPastFoodBookings()
		{
			var bookings = await GetFoodBookings();
			if (bookings.Count == 0)
			{
				return [];
			}
			return new List<FoodBookingDTO>(bookings.Where(x => x.FoodBookingDate < DateTime.Today));
		}
	}
}
