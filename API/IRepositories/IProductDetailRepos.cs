using API.DTO;
using DataProcessing.Models;
using Microsoft.AspNetCore.Mvc;

namespace API.IRepositories
{
    public interface IProductDetailRepos
    {
        Task<List<ProductDetail>> GetAllProductDetail();
        Task<ProductDetail> GetProductDetailById(string id);
        Task Create(ProductDetail productDetail);
        Task Update(ProductDetail productDetail);
        Task Delete(string id);
        Task<List<ProductDetailDTO>> GetVariantsByProductIds(List<Guid> productIds);
        Task SaveChanges();
        Task<List<ProductDetail>> GetFilteredProductDetails(
            string? searchQuery = null,
       Guid? colorId = null,
       Guid? sizeId = null,
       Guid? categoryId = null,
       Guid? brandId = null,
       Guid? soleId = null,
       Guid? materialId = null);
    }
}
