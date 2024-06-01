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
        [HttpGet("GetIndustryById/{industryId}")]
        public async Task<IActionResult> GetIndustryById(int industryId)
        {
            var result = await _industryService.GetIndustryById(industryId);
            if (result.IsFailure)
            {
                return BadRequest(result.Error);
            }
            return Ok(result.Value);
        }
        [HttpGet("GetAllSubIndustry/{industryId}")]
        public async Task<IActionResult> GetAllSubIndustry(int industryId)
        {
            var result = await _industryService.GetAllSubIndustry(industryId);
            if (result.IsFailure)
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
        [HttpPut("UpdateIndustry/{id}")]
        public async Task<IActionResult> UpdateIndustry(int id, CreateIndustryAndDetailCommand request)
        {
            var result = await _industryService.UpdateIndustry(id, request);
            if (result.IsFailure)
            {
                return BadRequest(result.Error);
            }
            return Ok(result.Value);
        }
        [HttpDelete("DeleteIndustry/{id}")]
        public async Task<IActionResult> DeleteIndustry(int id)
        {
            var result = await _industryService.DeleteIndustry(id);
            if (result.IsFailure)
            {
                return BadRequest(result.Error);
            }
            return Ok(result);
        }
    }
}
