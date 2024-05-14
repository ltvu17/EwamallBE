using Ewamall.Domain.Primitives;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ewamall.Domain.Entities
{
    public sealed class OrderDetail : Entity
    {
        public OrderDetail() { }

        public OrderDetail(int id,int quantity, Order order, ProductSellDetail productDetail) :  base(id)
        {
            Quantity = quantity;
            Order = order;
            ProductSellDetail = productDetail;
        }
        public int Quantity { get; set; }
        public Order Order { get; set; }
        public ProductSellDetail ProductSellDetail { get; set; }
    }
}
