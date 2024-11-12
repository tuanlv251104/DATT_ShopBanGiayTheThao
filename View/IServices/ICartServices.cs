using DataProcessing.Models;

namespace View.IServices
{
    public interface ICartServices
    {
        Task<Cart> GetCartAsync(Guid id);
        Task<Cart> GetCartByUserId(Guid userId);
        Task CreateCart(Cart cart);
        Task Update(Cart cart , Guid id);
        Task <List<CartDetail>> GetAllCartDetails();
        Task<List<CartDetail>> GetCartDetailByCartId(Guid cartId);
        Task<Cart> GetCartDetailById(Guid id);
        Task CreateCartDetails(CartDetail cartDetail);
        Task UpdateCartDetails(CartDetail cartDetail,Guid id);
        Task Delete(Guid id);

    }
}
