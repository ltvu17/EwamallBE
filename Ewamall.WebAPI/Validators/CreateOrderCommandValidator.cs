using Ewamall.Business.IRepository;
using Ewamall.Domain.Entities;
using Ewamall.Domain.IRepository;
using Ewamall.WebAPI.DTOs;
using FluentValidation;

namespace Ewamall.WebAPI.Validators
{
    public class CreateOrderCommandValidator : AbstractValidator<CreateOrderCommand>
    {
        public CreateOrderCommandValidator(IShipAddressRepo shipAddressRepo, IVoucherRepo voucherRepo, IBaseRepo<OrderStatus> orderStatusRepo)
        {
            RuleFor(s => s.TotalCost).Must(s => s > 0);
            RuleFor(s => s.ShipCost).Must(s => s > 0);
            RuleFor(s => s.OrderCode).NotEmpty().NotNull();
            RuleForEach(s => s.CreateOrderDetailCommands).Must(s => s.Quantity > 0);
            RuleFor(s => s.ShipAddressId).Must( s =>
            {
                var shipAddress = shipAddressRepo.GetByIdAsync(s).GetAwaiter().GetResult();
                if(shipAddress == null)
                {
                    return false;
                }
                return true;
            }).WithMessage("ShipAddress Id is not exist");
            RuleFor(s => s.VoucherId).Must(s =>
            {
                if(s == 0)
                {
                    return true;
                }
                else
                {
                    var voucher = voucherRepo.GetByIdAsync(s).GetAwaiter().GetResult(); 
                    if(voucher == null)
                    {
                        return false;
                    }
                    return true;
                }
            });
            RuleFor(s => s.StatusId).Must(s =>
            {
                var status = orderStatusRepo.GetByIdAsync(s).GetAwaiter().GetResult();
                if (status == null)
                {
                    return false;
                }
                return true;
            });
        }
    }
}
