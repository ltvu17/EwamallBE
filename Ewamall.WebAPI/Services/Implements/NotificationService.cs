using AutoMapper;
using Ewamall.Business.Entities;
using Ewamall.Business.IRepository;
using Ewamall.DataAccess.Hubs;
using Ewamall.DataAccess.Repository;
using Ewamall.Domain.Entities;
using Ewamall.Domain.IRepository;
using Ewamall.Domain.Shared;
using Ewamall.WebAPI.DTOs;

namespace Ewamall.WebAPI.Services.Implements
{
    public class NotificationService : INotificationService
    {
        private readonly INotificationRepo _notificationRepo;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IEmailSender _emailSender;
        private readonly IMapper _mapper;
        private readonly NotificationHub _notificationHub;

        public NotificationService(INotificationRepo notificationRepo, IUnitOfWork unitOfWork, IEmailSender emailSender, IMapper mapper, NotificationHub notificationHub)
        {
            _notificationRepo = notificationRepo;
            _unitOfWork = unitOfWork;
            _emailSender = emailSender;
            _mapper = mapper;
            _notificationHub = notificationHub;
        }

        public async Task<Result<Notification>> CreateNotification(CreateNotification request)
        {
            var result = Notification.Create(
                request.Username,
                request.Title,
                request.Message,
                request.CreatedAt,
                request.NotificationType,
                request.Sender,
                request.RoleId
                );
            if (result.IsFailure)
            {
                return Result.Failure<Notification>(new Error("Create notification", "Fail to create notification"));
            }
            var notification = result.Value;
            await _notificationRepo.AddAsync(notification);

            if(await _unitOfWork.SaveChangesAsyncResult() > 0 )
            {
                SendToClientAsync(notification);
            }
            return result;
        }

        public async Task<Result<Notification>> DeleteNotification(int id)
        {
            var notification = await _notificationRepo.GetByIdAsync(id);
            if (notification is null)
            {
                return Result.Failure<Notification>(new Error("DeleteNotification.GetById", "Notification not found"));
            }
            await _notificationRepo.RemoveAsync(id);
            await _unitOfWork.SaveChangesAsync();
            return notification;
        }

        public async Task<Result<IEnumerable<Notification>>> GetAllNotification()
        {
            Result<IEnumerable<Notification>> result = (await _notificationRepo.GetAllAsync()).ToList();
            if (result.IsFailure)
            {
                return Result.Failure<IEnumerable<Notification>>(new Error("IEnumerable<Notification>.GetAll()", "Fail to load notification"));
            }
            return result;
        }

        public async Task<Result<IEnumerable<Notification>>> GetAllNotificationByUserName(string userName)
        {
            Result<IEnumerable<Notification>> result = (await _notificationRepo.GetAllNotificationByUserName(userName)).ToList();
            if (result.IsFailure)
            {
                return Result.Failure<IEnumerable<Notification>>(new Error("IEnumerable<Notification>.GetAll()", "Fail to load notification of user"));
            }
            return result;
        }

        public async Task<Result<Notification>> UpdateNotification(int id, CreateNotification request)
        {
            var notification = await _notificationRepo.GetByIdAsync(id);
            var updateNotification = _mapper.Map(request, notification);
            if (notification is null)
            {
                return Result.Failure<Notification>(new Error("UpdateNotification.GetById", "Notification not found"));
            }

            await _notificationRepo.UpdateAsync(updateNotification);
            if (await _unitOfWork.SaveChangesAsyncResult() > 0)
            {
                SendToClientAsync(notification);
            }
            return updateNotification;
        }
        private async void SendToClientAsync(Notification notification)
        {       
            if (notification.NotificationType == "All")
            {
                await _notificationHub.SendNotificationToAll(notification.Title, notification.Message);
            }
            else if (notification.NotificationType == "Personal")
            {
                await _notificationHub.SendNotificationToClient(notification.Title, notification.Message, notification.Username.ToString());
            }
            else if (notification.NotificationType == "Group")
            {
                await _notificationHub.SendNotificationToGroup(notification.Title, notification.Message, notification.RoleId);
            }           
        }
    }
}
