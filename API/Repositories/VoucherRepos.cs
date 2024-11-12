using API.Data;
using API.IRepositories;
using DataProcessing.Models;
using Microsoft.EntityFrameworkCore;
using System.Drawing;

namespace API.Repositories
{
    public class VoucherRepos : IVoucherRepos
    {
        private readonly ApplicationDbContext _context;
        public VoucherRepos(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task create(Voucher voucher)
        {
            if (await GetById(voucher.Id) != null) throw new DuplicateWaitObjectException($"Voucher : {voucher.Id} is existed!");

            // Kiểm tra tính hợp lệ của voucher trước khi thêm
            ValidateVoucher(voucher);

            await _context.Vouchers.AddAsync(voucher);
            await _context.SaveChangesAsync();
        }

        public async Task delete(Guid id)
        {
            var voucher = await GetById(id);
            if (voucher == null) throw new KeyNotFoundException("Not found this voucher!");
            if (_context.Orders.Any(o => o.VoucherId == id))
                throw new Exception("This voucher is applied to one or more invoices and cannot be deleted!");
            if (voucher.Status)
                throw new Exception("Cannot delete an active voucher!");

            _context.Vouchers.Remove(voucher);
            await _context.SaveChangesAsync();
        }

        public async Task<List<Voucher>> GetAll()
        {
            return await _context.Vouchers.ToListAsync();
        }

        public async Task<Voucher> GetById(Guid id)
        {
            return await _context.Vouchers.FindAsync(id);
        }

        public async Task SaveChanges()
        {
            await _context.SaveChangesAsync();
        }

        public async Task update(Voucher voucher)
        {
            if (await GetById(voucher.Id) == null)
                throw new KeyNotFoundException("Not found this voucher!");

            // Kiểm tra tính hợp lệ của voucher trước khi cập nhật
            ValidateVoucher(voucher);

            _context.Entry(voucher).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        // Phương thức kiểm tra tính hợp lệ của voucher
        private void ValidateVoucher(Voucher voucher)
        {
            if (voucher.DiscountType == "Percent")
            {
                if (voucher.DiscountPercent == null || voucher.DiscountPercent < 0 || voucher.DiscountPercent > 100)
                    throw new ArgumentException("Giá trị phần trăm giảm giá phải từ 0 đến 100 khi loại giảm giá là 'Percent'.");

                if (voucher.DiscountAmount != null)
                    throw new ArgumentException("Không thể thiết lập giá trị giảm trực tiếp khi loại giảm giá là 'Percent'.");
            }
            else if (voucher.DiscountType == "Amount")
            {
                if (voucher.DiscountAmount == null || voucher.DiscountAmount < 0)
                    throw new ArgumentException("Giá trị giảm trực tiếp phải là một giá trị dương khi loại giảm giá là 'Amount'.");

                if (voucher.DiscountPercent != null)
                    throw new ArgumentException("Không thể thiết lập giá trị phần trăm khi loại giảm giá là 'Amount'.");
            }
            else
            {
                throw new ArgumentException("Loại giảm giá phải là 'Percent' hoặc 'Amount'.");
            }

            if (voucher.StartDate < DateTime.Now)
                throw new ArgumentException("Ngày bắt đầu không thể là quá khứ.");

            if (voucher.EndDate <= voucher.StartDate)
                throw new ArgumentException("Ngày kết thúc phải sau ngày bắt đầu.");
            if (voucher.DiscountAmount.HasValue && voucher.DiscountPercent.HasValue)
            {
                throw new ArgumentException("Chỉ được điền vào một trong hai giá trị giảm giá: phần trăm hoặc tiền mặt, không được điền cả hai.");
            }  

        }
    }

}
