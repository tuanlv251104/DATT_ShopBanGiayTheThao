 using Data.Models;
using Data.ViewModels;
using DataProcessing.Models;

namespace API.IRepositories
{
    public interface IPromotionRepos
    {
        Task<List<Promotion>> GetAllPromotion();
        Task<Promotion> GetPromotionById(Guid id);
        Task<List<ProductDetailsPromotionViewModel>> GetAllProductDetailsPromotion();
        Task Create(Promotion promotion);
        Task Update(Promotion promotion);
        Task Delete(Guid id);
        Task SaveChanges();
    }
    public interface IProductDetailPromotionRepos
    {
        Task<ProductDetailPromotion> GetByIdAsync( Guid id);
        Task<IEnumerable<ProductDetailPromotion>> GetAllAsync();
        Task AddAsync(ProductDetailPromotion productDetailPromotion);
        Task UpdateAsync(ProductDetailPromotion productDetailPromotion);
        Task DeleteAsync(Guid id);
        Task SaveChanges();

    }
}
