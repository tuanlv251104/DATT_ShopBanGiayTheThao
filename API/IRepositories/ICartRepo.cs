using DataProcessing.Models;

namespace API.IRepositories
{
    public interface ICartRepo
    {

        Task<Cart> GetCartById(Guid id);
        Task<Cart?> GetCartByUserId(Guid userId);
        Task Create (Cart cart);
        Task Update(Guid id, Cart cart);

    }
    public interface ICartDetailsRepo
    {
        Task<List<CartDetail>?> GetCartDetailByCartId(Guid cartId);
        Task<List<CartDetail>?> GetAllCartDetails();
        Task<CartDetail> GetCartDetailById(Guid id);
        Task Create(CartDetail cartDetails);
        Task Update(CartDetail cartDetails, Guid id);
        Task Delete(Guid id);
    }
}
