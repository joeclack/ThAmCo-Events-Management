namespace ThAmCo.Events.Services
{
	using System.Text;
	using System.Text.Json;
	using ThAmCo.Events.DTOs;

	/// <summary>
	/// A service that provides methods to interact with the Catering API
	/// </summary>
	public class CateringService
	{
		/// <summary>
		/// Defines the Catering Service BaseUrl
		/// </summary>
		const string ServiceBaseUrl = "https://localhost:7012/api";

		/// <summary>
		/// Defines the Menu EndPoint
		/// </summary>
		const string MenuEndPoint = "/menus";

		/// <summary>
		/// Defines the FoodItems EndPoint
		/// </summary>
		const string FoodItemsEndPoint = "/fooditems";

		/// <summary>
		/// Defines the FoodBookings EndPoint
		/// </summary>
		const string FoodBookingsEndPoint = "/foodbookings";

		/// <summary>
		/// Defines the MenuFoodItems Endpoint
		/// </summary>
		const string MenuFoodItemsEndpoint = "/menufooditems";

		private readonly HttpClient _httpClient;

		private readonly EventService _eventService;

		/// <summary>
		/// Defines the jsonOptions
		/// </summary>
		JsonSerializerOptions jsonOptions = new JsonSerializerOptions
		{
			PropertyNameCaseInsensitive = true
		};

		public CateringService(HttpClient httpClient, IServiceProvider serviceProvider)
		{
			_httpClient   = httpClient;
			_eventService = serviceProvider.GetRequiredService<EventService>();
		}

		/// <summary>
		/// Retrieves all menus 
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
		/// Retrieves requested menu 
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
		/// Retrieves food items that are not already on the menu 
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
		/// Retrieves all food items 
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
		/// Adds food item to the menu 
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
		/// Removes food item from menu
		/// </summary>
		/// <param name="foodItemId">The foodItemId<see cref="int"/></param>
		/// <param name="menuId">The menuId<see cref="int"/></param>
		/// <returns>The <see cref="Task"/></returns>
		public async Task DeleteMenuFoodItem(int foodItemId, int menuId)
		{
			var request = await _httpClient.DeleteAsync($"{ServiceBaseUrl}{MenuFoodItemsEndpoint}/{menuId}/{foodItemId}");
		}

		/// <summary>
		/// Updates menu info
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
		/// Creates new menu
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
		/// Gets just menu name and ID for food booking
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
		/// Gets menu without menu food items to display in lists for when choosing a menu 
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
		/// Deletes the menu
		/// </summary>
		/// <param name="menuId">The menuId<see cref="int"/></param>
		/// <returns>The <see cref="Task"/></returns>
		public async Task DeleteMenu(int menuId)
		{
			var request = await _httpClient.DeleteAsync($"{ServiceBaseUrl}{MenuEndPoint}/{menuId}");
		}

		/// <summary>
		/// Gets requested food item
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
		/// Creates new food item
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
		/// Updates food item details
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
		/// Deletes food item
		/// </summary>
		/// <param name="foodItemId">The foodItemId<see cref="int"/></param>
		/// <returns>The <see cref="Task"/></returns>
		public async Task DeleteFoodItem(int foodItemId)
		{
			var request = await _httpClient.DeleteAsync($"{ServiceBaseUrl}{FoodItemsEndPoint}/{foodItemId}");
		}

		/// <summary>
		/// Retrieves all food bookings and gets event name that is linked to food booking
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
		/// Retrieves requested food booking
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
		/// Updates food booking details
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
		/// Deletes food booking
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
		/// Creates new food booking
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
		/// Retrieves upcoming food bookings. This includes all bookings with today's date or in the future
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
		/// Retrieves past food bookings
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
