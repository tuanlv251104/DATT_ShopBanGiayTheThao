using API.Data;
using API.IRepositories;
using Data.Models;
using DataProcessing.Models;
using Microsoft.EntityFrameworkCore;

namespace API.Repositories
{
    public class ProductDetailPromotionRepo : IProductDetailPromotionRepos
    {
        private readonly ApplicationDbContext _context;

        public ProductDetailPromotionRepo(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(ProductDetailPromotion productDetailPromotion)
        {
            // Kiểm tra sự tồn tại của ProductDetail và Promotion
            var productDetailExists = await _context.ProductDetails.AnyAsync(pd => pd.Id == productDetailPromotion.ProductDetailId);
            var promotionExists = await _context.Promotions.AnyAsync(p => p.Id == productDetailPromotion.PromotionId);

            if (!productDetailExists)
            {
                throw new InvalidOperationException($"ProductDetail with ID {productDetailPromotion.ProductDetailId} does not exist.");
            }

            if (!promotionExists)
            {
                throw new InvalidOperationException($"Promotion with ID {productDetailPromotion.PromotionId} does not exist.");
            }

            // Kiểm tra xem productDetailPromotion có tồn tại hay không
            if (await GetByIdAsync(productDetailPromotion.Id) != null)
            {
                throw new InvalidOperationException($"ProductDetailPromotion with ID {productDetailPromotion.Id} already exists.");
            }

            await _context.ProductDetailPromotions.AddAsync(productDetailPromotion);
            await _context.SaveChangesAsync(); // Lưu thay đổi vào cơ sở dữ liệu
        }


        public Task DeleteAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<ProductDetailPromotion>> GetAllAsync()
        {
            return await _context.ProductDetailPromotions.ToListAsync();
        }

        public async Task<ProductDetailPromotion> GetByIdAsync(Guid id)
        {
           return await _context.ProductDetailPromotions.FindAsync(id);
        }

        public async Task SaveChanges()
        {
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(ProductDetailPromotion productDetailPromotion)
        {
            if (await GetByIdAsync(productDetailPromotion.Id) == null) throw new KeyNotFoundException("Not found this Id!");
            _context.Entry(productDetailPromotion).State = EntityState.Modified;
        }
    }
}
