using Ewamall.Business.Entities;
using Ewamall.Business.IRepository;
using Ewamall.DataAccess.Repository;
using Ewamall.Domain.Entities;
using Ewamall.Domain.Shared;
using Ewamall.WebAPI.DTOs;

namespace Ewamall.WebAPI.Services.Implements
{
    public class DashBoardService : IDashBoardService
    {
        private readonly IDashBoardRepo _dashBoardRepo;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IUserRepo _userRepo;
        private readonly IOrderRepo _orderRepo;
        private readonly ISellerRepo _sellerRepo;

        public DashBoardService(IDashBoardRepo dashBoardRepo, IUnitOfWork unitOfWork, IUserRepo userRepo, IOrderRepo orderRepo, ISellerRepo sellerRepo)
        {
            _dashBoardRepo = dashBoardRepo;
            _unitOfWork = unitOfWork;
            _userRepo = userRepo;
            _orderRepo = orderRepo;
            _sellerRepo = sellerRepo;
        }
        public async Task<Result<bool>> AddNewDownloader()
        {
            var date = DateTime.Now;
            await _dashBoardRepo.AddAsync(new DashBoard
            {
                Day = date.Day,
                Month = date.Month,
                Year = date.Year,
            });
            await _unitOfWork.SaveChangesAsync();
            return Result.Success(true);
        }

        public async Task<Result<DashBoardCustomerResponse>> GetAllCustomer()
        {
            var getAllUser = (await _userRepo.GetAllAsync()).Count();
            var getAllUserThisMonth = (await _userRepo.FindAsync(s =>s.CreateDate.Month == DateTime.Now.Month, int.MaxValue, 1)).Count();
            return Result.Success<DashBoardCustomerResponse>(new DashBoardCustomerResponse {
                Total = getAllUser,
                ThisMonth = getAllUserThisMonth,
            });
        }

        public async Task<Result<DashBoardCustomerResponse>> GetAllOrders()
        {
            var getAll = (await _orderRepo.GetAllAsync()).Count();
            var getThisMonth = (await _orderRepo.FindAsync(s => s.OrderDate.Month == DateTime.Now.Month, int.MaxValue, 1)).Count();
            return Result.Success<DashBoardCustomerResponse>(new DashBoardCustomerResponse
            {
                Total = getAll,
                ThisMonth = getThisMonth,
            });
        }

        public async Task<Result<DashBoardRevenueResponse>> GetAllRevenue()
        {
            var orders = (await _orderRepo.GetAllAsync()).Where(s=>s.StatusId == 4).ToList();
            DashBoardRevenueResponse response = new DashBoardRevenueResponse();
            foreach (var order in orders)
            {
                if(order.OrderDate.Month == 1)
                {
                    response.RevenueMonth1 += order.TotalCost;
                }
                else
                if (order.OrderDate.Month == 2)
                {
                    response.RevenueMonth2 += order.TotalCost;
                }
                else
                if (order.OrderDate.Month == 3)
                {
                    response.RevenueMonth3 += order.TotalCost;
                }
                else
                if (order.OrderDate.Month == 4)
                {
                    response.RevenueMonth4 += order.TotalCost;
                }
                else
                if (order.OrderDate.Month == 5)
                {
                    response.RevenueMonth5 += order.TotalCost;
                }
                else
                if (order.OrderDate.Month == 6)
                {
                    response.RevenueMonth6 += order.TotalCost;
                }
                else
                if (order.OrderDate.Month == 7)
                {
                    response.RevenueMonth7 += order.TotalCost;
                }
                else
                if (order.OrderDate.Month == 8)
                {
                    response.RevenueMonth8 += order.TotalCost;
                }
                else
                if (order.OrderDate.Month == 9)
                {
                    response.RevenueMonth9 += order.TotalCost;
                }
                else
                if (order.OrderDate.Month == 10)
                {
                    response.RevenueMonth10 += order.TotalCost;
                }
                else
                if (order.OrderDate.Month == 11)
                {
                    response.RevenueMonth11 += order.TotalCost;
                }
                else
                if (order.OrderDate.Month == 12)
                {
                    response.RevenueMonth12 += order.TotalCost;
                }
            }
            return response;
        }

        public async Task<Result<DashBoardCustomerResponse>> GetAllSeller()
        {
            var getAll = (await _sellerRepo.GetAllAsync()).Count();
            var getThisMonth = (await _sellerRepo.FindAsync(s => s.CreateDate.Month == DateTime.Now.Month, int.MaxValue, 1)).Count();
            return Result.Success<DashBoardCustomerResponse>(new DashBoardCustomerResponse
            {
                Total = getAll,
                ThisMonth = getThisMonth,
            });
        }

        public async Task<Result<IEnumerable<User>>> GetListCustomers()
        {
            Result<IEnumerable<User>> result = (await _userRepo.GetAllAsync()).ToList();
            if (result.IsFailure)
            {
                return Result.Failure<IEnumerable<User>>(new Error("IEnumerable<User>.GetAll()", "Fail to load account"));
            }
            return result;
        }

        public async Task<Result<IEnumerable<OrderDTO>>> GetListOrders()
        {
            Result<IEnumerable<OrderDTO>> result = (await _orderRepo.GetAllAsyncDTO()).ToList();
            if (result.IsFailure)
            {
                return Result.Failure<IEnumerable<OrderDTO>>(new Error("IEnumerable<Order>.GetAll()", "Fail to load account"));
            }
            return result;
        }

        public async Task<Result<IEnumerable<SellerDTO>>> GetListSellers()
        {
            Result<IEnumerable<SellerDTO>> result = (await _sellerRepo.GetSellerDTOs()).ToList();
            if (result.IsFailure)
            {
                return Result.Failure<IEnumerable<SellerDTO>>(new Error("IEnumerable<Seller>.GetAll()", "Fail to load account"));
            }
            return result;
        }

        public async Task<Result<DashBoardCustomerResponse>> GetTotalDownload()
        {
            var getAll = (await _dashBoardRepo.GetAllAsync()).Count();
            var getThisMonth = (await _dashBoardRepo.FindAsync(s => s.Month == DateTime.Now.Month, int.MaxValue, 1)).Count();
            return Result.Success<DashBoardCustomerResponse>(new DashBoardCustomerResponse
            {
                Total = getAll,
                ThisMonth = getThisMonth,
            });
        }
    }
}
