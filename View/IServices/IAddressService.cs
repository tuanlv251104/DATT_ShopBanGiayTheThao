using DataProcessing.Models;
using View.Servicecs;

namespace View.IServices
{
    public interface IAddressService
    {
        Task<List<Address>> GetAllAddresses();
        Task<Address> GetAddressById(Guid id);
        Task<List<Address>> GetAddressByUserId(Guid userId);
        Task Create(Address address);
        Task Update(Address address, Guid id);
        Task Delete(Guid id);
        // api bên thứ 3 
        Task <List<Province>> GetProvincesAsync();
        Task <List<Districted>> GetDistrictsAsync(int idProvince);
        Task <List<Ward>> GetWardsAsync(int idDistrict);
    }
}
