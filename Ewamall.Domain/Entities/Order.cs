using Ewamall.Domain.Primitives;
using Ewamall.Domain.Shared;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ewamall.Domain.Entities
{
    public sealed class Order : Entity
    {
        private List<OrderDetail> _orderDetails = new();
        protected Order()
        {
            
        }
        internal Order(string orderCode, float totalCost, float shipCost, int statusId, int userId, int shipAddressId, int voucherId, int paymentId)
        {
            OrderCode = orderCode;
            TotalCost = totalCost;
            ShipCost = shipCost;
            StatusId = statusId;
            UserId = userId;
            ShipAddressId = shipAddressId;
            VoucherId = voucherId;
            PaymentId = paymentId;
        }

        public string OrderCode { get; private set; }
        public DateTime OrderDate { get; private set; }
        public DateTime PayDate { get; private set; }
        public DateTime ShipDate { get; private set; }
        public float TotalCost { get; private set; }
        public float ShipCost { get; private set; }
        public DateTime? CancelDate { get; private set; }
        public string CancelReason { get; private set; }
        [ForeignKey("Status")]
        public int StatusId { get; private set; }
        public OrderStatus Status { get; private set; }
        [ForeignKey("User")]
        public int UserId { get; private set; } 
        public User User { get; private set; }
        [ForeignKey("ShipAddress")]
        public int ShipAddressId { get; private set; }
        public ShipAddress ShipAddress { get; private set; }
        [ForeignKey("Voucher")]
        public int? VoucherId { get; private set; }  
        public Voucher? Voucher { get; private set; }
        [ForeignKey("Payment")]
        public int PaymentId { get; private set; }  
        public Payment Payment { get; private set; }
        public IEnumerable<OrderDetail> OrderDetails => _orderDetails;


        public static Result<Order> Create(string orderCode, float totalCost, float shipCost, int statusId, int userId, int shipAddressId, int voucherId, int paymentId, DateTime orderDate)
        {
            if(totalCost < 0 || shipCost < 0)
            {
                return Result.Failure<Order>(new Error("Order.Create()", "Cost Error"));
            }
            var Order = new Order( orderCode, totalCost, shipCost, statusId, userId, shipAddressId, voucherId, paymentId);
            if(voucherId == 0)
            {
                Order.VoucherId = null;
            }
            Order.OrderDate = orderDate;
            Order.CancelReason = "";
            return Order;
        }
        public Result<Order> AddOrderDetail(int quantity, int productSellDetailId)
        {
            var result = OrderDetail.Create(quantity, this, productSellDetailId);
            if (result.IsFailure)
            {
                return Result.Failure<Order>(result.Error);
            }
            _orderDetails.Add(result.Value);
            return this;
        }
        public Result<Order> ChangeStatus(int statusId)
        {
            StatusId = statusId;
            return this;
        }
        public Result<Order> CancelOrder(int statusId, string cancelReason)
        {
            StatusId = statusId;
            CancelDate = DateTime.Now;
            CancelReason = cancelReason;
            return this;
        }
    }
}
