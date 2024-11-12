using DataProcessing.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using View.IServices;
using View.Servicecs;

namespace View.Controllers
{
    public class AddressController : Controller
    {
        private readonly IAddressService _addressService;

        public AddressController(IAddressService service)
        {
            _addressService = service;
        }
        // getall
        public async Task<IActionResult> Index()
        {
            var lstAddress = await _addressService.GetAllAddresses();
            return View(lstAddress);
        }
        [HttpGet]
        public async Task<IActionResult> Create(int? provinceId, int? districtId)
        {
            // Lấy danh sách Tỉnh
            var provinces = await _addressService.GetProvincesAsync();
            ViewBag.Provinces = provinces;
            ViewBag.SelectedProvinceId = provinceId;

            // Nếu có provinceId, lấy danh sách Huyện tương ứng
            if (provinceId.HasValue)
            {
                var districts = await _addressService.GetDistrictsAsync(provinceId.Value);
                ViewBag.Districts = districts;
                ViewBag.SelectedDistrictId = districtId; // Lưu districtId đã chọn
            }
            else
            {
                ViewBag.Districts = new List<Districted>(); // Để trống nếu chưa chọn Tỉnh
            }

            // Nếu có districtId, lấy danh sách Xã tương ứng
            if (districtId.HasValue)
            {
                var wards = await _addressService.GetWardsAsync(districtId.Value);
                ViewBag.Wards = wards;
            }
            else
            {
                ViewBag.Wards = new List<Ward>();
            }

            return View(new Address());
        }



        [HttpPost]
        public async Task<IActionResult> Create(Address address)
        {
            if (ModelState.IsValid)
            {
                await _addressService.Create(address);
                return RedirectToAction("Index");
            }

            // Nếu form không hợp lệ, lấy lại danh sách Tỉnh, Huyện và Xã
            ViewBag.Provinces = await _addressService.GetProvincesAsync();
            ViewBag.Districts = new List<Districted>();
            ViewBag.Wards = new List<Ward>();

            return View(address);
        }
    }
}
