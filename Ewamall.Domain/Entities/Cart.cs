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
    public sealed class Cart : Entity
    {
        protected Cart()
        {
            
        }
        internal Cart(int quantity, int userId, int sellDetailId)
        {
            Quantity = quantity;
            UserId = userId;
            SellDetailId = sellDetailId;
        }

        public int Quantity {  get; set; }
        [ForeignKey("User")]
        public int UserId { get; set; }
        public User User { get; set; }
        [ForeignKey("SellDetail")]
        public int SellDetailId { get; set; }
        public ProductSellDetail SellDetail { get; set; }

        public static Result<Cart> Create(int quantity, int userId, int sellDetailId, Cart cart)
        {
            if(cart != null)
            {
                cart.Quantity += quantity;
                return cart;
            }
            else
            {
                var newCart = new Cart(quantity , userId, sellDetailId);
                return newCart;
            }
        }
    }
}
