using DataProcessing.Models;
using Newtonsoft.Json;
using System.Net.Http;
using View.IServices;

namespace View.Servicecs
{
    public class AddressServices: IAddressService
    {
        private readonly HttpClient _httpClient;

        public AddressServices(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public async Task<List<Address>> GetAllAddresses()
        {
            string requestURL = "https://localhost:7170/api/Address/GetAllAddress";
            var response = await _httpClient.GetStringAsync(requestURL);
            return JsonConvert.DeserializeObject<List<Address>>(response);
        }

        public async Task<Address> GetAddressById(Guid id)
        {
            string requestURL = $"https://localhost:7170/api/Address/GetAddressById?id={id}";
            var response = await _httpClient.GetStringAsync(requestURL);
            return JsonConvert.DeserializeObject<Address>(response);
        }

        public async Task<List<Address>> GetAddressByUserId(Guid userId)
        {
            string requestURL = $"https://localhost:7170/api/Address/GetAddressByUserId?userId={userId}";
            var response = await _httpClient.GetStringAsync(requestURL);
            return JsonConvert.DeserializeObject<List<Address>>(response);
        }

        public async Task Create(Address address)
        {
            string requestURL = "https://localhost:7170/api/Address/CreateAddress";
            var response = await _httpClient.PostAsJsonAsync(requestURL, address);
        }

        public async Task Update(Address address, Guid id)
        {
            string requestURL = $"https://localhost:7170/api/Address/UpdateAddress?id={id}";
            var response = await _httpClient.PutAsJsonAsync(requestURL, address);
        }

        public async Task Delete(Guid id)
        {
            string requestURL = $"https://localhost:7170/api/Address/Delete?id={id}";
            await _httpClient.DeleteAsync(requestURL);
        }
        // lấy api địa chỉ Việt Nam từ bên tứ 3
        public async Task<List<Province>> GetProvincesAsync()
        {
            string url = "https://open.oapi.vn/location/provinces?size=100";
            var response = await _httpClient.GetFromJsonAsync<ProvinceResponse>(url);

            return response?.Data ?? new List<Province>();
        }
        public async Task<List<Districted>> GetDistrictsAsync(int idProvince)
        {
            string url = $"https://open.oapi.vn/location/districts/{idProvince}?page=0&size=30";
            var response = await _httpClient.GetFromJsonAsync<DistrictResponse>(url);

            return response?.Data ?? new List<Districted>();
        }

        public async Task<List<Ward>> GetWardsAsync(int idDistrict)
        {
            string url = $"https://open.oapi.vn/location/wards?districtId={idDistrict}";
            var response = await _httpClient.GetFromJsonAsync<WardResponse>(url);

            return response?.Data ?? new List<Ward>();
        }
    }
    // Định nghĩa cấu trúc dữ liệu phản hồi từ API
    public class ProvinceResponse
    {
        public List<Province> Data { get; set; }
        public string Code { get; set; }
    }
    public class DistrictResponse
    {
        public List<Districted> Data { get; set; }
        public string Code { get; set; }
    }

    public class WardResponse
    {
        public List<Ward> Data { get; set; }
        public string Code { get; set; }
    }

    public class Province
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public int Type { get; set; }
        public string TypeText { get; set; }
        public string Slug { get; set; }
    }
    public class Districted
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string ProvinceId { get; set; } 
        public int Type { get; set; }
        public string TypeText { get; set; }
    }

    public class Ward
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string DistrictId { get; set; } 
        public int Type { get; set; }
        public string TypeText { get; set; }
    }
}
