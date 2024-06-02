using Ewamall.Business.Entities;
using Ewamall.Domain.Entities;
using Ewamall.Domain.Shared;
using Ewamall.WebAPI.DTOs;

namespace Ewamall.WebAPI.Services
{
    public interface INotificationService
    {
        public Task<Result<IEnumerable<Notification>>> GetAllNotification();
        public Task<Result<IEnumerable<Notification>>> GetAllNotificationByUserName(string userName);
        public Task<Result<Notification>> CreateNotification(CreateNotification request);
        public Task<Result<Notification>> UpdateNotification(int id, CreateNotification request);
        public Task<Result<Notification>> DeleteNotification(int id);
    }
}
