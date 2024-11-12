
using DataProcessing.Models;

namespace View.IServices
{
	public interface IColorServices
	{
		Task<IEnumerable<Color>?> GetAllColors();
		Task<Color?> GetColorById(Guid id);
		Task Create(Color color);
		Task Update(Color color);
		Task Delete(Guid id);
	}

	public interface IImageServices
	{
		Task<IEnumerable<Image>?> GetAllImages();
		Task<IEnumerable<Image>?> GetImagesByColorId(Guid id);
		Task<Image?> GetImageById(Guid id);
		Task Create(Image image);
		Task Update(Image image);
		Task Delete(Guid id);
	}

	public interface ISelectedImageServices
	{
		Task<IEnumerable<SelectedImage>?> GetAllSelectedImages();
		Task<IEnumerable<SelectedImage>?> GetSelectedImagesByProductId(Guid id, Guid colorId);
		Task<SelectedImage?> GetSelectedImageById(Guid id);
		Task Create(SelectedImage selectedImage);
		Task Update(SelectedImage selectedImage);
		Task Delete(Guid id);
	}
}
