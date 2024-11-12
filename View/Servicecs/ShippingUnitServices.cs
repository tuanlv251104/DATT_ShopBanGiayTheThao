using DataProcessing.Models;
using Newtonsoft.Json;
using View.IServices;

namespace View.Servicecs
{
    public class ShippingUnitServices : IShippingUnitServices
    {
        private readonly HttpClient _httpClient;

        public ShippingUnitServices(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task create(ShippingUnit shippingUnit)
        {
            await _httpClient.PostAsJsonAsync("https://localhost:7170/api/ShippingUnits", shippingUnit);
        }

        public async Task delete(Guid? id)
        {
            await _httpClient.DeleteAsync($"https://localhost:7170/api/ShippingUnits/{id}");
        }

        public async Task<List<ShippingUnit>?> GetAllShippingUnit()
        {
            var response = await _httpClient.GetStringAsync("https://localhost:7170/api/ShippingUnits");
            var result = JsonConvert.DeserializeObject<List<ShippingUnit>>(response);
            return result;
        }

        public async Task<ShippingUnit> GetShippingUnitById(Guid? id)
        {
            var response = await _httpClient.GetStringAsync($"https://localhost:7170/api/ShippingUnits/{id}");
            var result = JsonConvert.DeserializeObject<ShippingUnit>(response);
            return result;
        }

        public async Task update(ShippingUnit shippingUnit)
        {
            await _httpClient.PutAsJsonAsync($"https://localhost:7170/api/ShippingUnits{shippingUnit.ShippingUnitID}", shippingUnit);
        }
    }
}
