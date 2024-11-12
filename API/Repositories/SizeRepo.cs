using API.Data;
using API.IRepositories;
using DataProcessing.Models;
using Microsoft.EntityFrameworkCore;

namespace API.Repositories
{
	public class SizeRepo : ISizeRepo
	{
		private readonly ApplicationDbContext _context;
        public SizeRepo(ApplicationDbContext sizeRepo)
        {
            _context = sizeRepo;
        }
        public async Task Create(Size size)
		{
			if (await GetSizeById(size.Id) != null) throw new DuplicateWaitObjectException($"Size : {size.Id} is existed!");
			await _context.Sizes.AddAsync(size);
		}

		public async Task Delete(Guid id)
		{
			var size = await GetSizeById(id);
			if (size == null) throw new KeyNotFoundException("Not found this size!");
			if (_context.ProductDetails.Where(p => p.SizeId == id).Any()) throw new Exception("This size has used for some product!");
			_context.Sizes.Remove(size);
		}

		public async Task<List<Size>> GetAllSize()
		{
			return await _context.Sizes.ToListAsync();
		}

		public async Task<Size> GetSizeById(Guid id)
		{
			return await _context.Sizes.FindAsync(id);
		}

		public async Task SaveChanges()
		{
			await _context.SaveChangesAsync();
		}

		public async Task Update(Size size)
		{
			if (await GetSizeById(size.Id) == null) throw new KeyNotFoundException("Not found this size!");
			_context.Entry(size).State = EntityState.Modified;
		}
	}
}
