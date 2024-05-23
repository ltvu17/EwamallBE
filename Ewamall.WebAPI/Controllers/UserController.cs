using Ewamall.WebAPI.DTOs;
using Ewamall.WebAPI.Services;
using Microsoft.AspNetCore.Mvc;

namespace Ewamall.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : Controller
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }
        // Cart Service
        [HttpGet("GetAllCart/{userId}")]
        public async Task<IActionResult> GetCartOfUser(int userId)
        {
            var result = await _userService.GetAllCartOfUser(userId);
            if (result.IsFailure)
            {
                return BadRequest(result.Error);
            }
            return Ok(result.Value);
        }
        [HttpPost("AddToCart")]
        public async Task<IActionResult> AddToCart(CreateCartCommand request)
        {
            var result = await _userService.AddToCart(request);
            if(result.IsFailure)
            {
                return BadRequest(result.Error);
            }
            return Ok(result.Value);
        }
        [HttpPost("UpdateQuantityCart/{cartId}")]
        public async Task<IActionResult> UpdateQuantityCart(int cartId, float quantity)
        {
            var result = await _userService.UpdateQuantityCart(cartId , quantity);
            if (result.IsFailure)
            {
                return BadRequest(result.Error);
            }
            return Ok(result.Value);
        }
        [HttpDelete("RemoveCart/{cartId}")]
        public async Task<IActionResult> RemoveCart(int cartId)
        {
            var result = await _userService.RemoveCart(cartId);
            if (result.IsFailure)
            {
                return BadRequest(result.Error);
            }
            return Ok(result.Value);
        }
        //ShipAddress Service
        [HttpGet("GetShipAddress/{userId}")]
        public async Task<IActionResult> GetShipAddress(int userId)
        {
            var result = await _userService.GetUserShipAddress(userId);
            if (result.IsFailure)
            {
                return BadRequest(result.Error);
            }
            return Ok(result.Value);
        }
        [HttpPost("CreateShipAddress/{userId}")]
        public async Task<IActionResult> CreateShipAddress(int userId, CreateShipAddressCommand request)
        {
            var result = await _userService.CreateUserShipAddress(userId, request);
            if (result.IsFailure)
            {
                return BadRequest(result.Error);
            }
            return Ok(result.Value);
        }
        [HttpPut("UpdateShipAddress/{shipAddressId}")]
        public async Task<IActionResult> UpdateShipAddress(int shipAddressId, CreateShipAddressCommand request)
        {
            var result = await _userService.UpdateUserShipAddress(shipAddressId, request);
            if (result.IsFailure)
            {
                return BadRequest(result.Error);
            }
            return Ok(result.Value);
        }
        [HttpDelete("DeleteShipAddress/{shipAddressId}")]
        public async Task<IActionResult> DeleteShipAddress(int shipAddressId)
        {
            var result = await _userService.DeleteUserShipAddress(shipAddressId);
            if (result.IsFailure)
            {
                return BadRequest(result.Error);
            }
            return Ok(result.Value);
        }
        //Order Service
        [HttpPost("CreateOrder/{userId}")]
        public async Task<IActionResult> CreateOrder(int userId, CreateOrderCommand request)
        {
            var result = await _userService.CreateOrder(userId, request);
            if (result.IsFailure)
            {
                return BadRequest(result.Error);
            }
            return Ok(result.Value);
        }
        [HttpPost("AcceptOrder/{orderId}")]
        public async Task<IActionResult> AcceptOrder(int orderId, string statusCode)
        {
            var result = await _userService.AcceptOrder(orderId, statusCode);
            if (result.IsFailure)
            {
                return BadRequest(result.Error);
            } 
            return Ok(result.Value);
        }
        [HttpPost("CancelOrder/{orderId}")]
        public async Task<IActionResult> CancelOrder(int orderId, string cancelReason)
        {
            var result = await _userService.CancelOrder(orderId, cancelReason);
            if (result.IsFailure)
            {
                return BadRequest(result.Error);
            }
            return Ok(result.Value);
        }
        [HttpGet("GetOrderByUserId/{userId}")]
        public async Task<IActionResult> GetOrderByUserId(int userId)
        {
            var result = await _userService.GetOrderByUserId(userId);
            if (result.IsFailure)
            {
                return BadRequest(result.Error);
            }
            return Ok(result.Value);
        }
        [HttpGet("GetOrderBySellerId/{sellerId}")]
        public async Task<IActionResult> GetOrderBySellerId(int sellerId)
        {
            var result = await _userService.GetOrderBySellerId(sellerId);
            if (result.IsFailure)
            {
                return BadRequest(result.Error);
            }
            return Ok(result.Value);
        }
    }
}
