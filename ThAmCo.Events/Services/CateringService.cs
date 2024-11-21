using NHibernate.Cfg.MappingSchema;
using System.Net.Http;
using System.Text.Json;
using System.Text.Json.Serialization;
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

        public async Task<List<MenuDTO>> GetMenus()
        {
            var response = await _httpClient.GetAsync(ServiceBaseUrl + CategoryEndPoint);
            response.EnsureSuccessStatusCode();

            var jsonResponse = await response.Content.ReadAsStringAsync();
            var items = JsonSerializer.Deserialize<List<MenuDTO>>(jsonResponse, jsonOptions);
            if (items == null)
            {
                throw new ArgumentNullException(nameof(response), "The Menus response is null.");
            }
            return items;
        }

        public async Task<MenuDTO> GetMenu(int id)
        {
            var response = await _httpClient.GetAsync(ServiceBaseUrl + CategoryEndPoint + $"/{id}");
            response.EnsureSuccessStatusCode();

            var jsonResponse = await response.Content.ReadAsStringAsync();
            var item = JsonSerializer.Deserialize<MenuDTO>(jsonResponse, jsonOptions);
            if(item == null)
            {
                throw new ArgumentNullException(nameof(response), "The Menu response is null");
            }
            return item;
        }
    }
}
