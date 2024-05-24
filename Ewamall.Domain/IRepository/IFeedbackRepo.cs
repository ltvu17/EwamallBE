using Ewamall.Domain.Entities;
using Ewamall.Domain.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ewamall.Business.IRepository
{
    public interface IFeedbackRepo : IBaseRepo<FeedBack>
    {
        public Task<IEnumerable<FeedBack>> GetAllFeedbackSeller(int sellerId);
        public Task<int> CountFeedbackBySellerIdAsync(int sellerId);
        public Task<IEnumerable<FeedBack>> GetAllFbByProduct(int productId);
    }
}
