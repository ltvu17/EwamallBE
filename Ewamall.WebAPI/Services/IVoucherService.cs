using Ewamall.Domain.Entities;
using Ewamall.Domain.Shared;
using Ewamall.WebAPI.DTOs;

namespace Ewamall.WebAPI.Services
{
    public interface IVoucherService
    {
        public Task<Result<IEnumerable<Voucher>>> GetAllVoucher();
        public Task<Result<Voucher>> CreateVoucher(int staffId, CreateVoucherCommand request);
        public Task<Result<Voucher>> UpdateVoucher(int id, CreateVoucherCommand request);
        public Task<Result<Voucher>> DeleteVoucher(int id);
    }
}
