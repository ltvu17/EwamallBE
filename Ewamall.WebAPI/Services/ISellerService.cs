using Ewamall.Domain.Entities;
using Ewamall.Domain.Shared;

namespace Ewamall.WebAPI.Services
{
    public interface ISellerService
    {
        /*public Task<Result<IEnumerable<FeedBack>>> GetALlFeedbackOfSeller(int sellerId);*/
        public Task<Result<int>> GetCountFeedbackOfSeller(int sellerId);
        public Task<Result<IEnumerable<FeedBack>>> GetAllFeedbackOfProduct(int productId);
        public Task<Result<FeedBack>> GetFeedbackById(int feedbackId);
    }
}
