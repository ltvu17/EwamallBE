using Ewamall.WebAPI.DTOs;
using Ewamall.WebAPI.Services;
using Ewamall.WebAPI.Services.Implements;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Ewamall.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WalletController : ControllerBase
    {
        private readonly IWalletService _walletService;

        public WalletController(IWalletService walletService)
        {
            _walletService = walletService;
        }
        [HttpGet("GetBalanceBySellerId/{sellerId}")]
        public async Task<IActionResult> GetBalanceBySellerId(int sellerId)
        {
            var result = await _walletService.GetBalance(sellerId);
            if (result.IsFailure)
            {
                return BadRequest(result.Error);
            }
            return Ok(result.Value);
        }
        [HttpPut("DepositBalance/{sellerId}")]
        public async Task<IActionResult> DepositBalance(int sellerId, [FromQuery] float amount)
        {
            var result = await _walletService.DepositBalance(sellerId, amount);
            if (result.IsFailure)
            {
                return BadRequest(result.Error);
            }
            return Ok(result.Value);
        }
        [HttpPut("WithdrawBalance/{sellerId}")]
        public async Task<IActionResult> WithdrawBalance(int sellerId, [FromQuery] float amount)
        {
            var result = await _walletService.WithdrawBalance(sellerId, amount);
            if (result.IsFailure)
            {
                return BadRequest(result.Error);
            }
            return Ok(result.Value);
        }
    }
}
