using Ewamall.Domain.Entities;
using Ewamall.Domain.IRepository;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ewamall.Business.IRepository
{
    public interface IOrderRepo: IBaseRepo<Order>
    {
        public Task<IEnumerable<Order>> GetOrderByUserId(int userId);
        public Task<IEnumerable<Order>> GetOrderBySellerId(int sellerId);
        public Task<IEnumerable<OrderDTO>> GetAllAsyncDTO();
    }
    public class OrderDTO
    {
        public int id { get; set; }
        public string OrderCode { get;  set; }
        public DateTime OrderDate { get;  set; }
        public DateTime PayDate { get;  set; }
        public DateTime ShipDate { get;  set; }
        public float TotalCost { get;  set; }
        public float ShipCost { get;  set; }
        public DateTime? CancelDate { get;  set; }
        public string CancelReason { get;  set; }
        public Seller Seller { get;  set; }
        public OrderStatus Status { get;  set; }

    }
}
