using Ewamall.Domain.Entities;
using Ewamall.Domain.Shared;
using Ewamall.WebAPI.DTOs;
using Ewamall.WebAPI.Services;
using Ewamall.WebAPI.Services.Implements;
using Microsoft.AspNetCore.Mvc;

namespace Ewamall.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VoucherController : Controller
    {
        private readonly IVoucherService _voucherService;

        public VoucherController(IVoucherService voucherService)
        {
            _voucherService = voucherService;
        }
        [HttpGet("GetAllVouchers")]
        public async Task<IActionResult> GetAllVouchers()
        {
            var result = await _voucherService.GetAllVoucher();
            if (result.IsFailure)
            {
                return BadRequest(result.Error);
            }
            return Ok(result.Value);
        }
        [HttpPost("CreateVoucher")]
        public async Task<IActionResult> CreateVoucher(int staffId, [FromBody] CreateVoucherCommand request)
        {
            var result = await _voucherService.CreateVoucher(staffId,request);
            if (result.IsFailure)
            {
                return BadRequest(result.Error);
            }
            return Ok(result.Value);
        }
        [HttpPut("UpdateVoucher/{id}")]
        public async Task<IActionResult> UpdateVoucher(int id, [FromBody] CreateVoucherCommand request)
        {
            var result = await _voucherService.UpdateVoucher(id, request);
            if (result.IsFailure)
            {
                return BadRequest(result.Error);
            }
            return Ok(result.Value);
        }
        [HttpDelete("DeleteVoucher/{id}")]
        public async Task<IActionResult> DeleteVoucher(int id)
        {
            var result = await _voucherService.DeleteVoucher(id);
            if (result.IsFailure)
            {
                return BadRequest(result.Error);
            }
            return Ok(result.Value);
        }
    }
}
