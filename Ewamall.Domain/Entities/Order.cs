using Ewamall.Domain.Primitives;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ewamall.Domain.Entities
{
    public sealed class Order : Entity
    {
        protected Order()
        {
            
        }
        public Order(int id, string orderCode, DateTime orderDate, DateTime payDate, DateTime shipDate, float totalCost, float shipCost, DateTime? cancelDate, string cancelReason, OrderStatus status, User user, ShipAddress shipAddress, Voucher voucher, Payment payment) : base(id)
        {
            OrderCode = orderCode;
            OrderDate = orderDate;
            PayDate = payDate; 
            ShipDate = shipDate;
            TotalCost = totalCost;
            ShipCost = shipCost;
            CancelDate = cancelDate;
            CancelReason = cancelReason;
            Status = status;
            User = user;
            ShipAddress = shipAddress;
            Voucher = voucher;
            Payment = payment;
        }

        public string OrderCode { get; set; }
        public DateTime OrderDate { get; set; }
        public DateTime PayDate { get; set; }
        public DateTime ShipDate { get; set; }
        public float TotalCost { get; set; }
        public float ShipCost { get; set; }
        public DateTime? CancelDate { get; set; }
        public string CancelReason { get; set; }
        public OrderStatus Status { get; set; }
        public User User { get; set; }
        public ShipAddress ShipAddress { get; set; }    
        public Voucher? Voucher { get; set; }    
        public Payment Payment { get; set; }
        public IEnumerable<OrderDetail> OrderDetails { get; set; } 
    }
}
