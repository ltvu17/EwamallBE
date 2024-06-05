using Ewamall.WebAPI.DTOs;
using Ewamall.WebAPI.Services;
using Ewamall.WebAPI.Services.Implements;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Ewamall.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAccountService _accountService;
        public AccountController(IAccountService accountService)
        {
            _accountService = accountService;
        }
        [HttpGet("GetAllAccounts")]
        public async Task<IActionResult> GetAllAccounts()
        {
            var result = await _accountService.GetAllAccount();
            if (result.IsFailure)
            {
                return BadRequest(result.Error);
            }
            return Ok(result.Value);
        }
        [HttpGet("GetUserDetail/{userId}")]
        public async Task<IActionResult> GetUserDetail(int userId)
        {
            var result = await _accountService.GetUserInformationById(userId);
            if (result.IsFailure)
            {
                return BadRequest(result.Error);
            }
            return Ok(result.Value);
        }
        [HttpPost("CreateAccout")]
        public async Task<IActionResult> CreateProduct([FromBody] CreateUserAccount request)
        {
            var result = await _accountService.CreateUserAccount(request);
            if (result.IsFailure)
            {
                return BadRequest(result.Error);
            }
            return Ok(result.Value);
        }
        [HttpGet("ConfirmAccount/{email}")]
        public async Task<IActionResult> ConfirmAccount(string email)
        {
            var result = await _accountService.ConfirmAccount(email);
            if (result.IsFailure)
            {
                return BadRequest(result.Error);
            }
            return Ok(result.Value);
        }
        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] Authentication login)
        {
            var result = await _accountService.Login(login);
            if (result.IsFailure)
            {
                return BadRequest(result.Error);
            }
            
            return Ok(result.Value);
        }
        [HttpPost("RegisterSeller")]
        public async Task<IActionResult> RegisterSeller([FromQuery] int userId, [FromBody] CreateSeller request)
        {
            var result = await _accountService.RegisterSeller(userId, request);
            if (result.IsFailure)
            {
                return BadRequest(result.Error);
            }
            return Ok(result.Value);
        }
        [HttpPut("UpdatePassword/{accountId}")]
        public async Task<IActionResult> UpdatePassword(int accountId, [FromBody] UpdateUserAccount request)
        {
            var result = await _accountService.UpdateAccountPassword(accountId, request);
            if (result.IsFailure)
            {
                return BadRequest(result.Error);
            }
            return Ok(result.Value);
        }
        [HttpPut("UpdateUserInformation/{userId}")]
        public async Task<IActionResult> UpdateUserInformation(int userId, [FromBody] UserInformation request)
        {
            var result = await _accountService.UpdateUserInformation(userId, request);
            if (result.IsFailure)
            {
                return BadRequest(result.Error);
            }
            return Ok(result.Value);
        }
        [HttpPut("UpdateSeller/{sellerId}")]
        public async Task<IActionResult> UpdateSellerById(int sellerId, [FromBody] CreateSeller request)
        {
            var result = await _accountService.UpdateSellerAccount(sellerId, request);
            if (result.IsFailure)
            {
                return BadRequest(result.Error);
            }
            return Ok(result.Value);
        }
        [HttpGet("GetSellerById/{sellerId}")]
        public async Task<IActionResult> GetSellerById(int sellerId)
        {
            var result = await _accountService.GetSellerById(sellerId);
            if (result.IsFailure)
            {
                return BadRequest(result.Error);
            }
            return Ok(result.Value);
        }
    }
}
