using API.Data;
using API.IRepositories;
using Data.ViewModels;
using DataProcessing.Models;
using Microsoft.EntityFrameworkCore;

namespace API.Repositories
{
    public class PromotionRepos : IPromotionRepos
    {
        private readonly ApplicationDbContext _context;

        public PromotionRepos(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task Create(Promotion promotion)
        {
           if(await GetPromotionById(promotion.Id) != null)
            {
                throw new DuplicateWaitObjectException($"promotion : {promotion.Id} is existed!");
            }
           await _context.Promotions .AddAsync(promotion);
        }

        public async Task Delete(Guid id)
        {
            var deleteItem = await GetPromotionById(id);
            if (deleteItem == null)
            {
                throw new KeyNotFoundException("Not found this Id!");
            }
              _context.Promotions.Remove (deleteItem);
        }

        public async Task<List<ProductDetailsPromotionViewModel>> GetAllProductDetailsPromotion()
        {
            var result = await _context.ProductDetailPromotions
               .Include(pdp => pdp.ProductDetail)
                   .ThenInclude(pd => pd.Product)
                       .ThenInclude(p => p.Category)
               .Include(pdp => pdp.ProductDetail)
                   .ThenInclude(pd => pd.Product)
                       .ThenInclude(p => p.Brand)
               .Include(pdp => pdp.ProductDetail)
                   .ThenInclude(pd => pd.Product)
                       .ThenInclude(p => p.Material)
               .Include(pdp => pdp.ProductDetail)
                   .ThenInclude(pd => pd.Color)
               .Include(pdp => pdp.ProductDetail)
                   .ThenInclude(pd => pd.Product)
                       .ThenInclude(p => p.Sole)
               .Include(pdp => pdp.Promotion)
               .Select(pdp => new ProductDetailsPromotionViewModel
               {
                   promotionId = pdp.PromotionId, 
                   ProductDetailsID = pdp.ProductDetailId,
                   ProductName = pdp.ProductDetail.Product.Name,
                   CategoryName = pdp.ProductDetail.Product.Category.Name,
                   BrandName = pdp.ProductDetail.Product.Brand.Name,
                   MaterialName = pdp.ProductDetail.Product.Material.Name,
                   SizeValue = pdp.ProductDetail.Size != null ? pdp.ProductDetail.Size.Value : 0,
                   ColorName = pdp.ProductDetail.Color != null ? pdp.ProductDetail.Color.Name : "No color",
                   SoleName = pdp.ProductDetail.Product.Sole != null ? pdp.ProductDetail.Product.Sole.TypeName : "No sole",
                   PriceProductDetail = pdp.ProductDetail.Price,
                   PricePromotion = pdp.PriceUpdate
               })
               .ToListAsync();

            return result;
        }


        public async Task<List<Promotion>> GetAllPromotion()
        {
            return await _context.Promotions.ToListAsync();
        }

        public async Task<Promotion> GetPromotionById(Guid id)
        {
            return await _context.Promotions.FindAsync(id);
        }

        public async Task SaveChanges()
        {
            await _context.SaveChangesAsync();
        }

        public async Task Update(Promotion promotion)
        {
            var item = await GetPromotionById (promotion.Id);
            if (item == null)
            {
                throw new KeyNotFoundException("Not found this Id!");
            }
            _context.Entry(promotion).State = EntityState.Modified;
        }
    }
}
