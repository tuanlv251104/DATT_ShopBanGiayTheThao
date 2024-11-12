using API.DTO;
using API.IRepositories;
using DataProcessing.Models;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace API.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class ImagesController : ControllerBase
	{
		private readonly IImageRepo _imageRepo;
        public ImagesController(IImageRepo imageRepo)
        {
            _imageRepo = imageRepo;
        }

        // GET: api/<ImagesController>
        [HttpGet]
		public async Task<ActionResult<List<Image>>> GetAllImage()
		{
			return await _imageRepo.GetAllImage();
		}

		// GET api/<ImagesController>/5
		[HttpGet("{id}")]
		public async Task<ActionResult<Image>> GetColorById(Guid id)
		{
			return await _imageRepo.GetImageById(id);
		}

		// POST api/<ImagesController>
		[HttpPost]
		public async Task<IActionResult> Post(Image image)
		{
			try
			{
				await _imageRepo.Create(image);
			}catch (Exception ex)
			{
				return Problem(ex.Message);
			}
			_imageRepo.SaveChanges();

			return Content("Success!");
		}

		// PUT api/<ImagesController>/5
		[HttpPut("{id}")]
		public async Task<IActionResult> Put(Image image)
		{
			try
			{
				await _imageRepo.Update(image);
			}catch (Exception ex)
			{
				return Problem(ex.Message);
			}

			_imageRepo.SaveChanges();

			return Content("Success!");
		}

		// DELETE api/<ImagesController>/5
		[HttpDelete("{id}")]
		public async Task<IActionResult> Delete(Guid id)
		{
			try
			{
				await _imageRepo.Delete(id);
			}catch (Exception ex)
			{
				return Problem(ex.Message);
			}
			_imageRepo.SaveChanges();

			return Content("Success!");
		}
	}
}
