using API.IRepositories;
using DataProcessing.Models;
using Microsoft.AspNetCore.Mvc;
using System.Security.Cryptography.X509Certificates;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AddressController : ControllerBase
    {
        private readonly IAddressRepo _repo;

        public AddressController(IAddressRepo repo)
        {
            _repo = repo;
        }
        // GET: api/<AddressController>
        [HttpGet("GetAllAddress")]
        public async Task<IActionResult> GetAllAddress()
        {
            var lstAddress= await _repo.GetAllAddresses();
            if (lstAddress == null)
            {
                return NotFound();
            }
            return Ok(lstAddress);
        }

        // GET api/<AddressController>/5
        [HttpGet("GetAddressById")]
        public async Task<IActionResult> GetAddressById(Guid id)
        {
            var address = await _repo.GetAddressById(id);
            if (address == null)
            {
                return NotFound();
            }
            return Ok(address);
        }
        [HttpGet("GetAddressByUserId")]
        public async Task<IActionResult> GetAddressByUserId(Guid userId)
        {
            var result = await _repo.GetAddressByUserId(userId);
            if (result == null)
            {
                return NotFound();
            }
            return Ok(result);
        }
        // POST api/<AddressController>
        [HttpPost("CreateAddress")]
        public async Task<IActionResult> Create(Address address)
        {
            await _repo.Create(address);
            return Ok(address);
        }          

        // PUT api/<AddressController>/5
        [HttpPut("UpdateAddress")]
        public async Task<IActionResult> Update(Address address , Guid id)
        {
            await _repo.Update(address, id);
            return Ok(address);
        }
        
        // DELETE api/<AddressController>/5
        [HttpDelete("Delete")]
        public async Task<IActionResult> Delete(Guid id)
        {
            await _repo.Delete(id);
            return Ok();
        }
    }
}
