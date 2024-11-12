using DataProcessing.Models;
using Newtonsoft.Json;
using View.IServices;

namespace View.Servicecs
{
	public class SizeServices : ISizeServices
	{
		private readonly HttpClient _httpClient;

        public SizeServices(HttpClient httpClient)
        {
			_httpClient = httpClient;
        }
        public async Task Create(Size size)
		{
			await _httpClient.PostAsJsonAsync("https://localhost:7170/api/Sizes", size);
		}

		public async Task Delete(Guid id)
		{
			await _httpClient.DeleteAsync($"https://localhost:7170/api/Sizes/{id}");
		}

		public async Task<IEnumerable<Size>?> GetAllSizes()
		{
			var response = await _httpClient.GetStringAsync("https://localhost:7170/api/Sizes");
			var sizes = JsonConvert.DeserializeObject<IEnumerable<Size>>(response);
			return sizes;
		}

		public async Task<Size?> GetSizeById(Guid id)
		{
			var response = await _httpClient.GetStringAsync($"https://localhost:7170/api/Sizes/{id}");
			var size = JsonConvert.DeserializeObject<Size>(response);
			return size;			
		}

		public async Task Update(Size size)
		{
			await _httpClient.PutAsJsonAsync($"https://localhost:7170/api/Sizes/{size.Id}", size);
		}
	}
}
