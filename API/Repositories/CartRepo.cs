using API.Data;
using API.IRepositories;
using Data.ViewModels;
using DataProcessing.Models;
using Microsoft.EntityFrameworkCore;

namespace API.Repositories
{
    public class CartRepo : ICartRepo
    {
        private readonly ApplicationDbContext _context;

        public CartRepo(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task Create (Cart cart)
        {
            bool exists = await _context.Carts.AnyAsync(c => c.Id == cart.Id);
            if (exists)
            {
                throw new DuplicateWaitObjectException("This cartDetails is existed!");
            }
            await _context.Carts.AddAsync(cart);
            await _context.SaveChangesAsync();
        }
        public async Task<Cart?> GetCartByUserId(Guid userId)
        {
            var cart = await _context.Carts.Where(c => c.AccountId == userId).FirstOrDefaultAsync();
            return cart;
        } 

        public async Task<Cart> GetCartById(Guid id)
        {
            var item = await _context.Carts.FindAsync(id);
            return item;
        }

        public async Task Update(Guid id, Cart cart)
        {
            var updateItem = await _context.Carts.FindAsync(id);
            if (updateItem != null)
            {
                updateItem.TotalPrice=cart.TotalPrice;
            }
            _context.Carts.Update(updateItem);
            await _context.SaveChangesAsync();      
        }
    }
    public class CartDetailsRepo :ICartDetailsRepo
    {
        private readonly ICartDetailsRepo _repo;
        private readonly ApplicationDbContext _context;

        public CartDetailsRepo(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task Create(CartDetail cartDetails)
        {
            bool exists = await _context.CartDetails.AnyAsync(c => c.Id == cartDetails.Id);
            if (exists)
            {
                throw new DuplicateWaitObjectException("This cartDetails is existed!");
            }
            await _context.CartDetails.AddAsync(cartDetails);
            await _context.SaveChangesAsync();
        }

        public async Task Delete(Guid id)
        {
            var deleteItem = await _context.CartDetails.FindAsync(id);
            if (deleteItem == null)
            {
                throw new KeyNotFoundException($"CartDetail with Id {id} not found.");
            }
            _context.CartDetails.Remove(deleteItem);
            await _context.SaveChangesAsync();
        }

        public async Task<List<CartDetail>?> GetAllCartDetails()
        {
            var lstCartDetails = await _context.CartDetails.ToListAsync();
            return lstCartDetails; 
        }

        public async Task<List<CartDetail>?> GetCartDetailByCartId(Guid cartId)
        {
            return await _context.CartDetails
                .Where(cd=>cd.CartId==cartId)
                .Include(cd => cd.ProductDetail)
                .ToListAsync();
        }

        public async Task<CartDetail> GetCartDetailById(Guid id)
        {
            return await _context.CartDetails.FindAsync(id);
        }

        public async Task Update(CartDetail cartDetails, Guid id)
        {
            var updateItem = await _context.CartDetails.FindAsync(id);  
            if(cartDetails!=null)
            {
                updateItem.Quanlity = cartDetails.Quanlity;
                updateItem.TotalPrice = cartDetails.TotalPrice;
            }
            _context.CartDetails.Update(updateItem);
            await _context.SaveChangesAsync();
        }
    }
}
