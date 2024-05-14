using Ewamall.Domain.Primitives;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ewamall.Domain.Entities
{
    public sealed class Cart : Entity
    {
        protected Cart()
        {
            
        }
        public Cart(int id, int quantity, User user, ProductSellDetail sellDetail) : base(id)
        {
            Quantity = quantity;
            User = user;
            SellDetail = sellDetail;
        }

        public int Quantity {  get; set; }
        public User User { get; set; }  
        public ProductSellDetail SellDetail { get; set; }
    }
}
