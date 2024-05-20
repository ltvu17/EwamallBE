using Ewamall.Domain.Primitives;
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
        public OrderDetail() { }

        internal OrderDetail(int quantity, Order order, ProductSellDetail productDetail)
        {
            Quantity = quantity;
            Order = order;
            ProductSellDetail = productDetail;
        }
        public int Quantity { get; private set; }
        public Order Order { get; private set; }
        [ForeignKey("ProductSellDetail")]
        public int ProductSellDetailId { get; private set; }
        public ProductSellDetail ProductSellDetail { get; private set; }
    }
}
