using Ewamall.Domain.Shared;
using Ewamall.WebAPI.DTOs;
using Ewamall.WebAPI.Services;
using Ewamall.WebAPI.Services.Implements;
using Microsoft.AspNetCore.Mvc;

namespace Ewamall.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DashBoardController : Controller
    {
        private readonly IDashBoardService _dashBoardService;

        public DashBoardController(IDashBoardService dashBoardService)
        {
            _dashBoardService = dashBoardService;
        }
        [HttpGet("GetAllRevenue")]
        public async Task<IActionResult> GetAllRevenue()
        {
            var response = await _dashBoardService.GetAllRevenue();
            if (response.IsFailure == true) { 
            return BadRequest(Result.Failure<DashBoardRevenueResponse>(new Error("GetAllRevenue()", "can not get response Revenue")));
            }
            return Ok(response.Value);
        }
        [HttpGet("GetAllCustomer")]
        public async Task<IActionResult> GetAllCustomer()
        {
            var response = await _dashBoardService.GetAllCustomer();
            if (response.IsFailure == true)
            {
                return BadRequest(Result.Failure<DashBoardRevenueResponse>(new Error("GetAllCustomer()", "can not get response Customer")));
            }
            return Ok(response.Value);
        }
        [HttpGet("GetAllSeller")]
        public async Task<IActionResult> GetAllSeller()
        {
            var response = await _dashBoardService.GetAllSeller();
            if (response.IsFailure == true)
            {
                return BadRequest(Result.Failure<DashBoardRevenueResponse>(new Error("GetAllSeller()", "can not get response Seller")));
            }
            return Ok(response.Value);
        }
        [HttpGet("GetAllOrder")]
        public async Task<IActionResult> GetAllOrder()
        {
            var response = await _dashBoardService.GetAllOrders();
            if (response.IsFailure == true)
            {
                return BadRequest(Result.Failure<DashBoardRevenueResponse>(new Error("GetAllOrder()", "can not get response Order")));
            }
            return Ok(response.Value);
        }
        [HttpGet("GetAllDownloader")]
        public async Task<IActionResult> GetAllDownloader()
        {
            var response = await _dashBoardService.GetTotalDownload();
            if (response.IsFailure == true)
            {
                return BadRequest(Result.Failure<DashBoardRevenueResponse>(new Error("GetAllOrder()", "can not get response Order")));
            }
            return Ok(response.Value);
        }
        [HttpPost("NewDownload")]
        public async Task<IActionResult> NewDownload()
        {
            var response = await _dashBoardService.AddNewDownloader();
            if (response.IsFailure == true)
            {
                return BadRequest(Result.Failure<DashBoardRevenueResponse>(new Error("NewDownload()", "Insert new downloader")));
            }
            return Ok(response.Value);
        }
        [HttpGet("GetListSellers")]
        public async Task<IActionResult> GetAllAccounts()
        {
            var result = await _dashBoardService.GetListSellers();
            if (result.IsFailure)
            {
                return BadRequest(result.Error);
            }
            return Ok(result.Value);
        }
        [HttpGet("GetListCustomers")]
        public async Task<IActionResult> GetListCustomers()
        {
            var result = await _dashBoardService.GetListCustomers();
            if (result.IsFailure)
            {
                return BadRequest(result.Error);
            }
            return Ok(result.Value);
        }
        [HttpGet("GetListOrders")]
        public async Task<IActionResult> GetListOrders()
        {
            var result = await _dashBoardService.GetListOrders();
            if (result.IsFailure)
            {
                return BadRequest(result.Error);
            }
            return Ok(result.Value);
        }
        [HttpGet("Download")]
        public async Task<IActionResult> GetFileById()
        {
            string path = "app\\Ewamall.apk";
            string dir = Directory.GetCurrentDirectory();
            var file = Directory.GetFiles(dir, "Ewamall.apk", SearchOption.AllDirectories);
            if (System.IO.File.Exists(file[0]))
            {
                return File(System.IO.File.OpenRead(file[0]), "application/octet-stream", Path.GetFileName(file[0]));
            }
  
            return Ok(file[0]);
        }
    }
}
