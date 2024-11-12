using DataProcessing.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using View.IServices;

namespace View.Controllers
{
    public class CustomerController : Controller
    {
        private readonly IProductDetailService _productDetailService;
        private readonly ISizeServices _sizeServices;
        private readonly IColorServices _colorServices;
        private readonly IProductServices _productServices;
        private readonly ICategoryServices _categoryServices;
        private readonly ISoleServices _soleServices;
        private readonly IBrandServices _brandServices;
        private readonly IMaterialServices _materialServices;


        public CustomerController(IProductDetailService productDetailService,
            ISizeServices sizeServices, IColorServices colorServices,
            IProductServices productServices, ICategoryServices categoryServices,
            ISoleServices soleServices, IBrandServices brandServices, IMaterialServices materialServices)
        {
            _productDetailService = productDetailService;
            _sizeServices = sizeServices;
            _colorServices = colorServices;
            _productServices = productServices;
            _brandServices = brandServices;
            _categoryServices = categoryServices;
            _soleServices = soleServices;
            _materialServices = materialServices;
        }
        public IActionResult Index()
        {
            // Kiểm tra xem session có tồn tại không
            var token = HttpContext.Session.GetString("AuthToken");
            if (string.IsNullOrEmpty(token))
            {
                return RedirectToAction("Login", "Account");
            }
            return View();
        }
        public async Task<IActionResult> ViewProductAsync() 
        {
            ViewData["ColorId"] = new SelectList((await _colorServices.GetAllColors()).Where(x => x.Status), "Id", "Name");
            ViewData["SizeId"] = new SelectList((await _sizeServices.GetAllSizes()).Where(x => x.Status), "Id", "Value");
            ViewData["ProductId"] = new SelectList(await _productServices.GetAllProducts(), "Id", "Name");
            ViewData["BrandId"] = new SelectList(await _brandServices.GetAllBrands(), "Id", "Name");
            ViewData["CategoryId"] = new SelectList(await _categoryServices.GetAllCategories(), "Id", "Name");
            ViewData["SoleId"] = new SelectList(await _soleServices.GetAllSoles(), "Id", "TypeName");
            ViewData["MaterialId"] = new SelectList(await _materialServices.GetAllMaterials(), "Id", "Name");

            // Lấy dữ liệu productDetail
            var viewContext = await _productDetailService.GetAllProductDetail();

            // Kiểm tra xem dữ liệu có hợp lệ không (loại bỏ null hoặc kiểm tra dữ liệu)
            if (viewContext == null || !viewContext.Any())
            {
                // Nếu không có dữ liệu hoặc null, trả về một thông báo cho view
                ViewBag.Message = "No product details found.";
                return View(new List<ProductDetail>()); // Trả về một danh sách rỗng
            }

            // Trả về view với dữ liệu hợp lệ
            return View(viewContext);
        }

    }
}
