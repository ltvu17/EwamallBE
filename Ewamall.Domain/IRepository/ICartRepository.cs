using Ewamall.Domain.Entities;
using Ewamall.Domain.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ewamall.Business.IRepository
{
    public interface ICartRepository : IBaseRepo<Cart>
    {
        public Task<Cart> FindCartByUserIdAndProductId(int userId, int sellDetailId);
        public Task<IEnumerable<CartDTO>> GetCartOfUser(int userId);
    }
    public class CartDTO
    {
        public int Quantity { get; set; }
        public string Name { get; set; }
        public float? Cost { get; set; }
        public string ProductName { get; set; }
        public string SellerName { get; set; }
        public Guid? CoverImageId { get; set; }
        public int SellerId { get; set; }
        public int CartId { get; set; }
        public string NameProductSellDetail { get;  set; }
        public int ProductSellDetailId { get; set; }
    }
}
