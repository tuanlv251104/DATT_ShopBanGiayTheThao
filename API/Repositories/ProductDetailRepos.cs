using API.Data;
using API.DTO;
using API.IRepositories;
using DataProcessing.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Repositories
{
    public class ProductDetailRepos : IProductDetailRepos
    {
        private readonly ApplicationDbContext _context;
        public ProductDetailRepos(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task Create(ProductDetail productDetail)
        {
            if (await GetProductDetailById(productDetail.Id) != null)
                throw new DuplicateWaitObjectException($"Product Detail : {productDetail.Id} is existed!");
            await _context.ProductDetails.AddAsync(productDetail);
        }

        public async Task Delete(string id)
        {
            var productDetail = await GetProductDetailById(id);
            if (productDetail == null) throw new KeyNotFoundException("Not found this Id!");
            _context.ProductDetails.Remove(productDetail);
        }

        public async Task<List<ProductDetail>> GetAllProductDetail()
        {
            return await _context.ProductDetails
                .Include(p => p.Color)
                .Include(p => p.Size).
                Include(p => p.Product)
                .ThenInclude(p => p.Material).
                Include(p => p.Product)
                .ThenInclude(p => p.Sole).
                Include(p => p.Product)
                .ThenInclude(p => p.Brand).
                Include(p => p.Product)
                .ThenInclude(p => p.Category)
                 .ToListAsync();
        }

        public async Task<List<ProductDetail>> GetFilteredProductDetails(
    string? searchQuery = null, // Tham số cho tìm kiếm
    Guid? colorId = null,
    Guid? sizeId = null,
    Guid? categoryId = null,
    Guid? brandId = null,
    Guid? soleId = null,
    Guid? materialId = null)
        {
            var query = _context.ProductDetails
                .Include(p => p.Color)
                .Include(p => p.Size)
                .Include(p => p.Product)
                    .ThenInclude(p => p.Material)
                .Include(p => p.Product)
                    .ThenInclude(p => p.Sole)
                .Include(p => p.Product)
                    .ThenInclude(p => p.Brand)
                .Include(p => p.Product)
                    .ThenInclude(p => p.Category)
                .AsQueryable();

            // Lọc theo Color nếu colorId không null
            if (colorId.HasValue)
            {
                query = query.Where(p => p.Color.Id == colorId.Value);
            }

            // Lọc theo Size nếu sizeId không null
            if (sizeId.HasValue)
            {
                query = query.Where(p => p.Size.Id == sizeId.Value);
            }

            // Lọc theo Category nếu categoryId không null
            if (categoryId.HasValue)
            {
                query = query.Where(p => p.Product.Category.Id == categoryId.Value);
            }

            // Lọc theo Brand nếu brandId không null
            if (brandId.HasValue)
            {
                query = query.Where(p => p.Product.Brand.Id == brandId.Value);
            }

            // Lọc theo Sole nếu soleId không null
            if (soleId.HasValue)
            {
                query = query.Where(p => p.Product.Sole.Id == soleId.Value);
            }

            // Lọc theo Material nếu materialId không null
            if (materialId.HasValue)
            {
                query = query.Where(p => p.Product.Material.Id == materialId.Value);
            }

            // Tìm kiếm theo tên sản phẩm nếu searchQuery không null hoặc không trống
            if (!string.IsNullOrWhiteSpace(searchQuery))
            {
                query = query.Where(p => p.Product.Name.Contains(searchQuery)); // Giả sử bạn có thuộc tính Name trong Product
            }

            return await query.ToListAsync();
        }




        public async Task<ProductDetail?> GetProductDetailById(string id)
        {
            return await _context.ProductDetails.Where(p => p.Id == id).Include(p => p.Color)
                .Include(p => p.Size)
                .Include(p => p.Product)
                .FirstOrDefaultAsync();
        }


        public async Task<List<ProductDetailDTO>> GetVariantsByProductIds(List<Guid> productIds)
        {
            if (productIds == null || !productIds.Any())
            {
                return new List<ProductDetailDTO>();
            }

            var productVariants = await _context.ProductDetails
                .Where(pd => productIds.Contains(pd.ProductId))
                .Select(pd => new ProductDetailDTO
                {
                    Id = pd.Id,
                    ProductName = pd.Product.Name,
                    PriceProductDetail = pd.Price,
                    CategoryName = pd.Product.Category.Name, // Tên danh mục từ bảng Category
                    sizeValue = pd.Size != null ? pd.Size.Value :0,
                    BrandName = pd.Product.Brand.Name,      // Tên thương hiệu từ bảng Brand
                    MaterialName = pd.Product.Material.Name, // Tên chất liệu từ bảng Material
                    ColorName = pd.Color != null ? pd.Color.Name : "No color", // Tên màu từ bảng Color
                    SoleName = pd.Product.Sole != null ? pd.Product.Sole.TypeName : "No sole" // Tên đế giày từ bảng Sole
                })
                .ToListAsync();

            return productVariants;
        }


        public async Task SaveChanges()
        {
            await _context.SaveChangesAsync();
        }

        public async Task Update(ProductDetail productDetail)
        {
            if (await GetProductDetailById(productDetail.Id) == null) throw new KeyNotFoundException("Not found this Id!");
            _context.Entry(productDetail).State = EntityState.Modified;

        }


    }
}
