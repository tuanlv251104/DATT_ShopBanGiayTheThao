using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DataProcessing.Models;
using View.IServices;
using View.Servicecs;
using View.ViewModel;

namespace View.Controllers
{
    public class VoucherController : Controller
    {
        private readonly IVoucherServices _voucherService;

        public VoucherController(IVoucherServices voucherService)
        {
            _voucherService = voucherService;
        }

        // GET: Vouchers
        public async Task<IActionResult> Index(string searchQuery, DateTime? fromDate, DateTime? toDate, string type, string status, int currentPage = 1, int rowsPerPage = 10)
        {
            var vouchers = await _voucherService.GetAllVouchers();

            if (!string.IsNullOrEmpty(searchQuery))
            {
                searchQuery = searchQuery.ToLower();
                vouchers = vouchers.Where(v => v.Name.ToLower().Contains(searchQuery) || v.VoucherCode.ToLower().Contains(searchQuery)).ToList();
            }

            if (fromDate.HasValue)
            {
                vouchers = vouchers.Where(v => v.StartDate >= fromDate.Value).ToList();
            }

            if (toDate.HasValue)
            {
                vouchers = vouchers.Where(v => v.EndDate <= toDate.Value).ToList();
            }

            if (!string.IsNullOrEmpty(type))
            {
                vouchers = vouchers.Where(v => v.Type == type).ToList();
            }

            if (!string.IsNullOrEmpty(status))
            {
                bool isActive = status == "true";
                vouchers = vouchers.Where(v => v.Status == isActive).ToList();
            }

            // Phân trang
            int totalRecords = vouchers.Count();
            int totalPages = (int)Math.Ceiling((double)totalRecords / rowsPerPage);
            var paginatedVouchers = vouchers.Skip((currentPage - 1) * rowsPerPage).Take(rowsPerPage).ToList();

            // Truyền thông tin phân trang vào ViewBag
            ViewBag.CurrentPage = currentPage;
            ViewBag.TotalPages = totalPages;
            ViewBag.RowsPerPage = rowsPerPage;

            ViewData["CurrentSearchQuery"] = searchQuery;
            ViewData["CurrentFromDate"] = fromDate?.ToString("yyyy-MM-dd");
            ViewData["CurrentToDate"] = toDate?.ToString("yyyy-MM-dd");
            ViewData["CurrentType"] = type;
            ViewData["CurrentStatus"] = status;

            return View(paginatedVouchers);
        }


        // GET: Vouchers/Applicable
        public async Task<IActionResult> Applicable()
        {
            var vouchers = await _voucherService.GetAllVouchers();

            if (vouchers == null)
            {
                return Problem("Entity set 'Voucher' is null");
            }

            var applicableVouchers = vouchers
                .Where(v => v.Status
                    && v.Stock > 0
                    && (v.StartDate == null || v.StartDate <= DateTime.Now)  // Nếu có StartDate, nó phải nhỏ hơn hoặc bằng hiện tại
                    && (v.EndDate == null || v.EndDate >= DateTime.Now))    // Nếu có EndDate, nó phải lớn hơn hoặc bằng hiện tại
                .OrderByDescending(v => v.DiscountType == "Amount" ? v.DiscountAmount : v.DiscountPercent)
                .ToList();


            return View(applicableVouchers);
        }



        // GET: Vouchers/Details/5
        public async Task<IActionResult> Details(Guid id)
        {
            var voucher = await _voucherService.GetVoucherById(id);
            if (voucher == null)
            {
                return NotFound();
            }

            return View(voucher);
        }

        // GET: Vouchers/Create
        public async Task<IActionResult> Create()
        {
            var model = new VoucherViewModel
            {
                Voucher = new Voucher(),
                Accounts = await _voucherService.GetAllAccounts(), // Lấy danh sách các tài khoản từ dịch vụ
                CurrentPage = 1,
                TotalPages = 1
            };
            return View(model);
        }

        // POST: Vouchers/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(VoucherViewModel model)
        {
            if (ModelState.IsValid)
            {
                model.Voucher.Id = Guid.NewGuid();
                try
                {
                    await _voucherService.Create(model.Voucher);
                    return RedirectToAction(nameof(Index));
                }
                catch (ArgumentException ex)
                {
                    ModelState.AddModelError(string.Empty, ex.Message);
                }
            }
            // Reload danh sách Accounts khi xảy ra lỗi
            model.Accounts = await _voucherService.GetAllAccounts();
            return View(model);
        }




        // GET: Vouchers/Edit/5
        public async Task<IActionResult> Edit(Guid id)
        {
            var voucher = await _voucherService.GetVoucherById(id);
            if (voucher == null)
            {
                return NotFound();
            }
            return View(voucher);
        }

        // POST: Vouchers/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Id,VoucherCode,Name,DiscountType,DiscountAmount,DiscountPercent,MaxDiscountValue,Stock,Condition,StartDate,EndDate,Type,Status,AccountId")] Voucher voucher)
        {
            if (id != voucher.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _voucherService.Update(voucher);
                    return RedirectToAction(nameof(Index));
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (await _voucherService.GetVoucherById(id) == null)
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                catch (ArgumentException ex)
                {
                    ModelState.AddModelError(string.Empty, ex.Message);
                }
            }
            return View(voucher);
        }

        // GET: Vouchers/Delete/5
        public async Task<IActionResult> Delete(Guid id)
        {
            var voucher = await _voucherService.GetVoucherById(id);
            if (voucher == null)
            {
                return NotFound();
            }

            return View(voucher);
        }

        // POST: Vouchers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            try
            {
                await _voucherService.Delete(id);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
                return View(await _voucherService.GetVoucherById(id));
            }
            return RedirectToAction(nameof(Index));
        }
    }
}
