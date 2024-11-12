using API.Data;
using API.IRepositories;
using DataProcessing.Models;
using Microsoft.EntityFrameworkCore;

namespace API.Repositories
{
	public class ProductRepos : IProductRepo
	{
		private readonly ApplicationDbContext _context;
		public ProductRepos(ApplicationDbContext applicationDbContext)
		{
			_context = applicationDbContext;
		}

		public async Task Create(Product product)
		{
			if(await GetProductById(product.Id) != null) throw new DuplicateWaitObjectException($"Product : {product.Id} is existed!");
			await _context.Products.AddAsync(product);
		}

		public async Task Delete(Guid id)
		{
			var product = await GetProductById(id);
			if (product == null) throw new KeyNotFoundException("Not found this Id!");
			_context.Products.Remove(product);
		}

		public async Task<List<Product>> GetAllProduct()
		{
			return await _context.Products.Include(p => p.Brand)
				.Include(p => p.Category)
				.Include(p => p.Material)
				.Include(p => p.Sole)
				.ToListAsync();
		}

		public async Task<Product?> GetProductById(Guid id)
		{
			return await _context.Products.Where(p=>p.Id == id).Include(p => p.Brand)
				.Include(p => p.Category)
				.Include(p => p.Material)
				.Include(p => p.Sole).FirstOrDefaultAsync();
		}

		public async Task SaveChanges()
		{
			await _context.SaveChangesAsync();
		}

		public async Task Update(Product product)
		{
			if (await GetProductById(product.Id) == null) throw new KeyNotFoundException("Not found this Id!");
			_context.Entry(product).State = EntityState.Modified;
		}
	}

	//Sole Repository
	public class SoleRepos : ISoleRepo
	{
		private readonly ApplicationDbContext _context;
		public SoleRepos(ApplicationDbContext applicationDbContext)
		{
			_context = applicationDbContext;
		}

		public async Task Create(Sole sole)
		{
			if (await GetSoleById(sole.Id) != null) throw new DuplicateWaitObjectException($"Sole : {sole.Id} is existed!");
			await _context.Soles.AddAsync(sole);
		}

		public async Task Delete(Guid id)
		{
			var sole = await GetSoleById(id);
			if (sole == null) throw new KeyNotFoundException("Not found this sole!");
			if (_context.Products.Where(p => p.SoleId == id).Any()) throw new Exception("This sole has used for some product!");
			_context.Soles.Remove(sole);
		}

		public async Task<List<Sole>> GetAllSole()
		{
			return await _context.Soles.ToListAsync();
		}

		public async Task<Sole> GetSoleById(Guid id)
		{
			return await _context.Soles.FindAsync(id);
		}

		public async Task SaveChanges()
		{
			await _context.SaveChangesAsync();
		}

		public async Task Update(Sole sole)
		{
			if (await GetSoleById(sole.Id) == null) throw new KeyNotFoundException("Not found this sole!");
			_context.Entry(sole).State = EntityState.Modified;
		}
	}

	//Category Repository
	public class CategoryRepos : ICategoryRepo
	{
		private readonly ApplicationDbContext _context;
		public CategoryRepos(ApplicationDbContext applicationDbContext)
		{
			_context = applicationDbContext;
		}

		public async Task Create(Category data)
		{
			if (await GetCategoryById(data.Id) != null) throw new DuplicateWaitObjectException($"Category : {data.Id} is existed!");
			await _context.Categories.AddAsync(data);
		}

		public async Task Delete(Guid id)
		{
			var data = await GetCategoryById(id);
			if (data == null) throw new KeyNotFoundException("Not found this category!");
			if (_context.Products.Where(p => p.CategoryId == id).Any()) throw new Exception("This category has used for some product!");
			_context.Categories.Remove(data);
		}

		public async Task<List<Category>> GetAllCategories()
		{
			return await _context.Categories.ToListAsync();
		}

		public async Task<Category> GetCategoryById(Guid id)
		{
			return await _context.Categories.FindAsync(id);
		}

		public async Task SaveChanges()
		{
			await _context.SaveChangesAsync();
		}

		public async Task Update(Category data)
		{
			if (await GetCategoryById(data.Id) == null) throw new KeyNotFoundException("Not found this category!");
			_context.Entry(data).State = EntityState.Modified;
		}
	}

	//Brand Repository
	public class BrandRepos : IBrandRepo
	{
		private readonly ApplicationDbContext _context;
		public BrandRepos(ApplicationDbContext applicationDbContext)
		{
			_context = applicationDbContext;
		}

		public async Task Create(Brand data)
		{
			if (await GetBrandById(data.Id) != null) throw new DuplicateWaitObjectException($"Brand : {data.Id} is existed!");
			await _context.Brands.AddAsync(data);
		}

		public async Task Delete(Guid id)
		{
			var data = await GetBrandById(id);
			if (data == null) throw new KeyNotFoundException("Not found this brand!");
			if (_context.Products.Where(p => p.BrandId == data.Id).Any()) throw new Exception("This brand has used for some product!");
			_context.Brands.Remove(data);
		}

		public async Task<List<Brand>> GetAllBrands()
		{
			return await _context.Brands.ToListAsync();
		}

		public async Task<Brand> GetBrandById(Guid id)
		{
			return await _context.Brands.FindAsync(id);
		}

		public async Task SaveChanges()
		{
			await _context.SaveChangesAsync();
		}

		public async Task Update(Brand data)
		{
			if (await GetBrandById(data.Id) == null) throw new KeyNotFoundException("Not found this brand!");
			_context.Entry(data).State = EntityState.Modified;
		}
	}

	//Material Repository
	public class MaterialRepos : IMaterialRepo
	{
		private readonly ApplicationDbContext _context;
		public MaterialRepos(ApplicationDbContext applicationDbContext)
		{
			_context = applicationDbContext;
		}

		public async Task Create(Material data)
		{
			if (await GetMaterialById(data.Id) != null) throw new DuplicateWaitObjectException($"Material : {data.Id} is existed!");
			await _context.Materials.AddAsync(data);
		}

		public async Task Delete(Guid id)
		{
			var data = await GetMaterialById(id);
			if (data == null) throw new KeyNotFoundException("Not found this material!");
			if (_context.Products.Where(p => p.MaterialId == id).Any()) throw new Exception("This material has used for some product!");
			_context.Materials.Remove(data);
		}

		public async Task<List<Material>> GetAllMaterials()
		{
			return await _context.Materials.ToListAsync();
		}

		public async Task<Material> GetMaterialById(Guid id)
		{
			return await _context.Materials.FindAsync(id);
		}

		public async Task SaveChanges()
		{
			await _context.SaveChangesAsync();
		}

		public async Task Update(Material data)
		{
			if (await GetMaterialById(data.Id) == null) throw new KeyNotFoundException("Not found this material!");
			_context.Entry(data).State = EntityState.Modified;
		}
	}
}
