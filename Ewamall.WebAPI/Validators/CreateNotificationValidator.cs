using Ewamall.Business.IRepository;
using Ewamall.DataAccess.Repository;
using Ewamall.WebAPI.DTOs;
using FluentValidation;

namespace Ewamall.WebAPI.Validators
{
    public class CreateNotificationValidator : AbstractValidator<CreateNotification>
    {
        public CreateNotificationValidator(IUserRepo userRepo)
        {
            RuleFor(s => s.Title).NotNull().NotEmpty();
            RuleFor(s => s.Message).NotNull().NotEmpty();
            RuleFor(s => s.CreatedAt).NotNull().NotEmpty();
            RuleFor(s => s.Sender)
                .Must(userId => userRepo.IsUserExist(userId))
                .WithMessage("User not exists.");
            RuleFor(s => s.NotificationType).NotNull().NotEmpty()
                .Must(type => type == "All" || type == "Group" || type == "Personal")
                .WithMessage("NotificationType must be either 'All', 'Group', or 'Personal'.");
        }
    }
}
