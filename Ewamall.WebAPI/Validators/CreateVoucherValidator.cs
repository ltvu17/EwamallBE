using Ewamall.WebAPI.DTOs;
using FluentValidation;

namespace Ewamall.WebAPI.Validators
{
    public class CreateVoucherValidator : AbstractValidator<CreateVoucherCommand>
    {
        public CreateVoucherValidator() {
            RuleFor(s => s.VoucherCode).NotNull().NotEmpty();
            RuleFor(s => s.Type).NotNull().NotEmpty();
            RuleFor(s => s.Name).NotNull().NotEmpty();
            RuleFor(s => s.Description).NotNull().NotEmpty();
            RuleFor(s => s.Discount).NotNull().NotEmpty();
            RuleFor(s => s.StartDate).NotNull().NotEmpty();
            RuleFor(s => s.EndDate)
            .NotNull()
            .NotEmpty()
            .GreaterThan(s => s.StartDate).WithMessage("EndDate must be greater than StartDate.");
            RuleFor(s => s.MinOrder).NotNull().NotEmpty();
            RuleFor(s => s.MaxDiscount).NotNull().NotEmpty();
        }
    }
}
