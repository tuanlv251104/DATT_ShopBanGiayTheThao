using DataProcessing.Models;

namespace View.IServices
{
    public interface IVoucherServices
    {
        Task<IEnumerable<Voucher>?> GetAllVouchers();
        Task<Voucher?> GetVoucherById(Guid id);
        Task Create(Voucher voucher);
        Task Update(Voucher voucher);
        Task Delete(Guid id);
        Task<List<ApplicationUser>> GetAllAccounts();
    }
}
