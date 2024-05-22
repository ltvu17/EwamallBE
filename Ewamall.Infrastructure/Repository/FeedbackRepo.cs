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
    public class FeedbackRepo : BaseRepo<FeedBack>, IFeedbackRepo
    {
        private readonly EwamallDBContext _context;
        public FeedbackRepo(EwamallDBContext context) : base(context)
        {
            _context = context;
        }

        public async Task<IEnumerable<FeedBack>> GetAllFeedbackSeller(int sellerId)
        {
            return await _context.FeedBacks.Where(s => s.ProductSellDetail.Product.Seller.Id == sellerId).ToListAsync();
        }
        public async Task<int> CountFeedbackBySellerIdAsync(int sellerId)
        {
            return await _context.FeedBacks.Where(s => s.ProductSellDetail.Product.Seller.Id == sellerId).CountAsync();
        }

        public async Task<IEnumerable<FeedBack>> GetAllFbByProduct(int productId)
        {
            return await _context.FeedBacks.Where(s=>s.ProductSellDetail.Product.Id == productId).ToListAsync();
        }
        public override async Task<FeedBack> GetByIdAsync(int id)
        {
            return await _context.FeedBacks.Where(s => s.Id == id).Include(s => s.User).FirstOrDefaultAsync();
        }

    }
}
