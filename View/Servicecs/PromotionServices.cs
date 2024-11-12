using Data.Models;
using Data.ViewModels;
using DataProcessing.Models;
using Newtonsoft.Json;
using View.IServices;

namespace View.Servicecs
{
    public class PromotionServices : IPromotionServices
    {
        private readonly HttpClient _httpClient;

        public PromotionServices(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task Create(Promotion promotion)
        {
            await _httpClient.PostAsJsonAsync("https://localhost:7170/api/Promotions", promotion);
        }

        public async Task Delete(Guid id)
        {
            await _httpClient.DeleteAsync($"https://localhost:7170/api/Promotions/{id}");
        }

        public async Task<List<ProductDetailsPromotionViewModel>> GetAllProductDetailsPromotion()
        {
            var response = await _httpClient.GetStringAsync("https://localhost:7170/api/Promotions/ProductDetailsPromotion");
            var listProductDetailsPromotion = JsonConvert.DeserializeObject<List<ProductDetailsPromotionViewModel>>(response);
            return listProductDetailsPromotion;
        }

        public async Task<List<Promotion>?> GetAllPromotion()
        {
            var response = await _httpClient.GetStringAsync("https://localhost:7170/api/Promotions");
            var listPromotion = JsonConvert.DeserializeObject<List<Promotion>>(response);
            return listPromotion;
        }

        public async Task<Promotion?> GetPromotionById(Guid? id)
        {
            var response = await _httpClient.GetStringAsync($"https://localhost:7170/api/Promotions/{id}");
            var PromotionItem = JsonConvert.DeserializeObject<Promotion>(response);
            return PromotionItem;
        }

        public async Task Update(Promotion promotion)
        {
            await _httpClient.PutAsJsonAsync($"https://localhost:7170/api/Promotions/{promotion.Id}", promotion);
        }
    }
    public class ProductDetailPromotionServices : IProductDetailPromotionServices
    {
        private readonly HttpClient _httpClient;

        public ProductDetailPromotionServices(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task AddAsync(ProductDetailPromotion productDetailPromotion)
        {
            await _httpClient.PostAsJsonAsync("https://localhost:7170/api/ProductDetailPromotions", productDetailPromotion);
        }

        public async Task DeleteAsync(Guid? id)
        {
            await _httpClient.DeleteAsync($"https://localhost:7170/api/ProductDetailPromotions{id}");
        }

        public async Task<IEnumerable<ProductDetailPromotion>?> GetAllAsync()
        {
            var response = await _httpClient.GetStringAsync("https://localhost:7170/api/ProductDetailPromotions");
            var list = JsonConvert.DeserializeObject<IEnumerable<ProductDetailPromotion>>(response);
            return list;
        }

        public async Task<ProductDetailPromotion> GetByIdAsync(Guid? id)
        {
            var response = await _httpClient.GetStringAsync($"https://localhost:7170/api/ProductDetailPromotions/{id}");
            var item = JsonConvert.DeserializeObject<ProductDetailPromotion>(response);
            return item;
        }

        public async Task UpdateAsync(ProductDetailPromotion productDetailPromotion)
        {
            await _httpClient.PutAsJsonAsync($"https://localhost:7170/api/ProductDetailPromotions/{productDetailPromotion.Id}", productDetailPromotion);
        }
    }
}
