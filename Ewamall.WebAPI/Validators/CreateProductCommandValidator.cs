using Ewamall.Business.IRepository;
using Ewamall.WebAPI.DTOs;
using FluentValidation;

namespace Ewamall.WebAPI.Validators
{
    public class CreateProductCommandValidator : AbstractValidator<CreateProductCommand>
    {
        public CreateProductCommandValidator(IIndustryAggrateRepo industryRepo)
        {
            RuleForEach(s => s.ProductSellDetails).Must((item) =>
            {
                return industryRepo.IsDetailExist(item.DetailId);
            }).WithMessage("The detail Id is not exist");
            RuleFor(s => s.ProductName).NotNull().NotEmpty();
            RuleFor(s => s.ProductSellDetails).NotNull().NotEmpty();
            RuleFor(s => s.ProductDescription).NotNull().NotEmpty();
            RuleFor(s => s.ProductSellCommand).NotNull();
        }
    }
}
