using API.Data;
using API.IRepositories;
using DataProcessing.Models;
using Microsoft.EntityFrameworkCore;

namespace API.Repositories
{
    public class AddressRepo : IAddressRepo
    {
        private readonly ApplicationDbContext _context;

        public AddressRepo(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task Create(Address address)
        {
            var newAddress = new Address()
            {
                Id = Guid.NewGuid(),
                RecipientName = address.RecipientName,
                PhoneNumber = address.PhoneNumber,
                AddressDetail = address.AddressDetail,
                City = address.City,
                District = address.District,
                Commune = address.Commune,
                AccountId = address.AccountId,
                IsDeleted = address.IsDeleted,
            };
             _context.Addresses.Add(newAddress);
            await _context.SaveChangesAsync();
        }

        public async Task Delete(Guid id)
        {
            var deleteItem = await _context.Addresses.FindAsync(id);
            if (deleteItem != null)
            {
                _context.Addresses.Remove(deleteItem);
            }
            await _context.SaveChangesAsync();
        }

        public async Task<Address> GetAddressById(Guid id)
        {
            return await _context.Addresses.FindAsync(id);    
        }

        public async Task<List<Address>> GetAllAddresses()
        {
            return await _context.Addresses.ToListAsync();
        }

        //public async Task SetAsDefault(Guid id)
        //{                   
        //}
        public async Task<List<Address>> GetAddressByUserId(Guid userId)
        {
            var user = await _context.Accounts.FindAsync(userId);
            var lstAddressUserId = await _context.Addresses.Where(u=>u.AccountId==userId).ToListAsync();
            
            return lstAddressUserId;
                
        }
        public async Task Update(Address address, Guid id)
        {
            var updateItem = await _context.Addresses.FindAsync(id);
            if (updateItem != null)
            {
                updateItem.RecipientName = address.RecipientName;
                updateItem.PhoneNumber = address.PhoneNumber;
                updateItem.AddressDetail = address.AddressDetail;
                updateItem.City = address.City;
                updateItem.District = address.District;
                updateItem.Commune = address.Commune;
                updateItem.IsDeleted = address.IsDeleted;

                _context.Addresses.Update(updateItem);
                await _context.SaveChangesAsync();
            }
        }
    }
}
