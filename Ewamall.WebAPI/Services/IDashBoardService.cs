using Ewamall.Business.Entities;
using Ewamall.Domain.Shared;
using Ewamall.WebAPI.DTOs;

namespace Ewamall.WebAPI.Services
{
    public interface IDashBoardService
    {
        public Task<Result<bool>> AddNewDownloader();
        public Task<Result<DashBoardCustomerResponse>> GetTotalDownload();
        public Task<Result<DashBoardRevenueResponse>> GetAllRevenue();
        public Task<Result<DashBoardCustomerResponse>> GetAllCustomer();
        public Task<Result<DashBoardCustomerResponse>> GetAllOrders();
        public Task<Result<DashBoardCustomerResponse>> GetAllSeller();
    }
}
