using DataProcessing.Models;
using Newtonsoft.Json;
using View.IServices;

namespace View.Servicecs
{
	public class ProductServices : IProductServices
	{
		private readonly HttpClient _httpClient;
        public ProductServices(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public async Task Create(Product product)
		{
			await _httpClient.PostAsJsonAsync("https://localhost:7170/api/Products", product);
		}

		public async Task Delete(Guid id)
		{
			await _httpClient.DeleteAsync($"https://localhost:7170/api/Products/{id}");
		}

		public async Task<IEnumerable<Product>?> GetAllProducts()
		{
			var response = await _httpClient.GetStringAsync("https://localhost:7170/api/Products");
			var products = JsonConvert.DeserializeObject<IEnumerable<Product>>(response);
			return products;
		}

		public async Task<Product?> GetProductById(Guid id)
		{
			var response = await _httpClient.GetStringAsync($"https://localhost:7170/api/Products/{id}");
			var product = JsonConvert.DeserializeObject<Product>(response);
			return product;
		}

		public async Task Update(Product product)
		{
			await _httpClient.PutAsJsonAsync($"https://localhost:7170/api/Products/{product.Id}", product);
		}
	}

	//Sole Services
	public class SoleServices : ISoleServices
	{
		private readonly HttpClient _httpClient;
        public SoleServices(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public async Task Create(Sole sole)
		{
			await _httpClient.PostAsJsonAsync("https://localhost:7170/api/Soles", sole);
		}

		public async Task Delete(Guid id)
		{
			await _httpClient.DeleteAsync($"https://localhost:7170/api/Soles/{id}");
		}

		public async Task<IEnumerable<Sole?>> GetAllSoles()
		{
			var response = await _httpClient.GetStringAsync("https://localhost:7170/api/Soles");
			IEnumerable<Sole>? soles = JsonConvert.DeserializeObject<IEnumerable<Sole>>(response);
			return soles;
		}

		public async Task<Sole?> GetSoleById(Guid id)
		{
			var response = await _httpClient.GetStringAsync($"https://localhost:7170/api/Soles/{id}");
			Sole? sole = JsonConvert.DeserializeObject<Sole>(response);
			return sole;
		}

		public async Task Update(Sole sole)
		{
			await _httpClient.PutAsJsonAsync($"https://localhost:7170/api/Soles/{sole.Id}", sole);
		}
	}

	//Brand Services
	public class BrandServices : IBrandServices
	{
		private readonly HttpClient _httpClient;
		public BrandServices(HttpClient httpClient)
		{
			_httpClient = httpClient;
		}
		public async Task Create(Brand Brand)
		{
			await _httpClient.PostAsJsonAsync("https://localhost:7170/api/Brands", Brand);
		}

		public async Task Delete(Guid id)
		{
			await _httpClient.DeleteAsync($"https://localhost:7170/api/Brands/{id}");
		}

		public async Task<IEnumerable<Brand>?> GetAllBrands()
		{
			var response = await _httpClient.GetStringAsync("https://localhost:7170/api/Brands");
			IEnumerable<Brand>? Brands = JsonConvert.DeserializeObject<IEnumerable<Brand>>(response);
			return Brands;
		}

		public async Task<Brand?> GetBrandById(Guid id)
		{
			var response = await _httpClient.GetStringAsync($"https://localhost:7170/api/Brands/{id}");
			Brand? Brand = JsonConvert.DeserializeObject<Brand>(response);
			return Brand;
		}

		public async Task Update(Brand Brand)
		{
			await _httpClient.PutAsJsonAsync($"https://localhost:7170/api/Brands/{Brand.Id}", Brand);
		}
	}

	//Category Services
	public class CategoryServices : ICategoryServices
	{
		private readonly HttpClient _httpClient;
		public CategoryServices(HttpClient httpClient)
		{
			_httpClient = httpClient;
		}
		public async Task Create(Category Category)
		{
			await _httpClient.PostAsJsonAsync("https://localhost:7170/api/Categories", Category);
		}

		public async Task Delete(Guid id)
		{
			await _httpClient.DeleteAsync($"https://localhost:7170/api/Categories/{id}");
		}

		public async Task<IEnumerable<Category>?> GetAllCategories()
		{
			var response = await _httpClient.GetStringAsync("https://localhost:7170/api/Categories");
			IEnumerable<Category>? Categorys = JsonConvert.DeserializeObject<IEnumerable<Category>>(response);
			return Categorys;
		}

		public async Task<Category?> GetCategoryById(Guid id)
		{
			var response = await _httpClient.GetStringAsync($"https://localhost:7170/api/Categories/{id}");
			Category? Category = JsonConvert.DeserializeObject<Category>(response);
			return Category;
		}

		public async Task Update(Category Category)
		{
			await _httpClient.PutAsJsonAsync($"https://localhost:7170/api/Categories/{Category.Id}", Category);
		}
	}

	//Material Services
	public class MaterialServices : IMaterialServices
	{
		private readonly HttpClient _httpClient;
		public MaterialServices(HttpClient httpClient)
		{
			_httpClient = httpClient;
		}
		public async Task Create(Material Material)
		{
			await _httpClient.PostAsJsonAsync("https://localhost:7170/api/Materials", Material);
		}

		public async Task Delete(Guid id)
		{
			await _httpClient.DeleteAsync($"https://localhost:7170/api/Materials/{id}");
		}

		public async Task<IEnumerable<Material>?> GetAllMaterials()
		{
			var response = await _httpClient.GetStringAsync("https://localhost:7170/api/Materials");
			IEnumerable<Material>? Materials = JsonConvert.DeserializeObject<IEnumerable<Material>>(response);
			return Materials;
		}

		public async Task<Material?> GetMaterialById(Guid id)
		{
			var response = await _httpClient.GetStringAsync($"https://localhost:7170/api/Materials/{id}");
			Material? Material = JsonConvert.DeserializeObject<Material>(response);
			return Material;
		}

		public async Task Update(Material Material)
		{
			await _httpClient.PutAsJsonAsync($"https://localhost:7170/api/Materials/{Material.Id}", Material);
		}
	}
}
