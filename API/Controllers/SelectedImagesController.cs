using API.DTO;
using API.IRepositories;
using DataProcessing.Models;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace API.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class SelectedImagesController : ControllerBase
	{
		private readonly ISelectedImageRepo _selectedImageRepo;
        public SelectedImagesController(ISelectedImageRepo selectedImageRepo)
        {
            _selectedImageRepo = selectedImageRepo;
        }
        // GET: api/<SelectedImagesController>
        [HttpGet]
		public async Task<ActionResult<List<SelectedImage>>> Get()
		{
			return await _selectedImageRepo.GetAllSelectedImage();
		}

		// GET api/<SelectedImagesController>/5
		[HttpGet("{id}")]
		public async Task<ActionResult<SelectedImage>> Get(Guid id)
		{
			return await _selectedImageRepo.GetSelectedImageById(id);
		}

		// POST api/<SelectedImagesController>
		[HttpPost]
		public async Task<IActionResult> Post(SelectedImageDTO image)
		{
			try
			{
				await _selectedImageRepo.Create(image);
			}catch (Exception ex)
			{
				return Problem(ex.Message);
			}
			await _selectedImageRepo.SaveChanges();

			return Content("Success!");
		}

		// PUT api/<SelectedImagesController>/5
		[HttpPut("{id}")]
		public async Task<IActionResult> Put(SelectedImageDTO image)
		{
			try
			{
				await _selectedImageRepo.Update(image);
			}catch(Exception ex)
			{
				return Problem(ex.Message);
			}

			await _selectedImageRepo.SaveChanges();

			return Content("Success!");
		}

		// DELETE api/<SelectedImagesController>/5
		[HttpDelete("{id}")]
		public async Task<IActionResult> Delete(Guid id)
		{
			try
			{
				await _selectedImageRepo.Delete(id);
			}catch (Exception ex)
			{
				return Problem(ex.Message);
			}

			await _selectedImageRepo.SaveChanges();

			return Content("Success!");
		}
	}
}
