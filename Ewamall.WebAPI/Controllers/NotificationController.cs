using Ewamall.WebAPI.DTOs;
using Ewamall.WebAPI.Services;
using Ewamall.WebAPI.Services.Implements;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Ewamall.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NotificationController : ControllerBase
    {
        private readonly INotificationService _notificationService;
        public NotificationController(INotificationService notificationService)
        {
            _notificationService = notificationService;
        }
        [HttpGet("GetAllNotification")]
        public async Task<IActionResult> GetAllNotification()
        {
            var result = await _notificationService.GetAllNotification();
            if (result.IsFailure)
            {
                return BadRequest(result.Error);
            }
            return Ok(result.Value);
        }
        [HttpGet("GetAllNotificationByUserId/{userName}")]
        public async Task<IActionResult> GetAllNotificationByUserName(string userName)
        {
            var result = await _notificationService.GetAllNotificationByUserName(userName);
            if (result.IsFailure)
            {
                return BadRequest(result.Error);
            }
            return Ok(result.Value);
        }
        [HttpPost("CreateNotification")]
        public async Task<IActionResult> CreateProduct([FromBody] CreateNotification request)
        {
            var result = await _notificationService.CreateNotification(request);
            if (result.IsFailure)
            {
                return BadRequest(result.Error);
            }
            return Ok(result.Value);
        }
        [HttpPut("UpdateNotification/{id}")]
        public async Task<IActionResult> UpdateNotification(int id, [FromBody] CreateNotification request)
        {
            var result = await _notificationService.UpdateNotification(id, request);
            if (result.IsFailure)
            {
                return BadRequest(result.Error);
            }
            return Ok(result.Value);
        }
        [HttpDelete("DeleteNotification/{id}")]
        public async Task<IActionResult> DeleteNotification(int id)
        {
            var result = await _notificationService.DeleteNotification(id);
            if (result.IsFailure)
            {
                return BadRequest(result.Error);
            }
            return Ok(result.Value);
        }
    }
}
