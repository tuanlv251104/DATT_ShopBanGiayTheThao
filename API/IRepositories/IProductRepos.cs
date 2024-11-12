using DataProcessing.Models;

namespace API.IRepositories
{
	public interface IProductRepo
	{
		Task<List<Product>> GetAllProduct();
		Task<Product> GetProductById(Guid id);
		Task Create(Product product);
		Task Update(Product product);
		Task Delete(Guid id);
		Task SaveChanges();
	}

	public interface ISoleRepo
	{
		Task<List<Sole>> GetAllSole();
		Task<Sole> GetSoleById(Guid id);
		Task Create(Sole sole);
		Task Update(Sole sole);
		Task Delete(Guid id);
		Task SaveChanges();
	}

	public interface ICategoryRepo
	{
		Task<List<Category>> GetAllCategories();
		Task<Category> GetCategoryById(Guid id);
		Task Create(Category category);
		Task Update(Category category);
		Task Delete(Guid id);
		Task SaveChanges();
	}

	public interface IBrandRepo
	{
		Task<List<Brand>> GetAllBrands();
		Task<Brand> GetBrandById(Guid id);
		Task Create(Brand brand);
		Task Update(Brand brand);
		Task Delete(Guid id);
		Task SaveChanges();
	}

	public interface IMaterialRepo
	{
		Task<List<Material>> GetAllMaterials();
		Task<Material> GetMaterialById(Guid id);
		Task Create(Material material);
		Task Update(Material material);
		Task Delete(Guid id);
		Task SaveChanges();
	}

	

}
