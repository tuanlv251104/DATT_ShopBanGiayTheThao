using API.DTO;
using DataProcessing.Models;

namespace API.IRepositories
{
	public interface IColorRepo
	{
		Task<List<Color>> GetAllColor();
		Task<Color> GetColorById(Guid id);
		Task Create(Color color);
		Task Update(Color color);
		Task Delete(Guid id);
		Task SaveChanges();
	}

	public interface IImageRepo
	{
		Task<List<Image>> GetAllImage();
		Task<Image> GetImageById(Guid id);
		Task Create(Image image);
		Task Update(Image image);
		Task Delete(Guid id);
		Task SaveChanges();
	}

	public interface ISelectedImageRepo
	{
		Task<List<SelectedImage>> GetAllSelectedImage();
		Task<SelectedImage> GetSelectedImageById(Guid id);
		Task Create(SelectedImageDTO slimage);
		Task Update(SelectedImageDTO slimage);
		Task Delete(Guid id);
		Task SaveChanges();
	}
}
