using DataProcessing.Models;
using Newtonsoft.Json;
using View.IServices;

namespace View.Servicecs
{
	public class ColorServices : IColorServices
	{
		private readonly HttpClient _httpClient;

		public ColorServices(HttpClient httpClient)
		{
			_httpClient = httpClient;
		}
		public async Task Create(Color color)
		{
			await _httpClient.PostAsJsonAsync("https://localhost:7170/api/Colors", color);
		}

		public async Task Delete(Guid id)
		{
			await _httpClient.DeleteAsync($"https://localhost:7170/api/Colors/{id}");
		}

		public async Task<IEnumerable<Color>?> GetAllColors()
		{
			var response = await _httpClient.GetStringAsync("https://localhost:7170/api/Colors");
			var colors = JsonConvert.DeserializeObject<IEnumerable<Color>>(response);
			return colors;
		}

		public async Task<Color?> GetColorById(Guid id)
		{
			var response = await _httpClient.GetStringAsync($"https://localhost:7170/api/Colors/{id}");
			var color = JsonConvert.DeserializeObject<Color>(response);
			return color;
		}

		public async Task Update(Color color)
		{
			await _httpClient.PutAsJsonAsync($"https://localhost:7170/api/Colors/{color.Id}", color);
		}
	}

	public class ImageServices : IImageServices
	{
		private readonly HttpClient _httpClient;

		public ImageServices(HttpClient httpClient)
		{
			_httpClient = httpClient;
		}
		public async Task Create(Image Image)
		{
			await _httpClient.PostAsJsonAsync("https://localhost:7170/api/Images", Image);
		}

		public async Task Delete(Guid id)
		{
			await _httpClient.DeleteAsync($"https://localhost:7170/api/Images/{id}");
		}

		public async Task<IEnumerable<Image>?> GetAllImages()
		{
			var response = await _httpClient.GetStringAsync("https://localhost:7170/api/Images");
			var Images = JsonConvert.DeserializeObject<IEnumerable<Image>>(response);
			return Images;
		}

		public async Task<Image?> GetImageById(Guid id)
		{
			var response = await _httpClient.GetStringAsync($"https://localhost:7170/api/Images/{id}");
			var Image = JsonConvert.DeserializeObject<Image>(response);
			return Image;
		}

		public async Task<IEnumerable<Image>?> GetImagesByColorId(Guid id)
		{
			var Images = GetAllImages().Result;
			return Images != null ? Images.Where(i => i.ColorId == id) : Images;
		}

		public async Task Update(Image Image)
		{
			await _httpClient.PutAsJsonAsync($"https://localhost:7170/api/Images/{Image.Id}", Image);
		}
	}

	public class SelectedImageServices : ISelectedImageServices
	{
		private readonly HttpClient _httpClient;

		public SelectedImageServices(HttpClient httpClient)
		{
			_httpClient = httpClient;
		}
		public async Task Create(SelectedImage SelectedImage)
		{
			await _httpClient.PostAsJsonAsync("https://localhost:7170/api/SelectedImages", SelectedImage);
		}

		public async Task Delete(Guid id)
		{
			await _httpClient.DeleteAsync($"https://localhost:7170/api/SelectedImages/{id}");
		}

		public async Task<IEnumerable<SelectedImage>?> GetAllSelectedImages()
		{
			var response = await _httpClient.GetStringAsync("https://localhost:7170/api/SelectedImages");
			var SelectedImages = JsonConvert.DeserializeObject<IEnumerable<SelectedImage>>(response);
			return SelectedImages;
		}

		public async Task<SelectedImage?> GetSelectedImageById(Guid id)
		{
			var response = await _httpClient.GetStringAsync($"https://localhost:7170/api/SelectedImages/{id}");
			var SelectedImage = JsonConvert.DeserializeObject<SelectedImage>(response);
			return SelectedImage;
		}

		public async Task<IEnumerable<SelectedImage>?> GetSelectedImagesByProductId(Guid id, Guid colorId)
		{
			var SelectedImages = GetAllSelectedImages().Result;
			return SelectedImages != null ? SelectedImages.Where(si => si.ProductId == id && si.ColorId == colorId) : SelectedImages;
		}

		public async Task Update(SelectedImage SelectedImage)
		{
			await _httpClient.PutAsJsonAsync($"https://localhost:7170/api/SelectedImages/{SelectedImage.Id}", SelectedImage);
		}
	}
}
