using API.IRepositories;
using Data.ViewModels;
using DataProcessing.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Runtime.CompilerServices;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountControllercs : ControllerBase
    {
        private readonly IAccountRepo _repo;

        public AccountControllercs(IAccountRepo repo)
        {
            _repo = repo;
        }
        [HttpPost("Login")]
        public async Task<IActionResult> Login(SignInModel model)
        {
            var result = await _repo.SignInAsync(model);
            if (string.IsNullOrEmpty(result))
            {
                return Unauthorized();
                }
            return Ok(result);
        }
        [HttpPost("Register")]
        public async Task<IActionResult> Register(SignUpModel model)
        {
            try
            {
                var result = await _repo.SignUpAsync(model);
                if (result.Succeeded)
                {
                    return Ok(result);
                }
                return BadRequest(result);
            }
            catch (Exception ex)
            {
                return BadRequest( $"{ex.Message}");
            }
        }
        [HttpPost("LogOut")]
        public async Task Logout()
        {
            await _repo.SignOutAsync();
        }
        [Authorize]
        [HttpPost("Create-Employee")]
        public async Task<IActionResult> CreateEmployee(CreateAccountModelcs models)

        {
            try
            {
                var result = await _repo.CreateEmployee(models);
                if (result.Succeeded)
                {
                    return Ok(result);
                }
                return BadRequest(result);
            }
            catch (Exception ex)
            {
                return BadRequest($"{ex.Message}");
            }
        }
        [HttpGet("Get-All-Employee")]
        public async Task<IActionResult> GetAllEmployee()
        {
            try
            {
                var result = await _repo.GetAllEmployee();
                return Ok(result);
            }
            catch(Exception ex) 
            {
                return BadRequest($"{ex.Message}");
            }
        }
        [HttpGet("GetById")]
        public async Task<IActionResult > GetById(Guid idAccount)
        {
            try
            {
                var result = await _repo.GetById(idAccount);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest($"{ex.Message}");
            }
        }
        [HttpPut("Update/{id}")]
        public async Task<IActionResult> UpdateUser(Guid id, [FromBody] ApplicationUser user)
        {
            if (id != user.Id)
            {
                return BadRequest("User ID mismatch.");
            }

            try
            {
                var updatedUser = await _repo.Update(user);
                return Ok(updatedUser);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpDelete("Delete")]
        public async Task<IActionResult> Delete(Guid idAccount)
        {
            try
            {
                var result = await _repo.Delete(idAccount);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest($"{ex.Message}");
            }
        }
        [HttpPost("Create-Customer")]
        public async Task<IActionResult> CreateCustomer(CreateAccountModelcs model)
        {
            try
            {
                var result = await _repo.CreateCustomer(model);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest($"{ex.Message}");
            }
        }
 
        [HttpGet("Get-All-Customer")]
        public async Task<IActionResult> GetAllCustomer()
        {
            try
            {
                var result = await _repo.GetAllCustomer();
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest($"{ex.Message}");
            }
        }




    }
}
