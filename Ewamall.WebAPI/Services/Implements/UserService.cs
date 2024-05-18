using AutoMapper;
using Ewamall.Business.IRepository;
using Ewamall.Domain.Entities;
using Ewamall.Domain.Shared;
using Ewamall.WebAPI.DTOs;

namespace Ewamall.WebAPI.Services.Implements
{
    public class UserService : IUserService
    {
        private readonly ICartRepository _cartRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IShipAddressRepo _shipAddressRepo;
        private readonly IMapper _mapper;

        public UserService(ICartRepository cartRepository,
            IUnitOfWork unitOfWork,
            IShipAddressRepo shipAddressRepo,
            IMapper mapper)
        {
            _cartRepository = cartRepository;
            _unitOfWork = unitOfWork;
            _shipAddressRepo = shipAddressRepo;
            _mapper = mapper;
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
            await _cartRepository.AddAsync(cart.Value);
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
    }
}
