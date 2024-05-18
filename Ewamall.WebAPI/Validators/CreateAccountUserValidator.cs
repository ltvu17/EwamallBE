using Ewamall.Business.IRepository;
using Ewamall.WebAPI.DTOs;
using FluentValidation;

namespace Ewamall.WebAPI.Validators
{
    public class CreateAccountUserValidator : AbstractValidator<CreateUserAccount>
    {
        public CreateAccountUserValidator(IAccountRepo accountRepo) {
            RuleFor(s => s.Email)
                .NotNull().WithMessage("Email is required.")
                .NotEmpty().WithMessage("Email cannot be empty.")
                .EmailAddress().WithMessage("Invalid email address format.")
                .Must(email => !accountRepo.IsEmailExist(email))
                .WithMessage("Email already exists.");
            RuleFor(s => s.Password).NotEmpty().NotNull();
            RuleFor(s => s.PhoneNumber)
                .NotNull().WithMessage("Phone number is required.")
                .NotEmpty().WithMessage("Phone number cannot be empty.")
                .Matches(@"^0\d{9}$").WithMessage("Invalid phone number format. Phone number must start with 0 and have 10 digits.")
                .Must(phone => !accountRepo.IsPhoneExist(phone))
                .WithMessage("Phone number is existed!")
                ;

            RuleFor(s => s.UserInformation.Name).NotNull().NotEmpty();
            RuleFor(s => s.UserInformation.DateOfBirth).NotNull().NotEmpty();
            RuleFor(s => s.UserInformation.Gender).Must(s=>s == Business.Enums.GenderEnum.MALE || s == Business.Enums.GenderEnum.FEMALE);
            RuleFor(s => s.UserInformation.Address).NotNull().NotEmpty();
        }
    }
}
