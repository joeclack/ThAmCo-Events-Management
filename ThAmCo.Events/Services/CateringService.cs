using Microsoft.DotNet.MSIdentity.Shared;
using NuGet.Versioning;
using System.Text;
using System.Text.Json;
using ThAmCo.Events.DTOs;

namespace ThAmCo.Events.Services
{
	public class CateringService
	{
		const string ServiceBaseUrl = "https://localhost:7012/api";
		const string MenuEndPoint = "/menus";
		const string FoodItemsEndPoint = "/fooditems";
		const string FoodBookingsEndPoint = "/foodbookings";
		const string MenuFoodItemsEndpoint = "/menufooditems";
		private readonly HttpClient _httpClient;

		private readonly EventService _eventService;

		JsonSerializerOptions jsonOptions = new JsonSerializerOptions
		{
			PropertyNameCaseInsensitive = true
		};

		public CateringService(HttpClient httpClient, IServiceProvider serviceProvider)
		{
			_httpClient = httpClient;
			_eventService = serviceProvider.GetRequiredService<EventService>();
		}

		public async Task<List<MenuGetDTO>> GetMenus()
		{
			var response = await _httpClient.GetAsync(ServiceBaseUrl + MenuEndPoint);
			response.EnsureSuccessStatusCode();

			var jsonResponse = await response.Content.ReadAsStringAsync();
			var items = JsonSerializer.Deserialize<List<MenuGetDTO>>(jsonResponse, jsonOptions);
			if (items == null)
			{
				throw new ArgumentNullException(nameof(response), "The Menus response is null.");
			}
			return items;
		}

		public async Task<MenuGetDTO> GetMenu(int id)
		{
			var response = await _httpClient.GetAsync(ServiceBaseUrl + MenuEndPoint + $"/{id}");
			response.EnsureSuccessStatusCode();

			var jsonResponse = await response.Content.ReadAsStringAsync();
			var item = JsonSerializer.Deserialize<MenuGetDTO>(jsonResponse, jsonOptions);
			if (item == null)
			{
				throw new ArgumentNullException(nameof(response), "The Menu response is null");
			}
			return item;
		}

		internal async Task<List<FoodItemGetDTO>> GetAvailableFoodItems(MenuGetDTO menu)
		{
			List<FoodItemGetDTO> availableItems = [];
			var allItems = await GetFoodItems();
			foreach (var f in allItems)
			{
				if (!menu.MenuFoodItems.Any(x => x.FoodItemId == f.FoodItemId))
				{
					availableItems.Add(f);
				}
			}
			return availableItems;
		}

		public async Task<List<FoodItemGetDTO>> GetFoodItems()
		{
			var response = await _httpClient.GetAsync(ServiceBaseUrl + FoodItemsEndPoint);
			response.EnsureSuccessStatusCode();

			var jsonResponse = await response.Content.ReadAsStringAsync();
			var items = JsonSerializer.Deserialize<List<FoodItemGetDTO>>(jsonResponse, jsonOptions);
			if (items == null)
			{
				throw new ArgumentNullException(nameof(response), "The Food Items response was null");
			}
			return items;
		}

		public async Task CreateMenuFoodItem(int foodItemId, int menuId)
		{
			var request = await _httpClient.PostAsync($"{ServiceBaseUrl}{MenuFoodItemsEndpoint}/{menuId}/{foodItemId}", null);
			request.EnsureSuccessStatusCode();
		}

		public async Task DeleteMenuFoodItem(int foodItemId, int menuId)
		{
			var request = await _httpClient.DeleteAsync($"{ServiceBaseUrl}{MenuFoodItemsEndpoint}/{menuId}/{foodItemId}");
		}

		public async Task UpdateMenu(MenuGetDTO menu)
		{
			var json = JsonSerializer.Serialize(menu);
			HttpContent content = new StringContent(json, Encoding.UTF8, "application/json");
			var request = await _httpClient.PutAsync($"{ServiceBaseUrl}{MenuEndPoint}/{menu.MenuId}", content);
		}

		public async Task CreateMenu(MenuPostDTO menu)
		{
			var json = JsonSerializer.Serialize(menu);
			HttpContent content = new StringContent(json, Encoding.UTF8, "application/json");
			var request = await _httpClient.PostAsync($"{ServiceBaseUrl}{MenuEndPoint}", content);
		}

		public async Task<MenuInfoDTO> FetchMenuInfoForBooking(int foodBookingId)
		{
			var foodBooking = await GetFoodBooking(foodBookingId);
			var menuId = foodBooking.MenuId;
			var response = await _httpClient.GetAsync($"{ServiceBaseUrl}{MenuEndPoint}/{menuId}");

			var jsonResponse = await response.Content.ReadAsStringAsync();
			var menuInfo = JsonSerializer.Deserialize<MenuInfoDTO>(jsonResponse, jsonOptions);

			return menuInfo;
		}

		public async Task<MenuInfoDTO> GetMenuInfo(int menuId)
		{
			var response = await _httpClient.GetAsync($"{ServiceBaseUrl}{MenuEndPoint}/{menuId}");

			var jsonResponse = await response.Content.ReadAsStringAsync();
			var menuInfo = JsonSerializer.Deserialize<MenuInfoDTO>(jsonResponse, jsonOptions);

			return menuInfo;
		}

		public async Task DeleteMenu(int menuId)
		{
			var request = await _httpClient.DeleteAsync($"{ServiceBaseUrl}{MenuEndPoint}/{menuId}");
		}

		public async Task<FoodItemGetDTO> GetFoodItem(int id)
		{
			var response = await _httpClient.GetAsync(ServiceBaseUrl + FoodItemsEndPoint + $"/{id}");
			response.EnsureSuccessStatusCode();

			var jsonResponse = await response.Content.ReadAsStringAsync();
			var item = JsonSerializer.Deserialize<FoodItemGetDTO>(jsonResponse, jsonOptions);
			if (item == null)
			{
				throw new ArgumentNullException(nameof(response), "The Menu response is null");
			}
			return item;
		}

		public async Task CreateFoodItem(FoodItemGetDTO foodItem)
		{
			var json = JsonSerializer.Serialize(foodItem);
			HttpContent content = new StringContent(json, Encoding.UTF8, "application/json");
			var request = await _httpClient.PostAsync($"{ServiceBaseUrl}{FoodItemsEndPoint}", content);
		}

		public async Task UpdateFoodItem(FoodItemGetDTO foodItem)
		{
			var json = JsonSerializer.Serialize(foodItem);
			HttpContent content = new StringContent(json, Encoding.UTF8, "application/json");
			var request = await _httpClient.PutAsync($"{ServiceBaseUrl}{FoodItemsEndPoint}/{foodItem.FoodItemId}", content);
		}

		public async Task DeleteFoodItem(int foodItemId)
		{
			var request = await _httpClient.DeleteAsync($"{ServiceBaseUrl}{FoodItemsEndPoint}/{foodItemId}");
		}

		public async Task<List<FoodBookingDTO>> GetFoodBookings()
		{
			var response = await _httpClient.GetAsync(ServiceBaseUrl + FoodBookingsEndPoint);
			response.EnsureSuccessStatusCode();

			var jsonResponse = await response.Content.ReadAsStringAsync();
			var items = JsonSerializer.Deserialize<List<FoodBookingDTO>>(jsonResponse, jsonOptions);
			if (items == null)
			{
				throw new ArgumentNullException(nameof(response), "The FoodBookings response is null");
			}
			var menus = await GetMenus();
			var events = await _eventService.GetAllEvents();
			items.Select(fb => fb.MenuName = menus.FirstOrDefault(m => m.MenuId == fb.MenuId).MenuName).ToList();
			foreach(var booking in items)
			{
				booking.MenuName = menus.FirstOrDefault(x => x.MenuId == booking.MenuId).MenuName;
				var matchedEvent = events.FirstOrDefault(x => x.FoodBookingId == booking.FoodBookingId);
				if (matchedEvent != null)
				{
					booking.EventName = matchedEvent.Name;
				}
			}
			return items;
		}

		internal async Task<FoodBookingDTO> GetFoodBooking(int id)
		{
			var response = await _httpClient.GetAsync(ServiceBaseUrl + FoodBookingsEndPoint + $"/{id}");
			if (response.IsSuccessStatusCode)
			{
				var jsonResponse = await response.Content.ReadAsStringAsync();
				var item = JsonSerializer.Deserialize<FoodBookingDTO>(jsonResponse, jsonOptions);
				if (item == null)
				{
					throw new ArgumentNullException(nameof(response), "The FoodBooking response is null");
				}
				var menu = await GetMenu(item.MenuId);
				item.MenuName = menu.MenuName;
				return item;
			}

			return null;
		}

		public async Task UpdateFoodBooking(FoodBookingDTO foodBooking)
		{
			FoodBookingPostDTO foodBookingPost = new FoodBookingPostDTO().CreateDTO(foodBooking);
			var response = await _httpClient.PutAsJsonAsync(ServiceBaseUrl + FoodBookingsEndPoint + $"/{foodBooking.FoodBookingId}", foodBookingPost);
			response.EnsureSuccessStatusCode();
		}

		internal async Task DeleteFoodBooking(int foodBookingId)
		{
			var request = await _httpClient.DeleteAsync($"{ServiceBaseUrl}{FoodBookingsEndPoint}/{foodBookingId}");
			var events = await _eventService.GetAllEvents();
			var _event = events.FirstOrDefault(x=>x.FoodBookingId == foodBookingId);
			_event.FoodBookingId = -1;
			await _eventService.UpdateEvent(_event);
		}

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

		internal async Task<List<FoodBookingDTO>> GetUpcomingFoodBookings()
		{
			var bookings = await GetFoodBookings();
			if(bookings.Count == 0)
			{
				return [];
			}
			var upcoming = new List<FoodBookingDTO>(bookings.Where(x=>x.FoodBookingDate >= DateTime.Today));
			return upcoming;
		}

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
