using Data.Models;
using Data.ViewModels;
using DataProcessing.Models;

namespace View.IServices
{
    public interface IPromotionServices
    {

        Task<List<Promotion>> GetAllPromotion();
        Task<Promotion> GetPromotionById(Guid? id);
        Task Create(Promotion promotion);
        Task Update(Promotion promotion);
        Task Delete(Guid id);
        Task<List<ProductDetailsPromotionViewModel>> GetAllProductDetailsPromotion();
    }
    public interface IProductDetailPromotionServices
    {
        Task<ProductDetailPromotion> GetByIdAsync(Guid? id);
        Task<IEnumerable<ProductDetailPromotion>?> GetAllAsync();
        Task AddAsync(ProductDetailPromotion productDetailPromotion);
        Task UpdateAsync(ProductDetailPromotion productDetailPromotion);
        Task DeleteAsync(Guid? id);
    }
}
