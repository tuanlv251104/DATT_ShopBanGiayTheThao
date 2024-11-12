using DataProcessing.Models;

namespace API.IRepositories
{
    public interface IAddressRepo
    {
        Task<List<Address>> GetAllAddresses();
        Task<Address> GetAddressById(Guid id);
        Task<List<Address>> GetAddressByUserId(Guid userId);
        Task Create(Address address);
        Task Update(Address address,Guid id);
        Task Delete(Guid id);
        //Task SetAsDefault(Guid id); 
    }
}
