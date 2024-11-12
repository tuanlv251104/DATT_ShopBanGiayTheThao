using DataProcessing.Models;

namespace API.IRepositories
{
    public interface IShippingUnitRepos
    {
        Task<List<ShippingUnit>> GetAllShippingUnit();
        Task<ShippingUnit> GetShippingUnitById(Guid id);
        Task create(ShippingUnit shippingUnit);
        Task update(ShippingUnit shippingUnit);
        Task delete(Guid id);
        Task SaveChanges();
    }
}
