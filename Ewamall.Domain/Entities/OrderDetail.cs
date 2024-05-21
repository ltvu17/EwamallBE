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
    public sealed class OrderDetail : Entity
    {
        private OrderDetail() { }

        private OrderDetail(int quantity, Order order, int productDetailId)
        {
            Quantity = quantity;
            Order = order;
            ProductSellDetailId = productDetailId;
        }
        public int Quantity { get; private set; }
        public Order Order { get; private set; }
        [ForeignKey("ProductSellDetail")]
        public int ProductSellDetailId { get; private set; }
        public ProductSellDetail ProductSellDetail { get; private set; }

        internal static Result<OrderDetail> Create(int quantity, Order order, int productDetailId)
        {
            if(quantity <= 0)
            {
                return Result.Failure<OrderDetail>(new Error("OrderDetail.Create()", "Fail to create order detail"));
            }
            var orderDetail = new OrderDetail(quantity, order, productDetailId);
            return orderDetail;
        }
    }
}
