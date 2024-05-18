using Ewamall.WebAPI.DTOs;
using Ewamall.WebAPI.Services;
using Microsoft.AspNetCore.Mvc;

namespace Ewamall.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class IndustryController : Controller
    {
        private readonly IIndustryService _industryService;

        public IndustryController(IIndustryService industryService)
        {
            _industryService = industryService;
        }
        [HttpGet("GetAllIndustry")]
        public async Task<IActionResult> GetAllIndustry() 
        {
            var result = await _industryService.GetAllIndustry();
            if(result.IsFailure)
            {
                return BadRequest(result.Error);
            }
            return Ok(result.Value);
        }
        [HttpPost("CreateIndustry")]
        public async Task<IActionResult> CreateIndustry(CreateIndustryAndDetailCommand request)
        {
            var result = await _industryService.CreateIndustry(request);
            if(result.IsFailure)
            {
                return BadRequest(result.Error);
            }
            return Ok(result.Value);
        }
    }
}
