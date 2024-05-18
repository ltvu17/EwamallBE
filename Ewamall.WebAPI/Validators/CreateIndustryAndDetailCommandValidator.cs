using Ewamall.Business.IRepository;
using Ewamall.WebAPI.DTOs;
using FluentValidation;
using FluentValidation.Validators;

namespace Ewamall.WebAPI.Validators
{
    public class CreateIndustryAndDetailCommandValidator : AbstractValidator<CreateIndustryAndDetailCommand>
    {
        public CreateIndustryAndDetailCommandValidator(IIndustryAggrateRepo industryRepo)
        {
            RuleFor(s => s.IndustryName).NotNull().NotEmpty();
            RuleFor(s => s.IsActive).NotNull().NotEmpty();
            RuleFor(s => s.IsLeaf).Must(x => x == false || x == true);
            RuleFor(s => s.Path).NotNull().NotEmpty();
            RuleFor(s => s.RootId).Must(s =>
            {
                if(s != 0)
                {
                    return industryRepo.IsIndustryExist(s);
                }
                else { return true; }
            }).WithMessage("Industry Id is not exist");
            RuleForEach(s => s.ExistDetails).Must(item =>
            {
                return industryRepo.IsDetailExist(item.DetailId);
            }).WithMessage("Detail Id is not exist");
            RuleForEach(s => s.NewDetails).Must(item =>
            {
                return !string.IsNullOrEmpty(item.DetailName) && !string.IsNullOrEmpty(item.DetailDescription);
            }).WithMessage("Detai name and description must not null");
        }
    }
}
