using DataProcessing.Models;

namespace API.IRepositories
{
    public interface IVoucherRepos
    {
        Task<List<Voucher>> GetAll();
        Task<Voucher> GetById(Guid id);
        Task create(Voucher voucher);
        Task update(Voucher voucher);
        Task delete(Guid id);
        Task SaveChanges();
    }
}
