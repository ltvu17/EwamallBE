using Ewamall.Business.IRepository;
using Ewamall.WebAPI.DTOs;
using FluentValidation;

namespace Ewamall.WebAPI.Validators
{
    public class LoginValidator : AbstractValidator<Authentication>
    {
        public LoginValidator(IAccountRepo accountRepo) {
            RuleFor(s => s.Email)
                .NotNull().WithMessage("Email is required.")
                .NotEmpty().WithMessage("Email cannot be empty.")
                .EmailAddress().WithMessage("Invalid email address format.")
                .Must(email => accountRepo.IsEmailExist(email))
                .WithMessage("Email not exists.");
            RuleFor(s => s.Password).NotNull().NotEmpty();
        }
    }
}
