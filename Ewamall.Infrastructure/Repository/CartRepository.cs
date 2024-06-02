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
    public class CartRepository : BaseRepo<Cart>, ICartRepository 
    {
        private readonly EwamallDBContext _dbContext;
        public CartRepository(EwamallDBContext dBContext) : base(dBContext)
        {
            _dbContext = dBContext;
        }
        public async Task<Cart> FindCartByUserIdAndProductId(int userId, int sellDetailId)
        {
            return await _dbContext.Carts.FirstOrDefaultAsync(s => s.UserId == userId && s.SellDetailId == sellDetailId);
        }

        public async Task<IEnumerable<CartDTO>> GetCartOfUser(int userId)
        {
            return await _dbContext.Carts.Where(s => s.UserId == userId).Include(s => s.SellDetail).ThenInclude(s => s.Product).ThenInclude(s => s.Seller)
                .Select(s => new CartDTO {
                    ProductName = s.SellDetail.Product.ProductName,
                    Cost = (float)s.SellDetail.Price,
                    Name = s.SellDetail.Name,
                    Quantity = s.Quantity,
                    SellerName = s.SellDetail.Product.Seller.ShopName,
                    CartId = s.Id,
                    CoverImageId = s.SellDetail.Product.CoverImageId,
                    NameProductSellDetail = s.SellDetail.Name,
                    SellerId = s.SellDetail.Product.SellerId,
                    ProductSellDetailId = s.SellDetailId
            }).ToListAsync();
        }
    }

}
