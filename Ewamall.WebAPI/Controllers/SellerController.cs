using Ewamall.Domain.Entities;
using Ewamall.WebAPI.Services;
using Ewamall.WebAPI.Services.Implements;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Ewamall.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SellerController : ControllerBase
    {
        private readonly ISellerService _sellerService;
        public SellerController(ISellerService sellerService)
        {
            _sellerService = sellerService;
        }
        [HttpGet("GetCountFbOfSeller/{sellerId}")]
        public async Task<IActionResult> GetCountFbOfSeller(int sellerId)
        {
            var result = await _sellerService.GetCountFeedbackOfSeller(sellerId);
            if (result.IsFailure)
            {
                return BadRequest(result.Error);
            }
            return Ok(result.Value);
        }
        [HttpGet("GetFeedbackOfProduct/{productId}")]
        public async Task<IActionResult> GetFeedbackOfProduct(int productId)
        {
            var result = await _sellerService.GetAllFeedbackOfProduct(productId);
            if (result.IsFailure)
            {
                return BadRequest(result.Error);
            }
            return Ok(result.Value);
        }
        [HttpGet("GetFeedbackByid/{feedbackId}")]
        public async Task<IActionResult> GetFeedbackByid(int feedbackId)
        {
            var result = await _sellerService.GetFeedbackById(feedbackId);
            if (result.IsFailure)
            {
                return BadRequest(result.Error);
            }
            return Ok(result.Value);
        }
    }
}
