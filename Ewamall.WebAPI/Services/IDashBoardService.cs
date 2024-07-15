using Ewamall.Business.Entities;
using Ewamall.Business.IRepository;
using Ewamall.Domain.Entities;
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
        public Task<Result<IEnumerable<SellerDTO>>> GetListSellers();
        public Task<Result<IEnumerable<User>>> GetListCustomers();
        public Task<Result<IEnumerable<OrderDTO>>> GetListOrders();
    }
}
