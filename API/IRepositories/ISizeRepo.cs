using DataProcessing.Models;

namespace API.IRepositories
{
	public interface ISizeRepo
	{
		Task<List<Size>> GetAllSize();
		Task<Size> GetSizeById(Guid id);
		Task Create(Size size);
		Task Update(Size size);
		Task Delete(Guid id);
		Task SaveChanges();
	}
}
