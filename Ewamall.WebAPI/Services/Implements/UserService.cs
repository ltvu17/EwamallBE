using AutoMapper;
using Ewamall.Business.IRepository;
using Ewamall.DataAccess.Repository;
using Ewamall.Domain.Entities;
using Ewamall.Domain.Shared;
using Ewamall.WebAPI.DTOs;
using Microsoft.AspNetCore.Http;
using System.Collections;
using System.Security.Principal;

namespace Ewamall.WebAPI.Services.Implements
{
    public class UserService : IUserService
    {
        private readonly ICartRepository _cartRepository;
        private readonly IUserRepo _userRepo;
        private readonly IProductSellDetailRepo _productSellDetailsRepo;
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
            IProductSellDetailRepo productSellDetailsRepo,
            IUnitOfWork unitOfWork,
            IShipAddressRepo shipAddressRepo,
            IOrderRepo orderRepo,
            IMapper mapper,
            IEmailSender emailSender,
            INotificationService notificationService)
        {
            _cartRepository = cartRepository;
            _userRepo = userRepo;
            _productSellDetailsRepo = productSellDetailsRepo;
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
        public async Task<Result<string>> CreateOrder(int userId, CreateOrderCommand request)
        {
            var result = Order.Create(request.OrderCode,
                request.TotalCost,
                request.ShipCost,
                request.StatusId,
                userId,
                request.ShipAddressId,
                request.VoucherId,
                request.PaymentId,
                (DateTime)request.OrderDate);
            if (result.IsFailure)
            {
                return Result.Failure<string>(new Error("CreateOrder.Create()", "Create order error"));
            }
            List<OrderResponseMail> orderResponse = new List<OrderResponseMail>();
            var orderDetails = request.CreateOrderDetailCommands;
            var order = result.Value;
            if (orderDetails != null)
            {
                foreach(var orderDetail in orderDetails)
                {
                    order.AddOrderDetail(orderDetail.Quantity, orderDetail.ProductSellDetailId);
                    var productInfor = await _productSellDetailsRepo.GetByIdAsync(orderDetail.ProductSellDetailId);
                    var orderResponseForList = new OrderResponseMail
                    {
                        ProductName = productInfor.Product.ProductName,
                        Quantity = orderDetail.Quantity,
                        Price = productInfor.Price,
                    };
                    orderResponse.Add(orderResponseForList);
                }
            }
            await _orderRepo.AddAsync(order);
            await _unitOfWork.SaveChangesAsync();
            // Mail
            // Tạo message HTML
            var user = await _userRepo.GetByIdAsync(userId);
            var shop = await _userRepo.GetSellerByProduct(request.CreateOrderDetailCommands.FirstOrDefault().ProductSellDetailId);
            /*            string htmlMessage = $@" <div style=""max-width: 600px; margin: 20px auto; padding: 20px; background-color: #fff; border: 1px solid #ddd; border-radius: 5px;"">
                    <h2 style=""background-color: #E9BB45; color: #242058; text-align: center; padding: 10px 0;"">Giao dịch thành công</h2>
                    <p>Đơn hàng của bạn đã được nhận và hiện đang được xử lý. Chi tiết đơn hàng của bạn được hiển thị bên dưới để bạn tham khảo:</p>
                    <h3>Order: #{request.OrderCode}</h3>
                    <table style=""width: 100%; border-collapse: collapse;"">
                        <tr>
                            <th style=""border: 1px solid #ddd; padding: 8px; text-align: left;"">Sản phẩm</th>
                            <th style=""border: 1px solid #ddd; padding: 8px; text-align: left;"">Số lượng</th>
                            <th style=""border: 1px solid #ddd; padding: 8px; text-align: left;"">Giá tiền</th>
                        </tr>
                        <tr>
                            <td style=""border: 1px solid #ddd; padding: 8px;"">Tên sản phẩm</td>
                            <td style=""border: 1px solid #ddd; padding: 8px;"">1</td>
                            <td style=""border: 1px solid #ddd; padding: 8px;"">$99.99</td>
                        </tr>
                        <tr>
                            <td colspan=""2"" style=""border: 1px solid #ddd; padding: 8px; text-align: right;"">Phương thức thanh toán:</td>
                            <td style=""border: 1px solid #ddd; padding: 8px;"">Ship code</td>
                        </tr>
                        <tr>
                            <td colspan=""2"" style=""border: 1px solid #ddd; padding: 8px; text-align: right; font-weight: bold;"">Order Total:</td>
                            <td style=""border: 1px solid #ddd; padding: 8px;"">{request.TotalCost} </td>
                        </tr>
                    </table>
                    <h3>Customer details</h3>
                    <p>Email: {user.Account.Email}<br>Sdt: {user.Account.PhoneNumber}</p>
                    <h3>Địa chỉ ship</h3>
                    <p>{user.Address}</p>
                </div>";*/
            string orderDetailsHtml = "";
foreach (var orderDetail in orderResponse)
{
    orderDetailsHtml += $@"
        <tr>
            <td style=""border: 1px solid #ddd; padding: 8px;"">{orderDetail.ProductName}</td>
            <td style=""border: 1px solid #ddd; padding: 8px;"">{orderDetail.Quantity}</td>
            <td style=""border: 1px solid #ddd; padding: 8px;"">{orderDetail.Price}</td>
        </tr>";
}
            string htmlMessage = $@" <div style=""max-width: 600px; margin: 20px auto; padding: 20px; background-color: #fff; border: 1px solid #ddd; border-radius: 5px;"">
    <h2 style=""background-color: #E9BB45; color: #242058; text-align: center; padding: 10px 0;"">Giao dịch thành công</h2>
    <p>Đơn hàng của bạn đã được nhận và hiện đang được xử lý. Chi tiết đơn hàng của bạn được hiển thị bên dưới để bạn tham khảo:</p>
    <h3>Order: #{request.OrderCode}</h3>
    <p>{DateTime.Parse(request.OrderDate.ToString()).ToShortDateString()}</p>
    <table style=""width: 100%; border-collapse: collapse;"">
        <tr>
            <th style=""border: 1px solid #ddd; padding: 8px; text-align: left;"">Sản phẩm</th>
            <th style=""border: 1px solid #ddd; padding: 8px; text-align: left;"">Số lượng</th>
            <th style=""border: 1px solid #ddd; padding: 8px; text-align: left;"">Giá tiền</th>
        </tr>
        {orderDetailsHtml}
        <tr>
            <td colspan=""2"" style=""border: 1px solid #ddd; padding: 8px; text-align: right;"">Phương thức thanh toán:</td>
            <td style=""border: 1px solid #ddd; padding: 8px;"">Ship code</td>
        </tr>
        <tr>
            <td colspan=""2"" style=""border: 1px solid #ddd; padding: 8px; text-align: right; font-weight: bold;"">Order Total:</td>
            <td style=""border: 1px solid #ddd; padding: 8px;"">{request.TotalCost} </td>
        </tr>
    </table>
     <div style=""display: flex"">
                    <div>
                <h3>Customer details</h3>
                <p>Email: {user.Account.Email}<br>Sdt: {user.Account.PhoneNumber}</p>
                <h3>Địa chỉ ship</h3>
                <p>{user.Address}</p>
                    </div>                  
                </div>
</div>";

            await _emailSender.SendEmailAsync(user.Account.Email, "Giao dịch thành công", htmlMessage);
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
/*            await _notificationService.CreateNotification(new CreateNotification()
            {
                Username = "Lê Văn Minh Nhật",
                Title = "Đơn hàng mới",
                Message = "Bạn có đơn hàng mới từ: "+ user.Name,
                CreatedAt = DateTime.Now,
                NotificationType = "Personal",
                Sender = 1,
                RoleId = 3
            });*/
            var returnOrderQr = $@"https://img.vietqr.io/image/mbbank-0377899819-compact2.jpg?amount={order.TotalCost}&addInfo={shop.ShopName}&accountName=Le%20Van%20Minh%20Nhat";
            return returnOrderQr;
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
