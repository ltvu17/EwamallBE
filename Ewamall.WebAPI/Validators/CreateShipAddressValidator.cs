using Ewamall.WebAPI.DTOs;
using FluentValidation;

namespace Ewamall.WebAPI.Validators
{
    public class CreateShipAddressValidator : AbstractValidator<CreateShipAddressCommand>
    {
        public CreateShipAddressValidator()
        {
            RuleFor(s=>s.PhoneNumber).NotEmpty().NotNull().WithMessage("Phone number is required.")
                .NotEmpty().WithMessage("Phone number cannot be empty.")
                .Matches(@"^0\d{9}$").WithMessage("Invalid phone number format. Phone number must start with 0 and have 10 digits.");
            RuleFor(s => s.Name).NotEmpty().NotNull();
            RuleFor(s => s.ProvinceId).Must(s=>s >0);
            RuleFor(s => s.DistrictId).Must(s => s > 0);
            RuleFor(s => s.WardId).Must(s => s > 0);
            RuleFor(s => s.Address).NotEmpty().NotNull();
        }
    }
}
