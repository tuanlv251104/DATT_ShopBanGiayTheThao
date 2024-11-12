using DataProcessing.Models;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using View.IServices;
using View.Servicecs;

namespace View.Controllers
{
    public class CartController : Controller
    {
        private readonly ICartServices _cartServices;
        private readonly IProductDetailService _productDetailService;

        public CartController(ICartServices cartServices , IProductDetailService productDetailService)
        {
            _cartServices = cartServices;
            _productDetailService = productDetailService;
        }
        public ActionResult Index(Guid cartId)
        {
            Guid userId = GetUserIdFromToken();
            // Kiểm tra userId hợp lệ và lấy thông tin giỏ hàng của người dùng
            if (userId != Guid.Empty)
            {
                var cart = _cartServices.GetCartByUserId(userId).Result;

                if (cart != null)
                {
                    return View(_cartServices.GetCartDetailByCartId(cart.Id).Result);
                }
            }
            return View("Error");
        }
        public async Task<IActionResult> AddToCart(CartDetail cartDetail, string productDetailId, int quantity)
        {
            Guid userId = GetUserIdFromToken();

            if (userId != Guid.Empty)
            {
                var cart = await _cartServices.GetCartByUserId(userId);
                if (cart == null)
                {
                    // Tạo giỏ hàng mới nếu chưa có
                    cart = new Cart
                    {
                        Id = Guid.NewGuid(),
                        AccountId = userId
                    };
                    await _cartServices.CreateCart(cart);
                }

                var productDetail = await _productDetailService.GetProductDetailById(productDetailId);
                if (productDetail != null)
                {
                    decimal price = quantity * productDetail.Price;
                    cartDetail = new CartDetail()
                    {
                        Id = Guid.NewGuid(),
                        CartId = cart.Id,
                        ProductDetailId = productDetail.Id,
                        ProductDetail = productDetail,  // Đảm bảo ProductDetail không null
                        Quanlity = quantity,
                        TotalPrice = price
                    };

                    // Gọi phương thức để thêm hoặc cập nhật giỏ hàng chi tiết
                    await _cartServices.CreateCartDetails(cartDetail);

                    return RedirectToAction("Index", new { cartId = cart.Id });
                }
            }

            return View("Error");
        }

        public async Task<IActionResult> DeleteCartDetail(Guid id)
        {
            var cartDetail = await _cartServices.GetCartDetailById(id);
            if (cartDetail != null)
            {
                await _cartServices.Delete(cartDetail.Id);
                return RedirectToAction("Index", new { cartId = cartDetail.Id });
            }
            return View("Error");
        }
        [HttpPost]
        public async Task<IActionResult> UpdateCartDetail(Guid id, CartDetail cartDetail)
        {
            var item = await _cartServices.GetCartDetailById(id);
            if (item != null)
            {
                await _cartServices.UpdateCartDetails(cartDetail,item.Id);
                return RedirectToAction("Index", new { cartId = cartDetail.Id });
            }
            return View("Error");
        }
        private Guid GetUserIdFromToken()
        {
            var token = HttpContext.Session.GetString("AuthToken");
            Guid userId = Guid.Empty;

            if (!string.IsNullOrEmpty(token))
            {
                var tokenHandler = new JwtSecurityTokenHandler();
                var jwtToken = tokenHandler.ReadJwtToken(token);
                var claims = jwtToken.Claims.ToList();
                var userIdClaim = claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;

                // ép kiểu userid từ string sang guid
                if (Guid.TryParse(userIdClaim, out Guid parsedUserId))
                {
                    userId = parsedUserId;
                }
            }

            return userId;
        }

    }
}
