using Ewamall.Business.IRepository;
using Ewamall.Domain.Entities;
using Ewamall.Infrastructure.Dbcontext;
using Ewamall.Infrastructure.Repository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ewamall.DataAccess.Repository
{
    public class OrderRepo : BaseRepo<Order>, IOrderRepo
    {
        private readonly EwamallDBContext _context;
        public OrderRepo(EwamallDBContext context) : base(context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Order>> GetOrderByUserId(int userId)
        {
            return await _context.Orders.Where(s =>s.UserId == userId).Include(s=>s.OrderDetails).ThenInclude(s=>s.ProductSellDetail).ThenInclude(s=>s.Product).ThenInclude(s=>s.Seller).Include(s=>s.Status).AsNoTracking().ToListAsync();
        }
        public async Task<IEnumerable<Order>> GetOrderBySellerId(int sellerId)
        {
            return await _context.Orders.Where(s => s.OrderDetails.FirstOrDefault().ProductSellDetail.Product.Seller.Id == sellerId).Include(s => s.Status).Include(s => s.OrderDetails).ThenInclude(s => s.ProductSellDetail).ThenInclude(s => s.Product).ThenInclude(s=>s.Seller).AsNoTracking().ToListAsync();
        }

        public async Task<IEnumerable<OrderDTO>> GetAllAsyncDTO()
        {
            return  _context.Orders.Include(s => s.OrderDetails).ThenInclude(s => s.ProductSellDetail).ThenInclude(s => s.Product).ThenInclude(s => s.Seller).AsNoTracking().Select(s=> new OrderDTO
            {
                id = s.Id,
                Seller = s.OrderDetails.FirstOrDefault().ProductSellDetail.Product.Seller,
                CancelReason = s.CancelReason,
                CancelDate = s.CancelDate,  
                OrderCode = s.OrderCode,
                OrderDate = s.OrderDate,
                PayDate = s.PayDate,
                ShipCost = s.ShipCost,
                ShipDate = s.ShipDate,
                TotalCost = s.TotalCost,
                Status = s.Status
            });
        }
    }
}
