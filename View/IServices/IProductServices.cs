using DataProcessing.Models;

namespace View.IServices
{
	public interface IProductServices
	{
		Task<IEnumerable<Product>> GetAllProducts();
		Task<Product> GetProductById(Guid id);
		Task Create(Product product);
		Task Update(Product product);
		Task Delete(Guid id);
	}

	public interface ISoleServices
	{
		Task<IEnumerable<Sole>> GetAllSoles();
		Task<Sole> GetSoleById(Guid id);
		Task Create(Sole sole);
		Task Update(Sole sole);
		Task Delete(Guid id);
	}

	public interface IBrandServices
	{
		Task<IEnumerable<Brand>> GetAllBrands();
		Task<Brand> GetBrandById(Guid id);
		Task Create(Brand Brand);
		Task Update(Brand Brand);
		Task Delete(Guid id);
	}

	public interface ICategoryServices
	{
		Task<IEnumerable<Category>> GetAllCategories();
		Task<Category> GetCategoryById(Guid id);
		Task Create(Category Category);
		Task Update(Category Category);
		Task Delete(Guid id);
	}

	public interface IMaterialServices
	{
		Task<IEnumerable<Material>> GetAllMaterials();
		Task<Material> GetMaterialById(Guid id);
		Task Create(Material Material);
		Task Update(Material Material);
		Task Delete(Guid id);
	}
}
