using Ewamall.Domain.Entities;
using Ewamall.WebAPI.Services;
using Microsoft.AspNetCore.Mvc;

namespace Ewamall.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BaseSetupController : Controller
    {
        private readonly IBaseSetupService<Payment> _paymentService;
        private readonly IBaseSetupService<OrderStatus> _orderService;

        public BaseSetupController(IBaseSetupService<Payment> paymentService, IBaseSetupService<OrderStatus> orderService)
        {
            _paymentService = paymentService;
            _orderService = orderService;
        }
        [HttpGet("GetPayment")]
        public async Task<IActionResult> GetPayment() 
        {
            var result = await _paymentService.GetAllAsync();
            if (result.IsFailure)
            {
                return BadRequest(result.Error);
            }
            return Ok(result.Value);
        }
        [HttpPost("CreatePayment")]
        public async Task<IActionResult> CreatePayment(Payment request)
        {
            var result = await _paymentService.AddAsync(request);
            if (result.IsFailure)
            {
                return BadRequest(result.Error);
            }
            return Ok(result.Value);
        }
        [HttpPut("UpdatePayment/{id}")]
        public async Task<IActionResult> UpdatePayment(int id, Payment request)
        {
            var result = await _paymentService.UpdateAsync(id, request);
            if (result.IsFailure)
            {
                return BadRequest(result.Error);
            }
            return Ok(result.Value);
        }
        [HttpDelete("DeletePayment/{id}")]
        public async Task<IActionResult> DeletePayment(int id)
        {
            var result = await _paymentService.DeleteAsync(id);
            if (result.IsFailure)
            {
                return BadRequest(result.Error);
            }
            return Ok(result.Value);
        }
        //OrderStatus Service
        [HttpGet("GetOrderStatus")]
        public async Task<IActionResult> GetOrderStatus()
        {
            var result = await _orderService.GetAllAsync();
            if (result.IsFailure)
            {
                return BadRequest(result.Error);
            }
            return Ok(result.Value);
        }
        [HttpPost("CreateOrderStatus")]
        public async Task<IActionResult> CreateOrderStatus(OrderStatus request)
        {
            var result = await _orderService.AddAsync(request);
            if (result.IsFailure)
            {
                return BadRequest(result.Error);
            }
            return Ok(result.Value);
        }
        [HttpPut("UpdateOrderStatus/{id}")]
        public async Task<IActionResult> UpdateOrderStatus(int id, OrderStatus request)
        {
            var result = await _orderService.UpdateAsync(id, request);
            if (result.IsFailure)
            {
                return BadRequest(result.Error);
            }
            return Ok(result.Value);
        }
        [HttpDelete("DeleteOrderStatus/{id}")]
        public async Task<IActionResult> DeleteOrderStatus(int id)
        {
            var result = await _orderService.DeleteAsync(id);
            if (result.IsFailure)
            {
                return BadRequest(result.Error);
            }
            return Ok(result.Value);
        }
    }
}
