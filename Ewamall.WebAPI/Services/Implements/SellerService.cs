using AutoMapper;
using Ewamall.Business.IRepository;
using Ewamall.Domain.Entities;
using Ewamall.Domain.IRepository;
using Ewamall.Domain.Shared;
using Ewamall.WebAPI.DTOs;
using System.Collections.Generic;

namespace Ewamall.WebAPI.Services.Implements
{
    public class SellerService : ISellerService
    {
        private readonly ISellerRepo _sellerRepo;
        private readonly IProductRepo _productRepo;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IFeedbackRepo _feedbackRepo;
        public SellerService(ISellerRepo sellerRepo, IUnitOfWork unitOfWork, IMapper mapper, IFeedbackRepo feedbackRepo, IProductRepo productRepo)
        {
            _mapper = mapper;
            _sellerRepo = sellerRepo;
            _unitOfWork = unitOfWork;
            _feedbackRepo = feedbackRepo;
            _productRepo = productRepo;
        }

        public async Task<Result<IEnumerable<FeedBack>>> GetAllFeedbackOfProduct(int productId)
        {
            var findProduct = await _productRepo.GetByIdAsync(productId);
            if (findProduct is null)
            {
                return Result.Failure<IEnumerable<FeedBack>>(new Error("Get all feedback of product", "ProductId not existed"));
            }
            Result<IEnumerable<FeedBack>> result = (await _feedbackRepo.GetAllFbByProduct(productId)).ToList(); 
            return result;
            
        }

        public async Task<Result<int>> GetCountFeedbackOfSeller(int sellerId)
        {
            var findSeller = await _sellerRepo.GetByIdAsync(sellerId);
            if (findSeller is null)
            {
                return Result.Failure<int>(new Error("Get count fb of seller", "SellerId not existed"));
            }
            return await _feedbackRepo.CountFeedbackBySellerIdAsync(sellerId);
        }

        public async Task<Result<FeedBack>> GetFeedbackById(int feedbackId)
        {
            var findFeedback = await _feedbackRepo.GetByIdAsync(feedbackId);
            if (findFeedback is null)
            {
                return Result.Failure<FeedBack>(new Error("GetFBById", "FeedbackId not existed"));
            }
            return await _feedbackRepo.GetByIdAsync(feedbackId);
        }
    }
}
