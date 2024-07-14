using Ewamall.Domain.Entities;
using Ewamall.Domain.Shared;
using Ewamall.WebAPI.DTOs;

namespace Ewamall.WebAPI.Services
{
    public interface IUserService
    {
        public Task<Result<IEnumerable<CartResponse>>> GetAllCartOfUser(int userId);
        public Task<Result<Cart>> AddToCart(CreateCartCommand request);
        public Task<Result<Cart>> UpdateQuantityCart(int cartId, float quantity);
        public Task<Result<Cart>> RemoveCart(int cartId);
        // ShipAddress Service
        public Task<Result<IEnumerable<ShipAddress>>> GetUserShipAddress(int userId);
        public Task<Result<ShipAddress>> UpdateUserShipAddress(int shipAddressId, CreateShipAddressCommand request);
        public Task<Result<ShipAddress>> CreateUserShipAddress(int userId, CreateShipAddressCommand request);
        public Task<Result<ShipAddress>> DeleteUserShipAddress(int shipAddressId);
        // Order Service
        public Task<Result<string>> CreateOrder(int userId, CreateOrderCommand request);
        public Task<Result<Order>> AcceptOrder(int orderId, string statusCode);
        public Task<Result<Order>> CancelOrder(int orderId, string cancelReason);
        public Task<Result<IEnumerable<Order>>> GetOrderByUserId(int userId);
        public Task<Result<IEnumerable<Order>>> GetOrderBySellerId(int sellerId);
    }
}
