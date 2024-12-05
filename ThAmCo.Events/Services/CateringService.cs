using Microsoft.EntityFrameworkCore;
using NHibernate.Cfg.MappingSchema;
using System.Net.Http;
using System.Text.Json;
using System.Text.Json.Serialization;
using ThAmCo.Events.DTOs;
using ThAmCo.Events.Models;

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

        internal async Task UpdateMenu(MenuGetDTO menu)
        {
            
        }

        internal async Task<List<FoodItemGetDTO>> GetAvailableFoodItems()
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
            
            //response.EnsureSuccessStatusCode();

            //var jsonResponse = await response.Content.ReadAsStringAsync();
            //var items = JsonSerializer.Deserialize<List<FoodItemGetDTO>>(jsonResponse, jsonOptions);
            //if (items == null)
            //{
            //    throw new ArgumentNullException(nameof(response), "The Food Items response was null");
            //}
            //return items;
        }
    }
}
