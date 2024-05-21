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
        [HttpPut("UpdateUserAccount/{id}")]
        public async Task<IActionResult> UpdateUserAccount(int userId, [FromBody] CreateUserAccount request)
        {
            var result = await _accountService.UpdateUserAccount(userId, request);
            if (result.IsFailure)
            {
                return BadRequest(result.Error);
            }
            return Ok(result.Value);
        }
        [HttpPut("UpdateSeller/{id}")]
        public async Task<IActionResult> UpdateSeller(int sellerId, [FromBody] CreateSeller request)
        {
            var result = await _accountService.UpdateSellerAccount(sellerId, request);
            if (result.IsFailure)
            {
                return BadRequest(result.Error);
            }
            return Ok(result.Value);
        }
    }
}
