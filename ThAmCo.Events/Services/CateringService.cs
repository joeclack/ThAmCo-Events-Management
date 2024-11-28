using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Text.Json;
using ThAmCo.Events.DTOs;

namespace ThAmCo.Events.Services
{
    public class CateringService
    {
        const string ServiceBaseUrl = "https://localhost:7012/api";
        const string CategoryEndPoint = "/menus";
        private readonly HttpClient _httpClient; 
                                                 
        JsonSerializerOptions jsonOptions = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        };

        public CateringService(HttpClient httpClient)
        {
            _httpClient = httpClient; // Initialise the HttpClient property.
        }

        #region Menus

        public async Task<List<MenuDTO>> GetMenus()
        {
            var response = await _httpClient.GetAsync(ServiceBaseUrl + CategoryEndPoint);
            response.EnsureSuccessStatusCode();

            var jsonResponse = await response.Content.ReadAsStringAsync();
            var items = JsonConvert.DeserializeObject<List<MenuDTO>>(jsonResponse);
            if (items == null)
            {
                throw new ArgumentNullException(nameof(response), "The Menus response is null.");
            }
            return items;
        }

        public async Task<FlatMenuDTO> GetMenu(int id)
        {
            try
            {
                var response = await _httpClient.GetAsync(ServiceBaseUrl + CategoryEndPoint + $"/{id}");
                response.EnsureSuccessStatusCode();

                var jsonResponse = await response.Content.ReadAsStringAsync();
                var item = JsonConvert.DeserializeObject<FlatMenuDTO>(jsonResponse);
                if (item == null)
                {
                    throw new ArgumentNullException(nameof(response), "The Menu response is null");
                }
                return item;
            }
            catch (HttpRequestException e)
            {
                Console.WriteLine("Could not get menu");
                return null;
            }
        }

        public async Task<IActionResult> CreateMenu(MenuDTO menuDTO)
        {
            var httpContent = new StringContent(JsonConvert.SerializeObject(menuDTO));
            httpContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            var response = await _httpClient.PostAsync(ServiceBaseUrl + CategoryEndPoint, httpContent);
            response.EnsureSuccessStatusCode();
            return new OkResult();
        }

        public async Task<IActionResult> EditMenu(int id, MenuDTO menuDTO)
        {
            var httpContent = new StringContent(JsonConvert.SerializeObject(menuDTO));
            httpContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            var response = await _httpClient.PutAsync(ServiceBaseUrl + CategoryEndPoint + $"/{id}", httpContent);
            response.EnsureSuccessStatusCode();
            return new OkResult();
        }

        public async Task<IActionResult> DeleteMenu(int id)
        {
            var response = await _httpClient.DeleteAsync(ServiceBaseUrl + CategoryEndPoint + $"/{id}");
            response.EnsureSuccessStatusCode();
            return new OkResult();
        }

        #endregion

        #region FoodItems

        public async Task<IActionResult> CreateFoodItem(FoodItemDTO foodItemDTO)
        {
            var httpContent = new StringContent(JsonConvert.SerializeObject(foodItemDTO));
            httpContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            var response = await _httpClient.PostAsync(ServiceBaseUrl + CategoryEndPoint + "/fooditems", httpContent);
            response.EnsureSuccessStatusCode();
            return new OkResult();
        }

        public async Task<IActionResult> EditFoodItem(int id, FoodItemDTO foodItemDTO)
        {
            var httpContent = new StringContent(JsonConvert.SerializeObject(foodItemDTO));
            httpContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            var response = await _httpClient.PutAsync(ServiceBaseUrl + CategoryEndPoint + $"/fooditems/{id}", httpContent);
            response.EnsureSuccessStatusCode();
            return new OkResult();
        }

        public async Task<IActionResult> DeleteFoodItem(int id)
        {
            var response = await _httpClient.DeleteAsync(ServiceBaseUrl + CategoryEndPoint + $"/fooditems/{id}");
            response.EnsureSuccessStatusCode();
            return new OkResult();
        }

        public async Task<List<FoodItemDTO>> GetFoodItems()
        {
            var response = await _httpClient.GetAsync(ServiceBaseUrl + CategoryEndPoint + "/fooditems");
            response.EnsureSuccessStatusCode();
            var jsonResponse = await response.Content.ReadAsStringAsync();
            var items = JsonConvert.DeserializeObject<List<FoodItemDTO>>(jsonResponse);
            if (items == null)
            {
                throw new ArgumentNullException(nameof(response), "The FoodItems response is null.");
            }
            return items;
        }

        public async Task<FoodItemDTO> GetFoodItem(int id)
        {
            var response = await _httpClient.GetAsync(ServiceBaseUrl + CategoryEndPoint + $"/fooditems/{id}");
            response.EnsureSuccessStatusCode();
            var jsonResponse = await response.Content.ReadAsStringAsync();
            var item = JsonConvert.DeserializeObject<FoodItemDTO>(jsonResponse);
            if (item == null)
            {
                throw new ArgumentNullException(nameof(response), "The FoodItem response is null.");
            }
            return item;
        }

        #endregion

        #region FoodBookings

        public async Task<IActionResult> CreateFoodBooking(FoodBookingDTO foodBookingDTO)
        {
            var httpContent = new StringContent(JsonConvert.SerializeObject(foodBookingDTO));
            httpContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            var response = await _httpClient.PostAsync(ServiceBaseUrl + CategoryEndPoint + "/foodbookings", httpContent);
            response.EnsureSuccessStatusCode();
            return new OkResult();
        }

        public async Task<IActionResult> EditFoodBooking(int id, FoodBookingDTO foodBookingDTO)
        {
            var httpContent = new StringContent(JsonConvert.SerializeObject(foodBookingDTO));
            httpContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            var response = await _httpClient.PutAsync(ServiceBaseUrl + CategoryEndPoint + $"/foodbookings/{id}", httpContent);
            response.EnsureSuccessStatusCode();
            return new OkResult();
        }

        public async Task<IActionResult> DeleteFoodBooking(int id)
        {
            var response = await _httpClient.DeleteAsync(ServiceBaseUrl + CategoryEndPoint + $"/foodbookings/{id}");
            response.EnsureSuccessStatusCode();
            return new OkResult();
        }

        public async Task<List<FoodBookingDTO>> GetFoodBookings()
        {
            var response = await _httpClient.GetAsync(ServiceBaseUrl + CategoryEndPoint + "/foodbookings");
            response.EnsureSuccessStatusCode();
            var jsonResponse = await response.Content.ReadAsStringAsync();
            var items = JsonConvert.DeserializeObject<List<FoodBookingDTO>>(jsonResponse);
            if (items == null)
            {
                throw new ArgumentNullException(nameof(response), "The FoodBookings response is null.");
            }
            return items;
        }

        #endregion

        #region MenuFoodItems

        public async Task<IActionResult> CreateMenuFoodItem(MenuFoodItemDTO menuFoodItemDTO)
        {
            var httpContent = new StringContent(JsonConvert.SerializeObject(menuFoodItemDTO));
            httpContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            var response = await _httpClient.PostAsync(ServiceBaseUrl + CategoryEndPoint + "/menufooditems", httpContent);
            response.EnsureSuccessStatusCode();
            return new OkResult();
        }

        public async Task<IActionResult> EditMenuFoodItem(int id, MenuFoodItemDTO menuFoodItemDTO)
        {
            var httpContent = new StringContent(JsonConvert.SerializeObject(menuFoodItemDTO));
            httpContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            var response = await _httpClient.PutAsync(ServiceBaseUrl + CategoryEndPoint + $"/menufooditems/{id}", httpContent);
            response.EnsureSuccessStatusCode();
            return new OkResult();
        }

        public async Task<IActionResult> DeleteMenuFoodItem(int id)
        {
            var response = await _httpClient.DeleteAsync(ServiceBaseUrl + CategoryEndPoint + $"/menufooditems/{id}");
            response.EnsureSuccessStatusCode();
            return new OkResult();
        }

        #endregion
    }
}
