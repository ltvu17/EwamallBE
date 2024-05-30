using Ewamall.Business.Entities;
using Ewamall.Domain.Entities;
using Ewamall.Domain.Shared;
using Ewamall.WebAPI.DTOs;

namespace Ewamall.WebAPI.Services
{
    public interface INotificationService
    {
        public Task<Result<IEnumerable<Notification>>> GetAllNotification();
        public Task<Result<Notification>> CreateNotification(CreateVoucherCommand request);
        public Task<Result<Voucher>> UpdateVoucher(int id, CreateVoucherCommand request);
        public Task<Result<Voucher>> DeleteVoucher(int id);
    }
}
