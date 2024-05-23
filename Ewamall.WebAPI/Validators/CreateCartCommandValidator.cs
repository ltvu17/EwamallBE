using Ewamall.Business.IRepository;
using Ewamall.Domain.IRepository;
using Ewamall.WebAPI.DTOs;
using FluentValidation;

namespace Ewamall.WebAPI.Validators
{
    public class CreateCartCommandValidator : AbstractValidator<CreateCartCommand>
    {
        public CreateCartCommandValidator(IUserRepo userRepo, IProductRepo productRepo)
        {
            RuleFor(s =>s.Quantity).Must(s =>s > 0);
            RuleFor(s => s.UserId).Must(s =>
            {
                var user = userRepo.GetByIdAsync(s).GetAwaiter().GetResult();
                if(user == null)
                {
                    return false;
                }
                return true;
            });
        }
    }
}
