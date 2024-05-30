using AutoMapper;
using Ewamall.Business.Entities;
using Ewamall.Business.IRepository;
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

        public NotificationService(INotificationRepo notificationRepo, IUnitOfWork unitOfWork, IEmailSender emailSender, IMapper mapper)
        {
            _notificationRepo = notificationRepo;
            _unitOfWork = unitOfWork;
            _emailSender = emailSender;
            _mapper = mapper;
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
                request.Receiver,
                request.RoleId
                );
            if (result.IsFailure)
            {
                return Result.Failure<Notification>(new Error("Create notification", "Fail to create notification"));
            }
            var notification = result.Value;
            await _notificationRepo.AddAsync(notification);
            await _unitOfWork.SaveChangesAsync();
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

        public async Task<Result<IEnumerable<Notification>>> GetAllNotificationByUserId(int userId)
        {
            Result<IEnumerable<Notification>> result = (await _notificationRepo.GetAllNotificationByUserId(userId)).ToList();
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
            await _unitOfWork.SaveChangesAsync();
            return updateNotification;
        }
    }
}
