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
                                                 
        JsonSerializerOptions jsonOptions = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        };

        public CateringService(HttpClient httpClient)
        {
            _httpClient = httpClient; // Initialise the HttpClient property.
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
            if(item == null)
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
                if (!menu.MenuFoodItems.Any(x=>x.FoodItemId == f.FoodItemId))
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

        internal async Task CreateMenuFoodItem(int foodItemId, int menuId)
        {
            var request = await _httpClient.PostAsync($"{ServiceBaseUrl}{MenuFoodItemsEndpoint}/{menuId}/{foodItemId}", null);
            request.EnsureSuccessStatusCode();
        }

        internal async Task DeleteMenuFoodItem(int foodItemId, int menuId)
        {
            var request = await _httpClient.DeleteAsync($"{ServiceBaseUrl}{MenuFoodItemsEndpoint}/{menuId}/{foodItemId}");
        }

        internal async Task UpdateMenu(MenuGetDTO menu)
        {
            var json = JsonSerializer.Serialize(menu);
            HttpContent content = new StringContent(json, Encoding.UTF8, "application/json");
            var request = await _httpClient.PutAsync($"{ServiceBaseUrl}{MenuEndPoint}/{menu.MenuId}", content);
        }

        internal async Task CreateMenu(MenuPostDTO menu)
        {
            var json = JsonSerializer.Serialize(menu);
            HttpContent content = new StringContent(json, Encoding.UTF8, "application/json");
            var request = await _httpClient.PostAsync($"{ServiceBaseUrl}{MenuEndPoint}", content);
        }

        internal async Task DeleteMenu(int menuId)
        {
            var request = await _httpClient.DeleteAsync($"{ServiceBaseUrl}{MenuEndPoint}/{menuId}");
        }

        internal async Task<FoodItemGetDTO> GetFoodItem(int id)
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

        internal async Task CreateFoodItem(FoodItemGetDTO foodItem)
        {
            var json = JsonSerializer.Serialize(foodItem);
            HttpContent content = new StringContent(json, Encoding.UTF8, "application/json");
            var request = await _httpClient.PostAsync($"{ServiceBaseUrl}{FoodItemsEndPoint}", content);
        }

        internal async Task UpdateFoodItem(FoodItemGetDTO foodItem)
        {
            var json = JsonSerializer.Serialize(foodItem);
            HttpContent content = new StringContent(json, Encoding.UTF8, "application/json");
            var request = await _httpClient.PutAsync($"{ServiceBaseUrl}{FoodItemsEndPoint}/{foodItem.FoodItemId}", content);
        }

        internal async Task DeleteFoodItem(int foodItemId)
        {
            var request = await _httpClient.DeleteAsync($"{ServiceBaseUrl}{FoodItemsEndPoint}/{foodItemId}");
        }
    }
}
