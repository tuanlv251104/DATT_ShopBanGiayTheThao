using API.Data;
using API.IRepositories;
using DataProcessing.Models;
using Microsoft.EntityFrameworkCore;

namespace API.Repositories
{
    public class ShippingUnitRepos : IShippingUnitRepos
    {
        private readonly ApplicationDbContext _context;

        public ShippingUnitRepos(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task create(ShippingUnit shippingUnit)
        {
            await _context.ShippingUnits.AddAsync(shippingUnit);
        }

        public async Task delete(Guid id)
        {
            var hasOrders = _context.Orders.Any(o => o.ShippingUnitID == id);
            if (!hasOrders)
            {
                var deleteItem = await GetShippingUnitById(id);
                if (deleteItem != null)
                {
                    _context.ShippingUnits.Remove(deleteItem);
                }

            }
                                                                
        }

        public async Task<List<ShippingUnit>> GetAllShippingUnit()
        {
            return await _context.ShippingUnits.ToListAsync();
        }

        public async Task<ShippingUnit> GetShippingUnitById(Guid id)
        {
            return await _context.ShippingUnits.FindAsync(id);
        }

        public async Task SaveChanges()
        {
            await _context.SaveChangesAsync();
        }

        public async Task update(ShippingUnit shippingUnit)
        {
             var updateItem = await GetShippingUnitById(shippingUnit.ShippingUnitID);
            updateItem.Status = shippingUnit.Status;
            updateItem.Address = shippingUnit.Address;
            updateItem.Email = shippingUnit.Email;
            updateItem.Phone = shippingUnit.Phone;
            updateItem.Website = shippingUnit.Website;
            updateItem.Name = shippingUnit.Name;

            _context.ShippingUnits.Update(updateItem);
        }
    }
}
