using Ewamall.Business.IRepository;
using Ewamall.DataAccess.Repository;
using Ewamall.WebAPI.DTOs;
using FluentValidation;

namespace Ewamall.WebAPI.Validators
{
    public class CreateSellerValidator : AbstractValidator<CreateSeller>
    {
        public CreateSellerValidator(ISellerRepo sellerRepo) {
            RuleFor(s => s.ShopName).NotNull().NotEmpty();
            RuleFor(s => s.Address).NotNull().NotEmpty();
            RuleFor(s => s.ProvinceId).NotNull().NotEmpty();
            RuleFor(s => s.DistrictId).NotNull().NotEmpty();
            RuleFor(s => s.WardId).NotNull().NotEmpty();
            RuleFor(s => s.Description).NotNull().NotEmpty();
            RuleFor(s => s.Email)
                .NotNull().WithMessage("Email is required.")
                .NotEmpty().WithMessage("Email cannot be empty.")
                .EmailAddress().WithMessage("Invalid email address format.")
                .Must(email => !sellerRepo.IsEmailExist(email))
                .WithMessage("Email already exists.");
            RuleFor(s => s.PhoneNumber)
                .NotNull().WithMessage("Phone number is required.")
                .NotEmpty().WithMessage("Phone number cannot be empty.")
                .Matches(@"^0\d{9}$").WithMessage("Invalid phone number format. Phone number must start with 0 and have 10 digits.")
                .Must(phone => !sellerRepo.IsPhoneExist(phone))
                .WithMessage("Phone number is existed!");
        }
    }
}
