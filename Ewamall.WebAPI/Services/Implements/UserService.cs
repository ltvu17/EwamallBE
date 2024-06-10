using AutoMapper;
using Ewamall.Business.IRepository;
using Ewamall.Domain.Entities;
using Ewamall.Domain.Shared;
using Ewamall.WebAPI.DTOs;
using Microsoft.AspNetCore.Http;
using System.Security.Principal;

namespace Ewamall.WebAPI.Services.Implements
{
    public class UserService : IUserService
    {
        private readonly ICartRepository _cartRepository;
        private readonly IUserRepo _userRepo;
        private readonly IBaseSetupService<OrderStatus> _orderStatusService;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IShipAddressRepo _shipAddressRepo;
        private readonly IOrderRepo _orderRepo;
        private readonly IMapper _mapper;
        private readonly IEmailSender _emailSender;
        private readonly INotificationService _notificationService;

        public UserService(ICartRepository cartRepository,
            IUserRepo userRepo,
            IBaseSetupService<OrderStatus> orderStatusService,
            IUnitOfWork unitOfWork,
            IShipAddressRepo shipAddressRepo,
            IOrderRepo orderRepo,
            IMapper mapper,
            IEmailSender emailSender,
            INotificationService notificationService)
        {
            _cartRepository = cartRepository;
            _userRepo = userRepo;
            _orderStatusService = orderStatusService;
            _unitOfWork = unitOfWork;
            _shipAddressRepo = shipAddressRepo;
            _orderRepo = orderRepo;
            _mapper = mapper;
            _emailSender = emailSender;
            _notificationService = notificationService;
        }
        public async Task<Result<Cart>> AddToCart(CreateCartCommand request)
        {
            var existCart = await _cartRepository.FindCartByUserIdAndProductId(
                request.UserId,
                request.SellDetailId);
            var cart =  Cart.Create(request.Quantity, request.UserId, request.SellDetailId, existCart);
            if(existCart != null)
            {
                await _cartRepository.UpdateAsync(cart.Value);
            }
            else
            {
                await _cartRepository.AddAsync(cart.Value);
            }
            await _unitOfWork.SaveChangesAsync();
            return cart;
        }

        public async Task<Result<IEnumerable<CartResponse>>> GetAllCartOfUser(int userId)
        {
            var responseCart = _mapper.Map<IEnumerable<CartResponse>>(await _cartRepository.GetCartOfUser(userId)).ToList();
            if(responseCart == null)
            {
                return Result.Failure<IEnumerable<CartResponse>>(new Error("GetAllCartOfUser.GetAll()", "Cart is empty"));
            }
            return responseCart;
        }

        public async Task<Result<Cart>> RemoveCart(int cartId)
        {
            var cart = (await _cartRepository.FindAsync(s => s.Id == cartId, 1, 1)).FirstOrDefault();
            if (cart == null)
            {
                return Result.Failure<Cart>(new Error("UpdateQuantityCart.FindAsync()", "Cart is not found"));
            }

            await _cartRepository.RemoveEntityAsync(cart);
            await _unitOfWork.SaveChangesAsync();
            return cart;
        }

        public async Task<Result<Cart>> UpdateQuantityCart(int cartId, float quantity)
        {
            var cart = (await _cartRepository.FindAsync(s=>s.Id == cartId, 1, 1)).FirstOrDefault();
            if(cart == null)
            {
                return Result.Failure<Cart>(new Error("UpdateQuantityCart.FindAsync()", "Cart is not found"));
            }
            cart.Quantity = (int)quantity;
            await _cartRepository.UpdateAsync(cart);
            await _unitOfWork.SaveChangesAsync();
            return cart;
        }
        //Ship Address service

        public async Task<Result<IEnumerable<ShipAddress>>> GetUserShipAddress(int userId)
        {
            var shipAddresses = (await _shipAddressRepo.FindAsync(s=> s.User.Id == userId, int.MaxValue, 1)).ToList();
            if (shipAddresses.Count == 0)
            {
                return Result.Failure<IEnumerable<ShipAddress>>(new Error("GetUserShipAddress.FindAsync()", "Ship Addresses is empty"));
            }
            return shipAddresses;
        }

        public async Task<Result<ShipAddress>> UpdateUserShipAddress(int shipAddressId, CreateShipAddressCommand request)
        {
            var oldShipAddress = (await _shipAddressRepo.FindAsync(s => s.Id == shipAddressId, 1, 1)).FirstOrDefault();
            if (oldShipAddress == null)
            {
                Result.Failure<ShipAddress>(new Error("UpdateUserShipAddress.FindAsync()", "shipAddressId not found"));
            }
            var newShipAddress = _mapper.Map(request, oldShipAddress);
            await _shipAddressRepo.UpdateAsync(newShipAddress);
            await _unitOfWork.SaveChangesAsync();
            return newShipAddress;
        }

        public async Task<Result<ShipAddress>> CreateUserShipAddress(int userId, CreateShipAddressCommand request)
        {
            var currentUserAddress = await _shipAddressRepo.FindAsync(s => s.UserId == userId, int.MaxValue, 1);
            var result = ShipAddress.Create(request.Name,
                request.Address,
                request.PhoneNumber,
                request.IsDefault,
                userId,
                request.ProvinceId,
                request.DistrictId,
                request.WardId,
                currentUserAddress
                );
            if (result.IsFailure)
            {
                return Result.Failure<ShipAddress>(new Error("GetUserShipAddress.FindAsync()", "Ship Addresses is empty"));
            }
            var shipAddress = result.Value;
            await _shipAddressRepo.AddAsync(shipAddress);   
            await _unitOfWork.SaveChangesAsync();
            return shipAddress;
        }

        public async Task<Result<ShipAddress>> DeleteUserShipAddress(int shipAddressId)
        {
            var oldShipAddress = (await _shipAddressRepo.FindAsync(s => s.Id == shipAddressId, 1, 1)).FirstOrDefault();
            if (oldShipAddress == null)
            {
                Result.Failure<ShipAddress>(new Error("UpdateUserShipAddress.FindAsync()", "shipAddressId not found"));
            }
            await _shipAddressRepo.RemoveEntityAsync(oldShipAddress);
            await _unitOfWork.SaveChangesAsync();
            return oldShipAddress;
        }
        //OrderService
        public async Task<Result<Order>> CreateOrder(int userId, CreateOrderCommand request)
        {
            var result = Order.Create(request.OrderCode,
                request.TotalCost,
                request.ShipCost,
                request.StatusId,
                userId,
                request.ShipAddressId,
                request.VoucherId,
                request.PaymentId);
            if (result.IsFailure)
            {
                return Result.Failure<Order>(new Error("CreateOrder.Create()", "Create order error"));
            }
            var orderDetails = request.CreateOrderDetailCommands;
            var order = result.Value;
            if (orderDetails != null)
            {
                foreach(var orderDetail in orderDetails)
                {
                    order.AddOrderDetail(orderDetail.Quantity, orderDetail.ProductSellDetailId);

                }
            }
            await _orderRepo.AddAsync(order);
            await _unitOfWork.SaveChangesAsync();
            // Mail
            // Tạo message HTML
            var user = await _userRepo.GetByIdAsync(userId);
            string htmlMessage = $@"
                <!DOCTYPE html>
                <html>
                <head>
                    <title>Thông tin mua hàng</title>
                </head>
                <body>
                    <h2>Xin chào, {user.Name}!</h2>
                    <p>EWaMall xin cám ơn vì bạn đã tin tưởng và mua sản phẩm của chúng tôi</p>
                    <p>Chi tiết đơn hàng của bạn bao gồm: Haha hihi hehe </p>
                    <p>Mong mọi người tiếp tục ủng hộ và nếu có vấn đề gì có thể phản hồi email này!</p>
                    <p> Xin cám ơn </>
                </body>
                </html>
                ";
            await _emailSender.SendEmailAsync(user.Account.Email, "Xác thực đăng kí tài khoản EWaMall", htmlMessage);
            await _notificationService.CreateNotification(new CreateNotification()
            {
                Username = user.Name,
                Title = "Bạn đã mua hàng thành công",
                Message = "Cám ơn bạn đã mua sản phẩm của chúng tôi",
                CreatedAt = DateTime.Now,
                NotificationType = "Personal",
                Sender = 1,
                RoleId  = 4
            });
            await _notificationService.CreateNotification(new CreateNotification()
            {
                Username = "Lê Văn Minh Nhật",
                Title = "Đơn hàng mới",
                Message = "Bạn có đơn hàng mới từ: "+ user.Name,
                CreatedAt = DateTime.Now,
                NotificationType = "Personal",
                Sender = 1,
                RoleId = 3
            });
            return result;
        }

        public async Task<Result<Order>> AcceptOrder(int orderId, string statusCode)
        {
            var order = await _orderRepo.GetByIdAsync(orderId);
            if (order == null)
            {
                return Result.Failure<Order>(new Error("CreateOrder.AcceptOrder()", "Not found order"));
            }
            var orderStatus = (await _orderStatusService.GetAllAsync()).Value.FirstOrDefault(x => x.Description.Equals(statusCode,StringComparison.OrdinalIgnoreCase));
            if (orderStatus == null)
            {
                return Result.Failure<Order>(new Error("CreateOrder.AcceptOrder()", "Order status not found"));
            }
            order.ChangeStatus(orderStatus.Id);
            await _orderRepo.UpdateAsync(order);
            await _unitOfWork.SaveChangesAsync();
            return order;
        }

        public async Task<Result<Order>> CancelOrder(int orderId, string cancelReason)
        {
            var order = await _orderRepo.GetByIdAsync(orderId);
            if (order == null)
            {
                return Result.Failure<Order>(new Error("CreateOrder.CancelOrder()", "Not found order"));
            }
            var orderStatus = (await _orderStatusService.GetAllAsync()).Value.FirstOrDefault(x => x.Description.Equals("Cancel", StringComparison.OrdinalIgnoreCase));
            if (orderStatus == null)
            {
                return Result.Failure<Order>(new Error("CreateOrder.CancelOrder()", "Order status not found"));
            }
            order.CancelOrder(orderStatus.Id, cancelReason);
            await _orderRepo.UpdateAsync(order);
            await _unitOfWork.SaveChangesAsync();
            return order;
        }

        public async Task<Result<IEnumerable<Order>>> GetOrderByUserId(int userId)
        {
            var orders = (await _orderRepo.GetOrderByUserId(userId)).ToList();
            if (orders == null) 
            {
                return Result.Failure<IEnumerable<Order>>(new Error("GetOrderByUserId.FindAsync()", "Not found order"));
            }
            return orders;
        }
        public async Task<Result<IEnumerable<Order>>> GetOrderBySellerId(int sellerId)
        {
            var orders = (await _orderRepo.GetOrderBySellerId(sellerId)).ToList();
            if (orders == null)
            {
                return Result.Failure<IEnumerable<Order>>(new Error("GetOrderBySellerId.FindAsync()", "Not found order"));
            }
            return orders;
        }
    } 
}
