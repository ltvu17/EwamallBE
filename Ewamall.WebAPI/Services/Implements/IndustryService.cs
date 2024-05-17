using Ewamall.Business.IRepository;
using Ewamall.Domain.Entities;
using Ewamall.Domain.Shared;
using Ewamall.WebAPI.DTOs;

namespace Ewamall.WebAPI.Services.Implements
{
    public class IndustryService : IIndustryService
    {
        private readonly IIndustryAggrateRepo _industryRepo;
        private readonly IUnitOfWork _unitOfWork;

        public IndustryService(IIndustryAggrateRepo industryRepo, IUnitOfWork unitOfWork)
        {
            _industryRepo = industryRepo;
            _unitOfWork = unitOfWork;
        }
        public async Task<Result<IEnumerable<Industry>>> GetAllIndustry()
        {
            return (await _industryRepo.GetAllAsync()).ToList();
        }

        public async Task<Result<Industry>> CreateIndustry(CreateIndustryAndDetailCommand request)
        {
            var result = Industry.Create(
                request.IndustryName,
                request.IsActive,
                request.Level,
                request.IsLeaf,
                request.Path,
                request.RootId
                );
            if (result.IsFailure)
            {
                return Result.Failure<Industry>(new Error("Industry.CreateIndustry()", "Can not create Industry"));
            }

            var industry = result.Value;
            var industryWithExistDetail = request.ExistDetails;
            if ( industryWithExistDetail != null)
            {
                foreach(var detail in industryWithExistDetail)
                {
                    industry.AddIndustryDetail(detail.DetailId);
                }
            }
            var industryWithNewDetail = request.NewDetails;
            if(industryWithNewDetail != null)
            {
                foreach( var detail in industryWithNewDetail)
                {
                    industry.AddIndustryDetailWithNewDetail(detail.DetailName, detail.DetailDescription);
                }
            }
            await _industryRepo.AddAsync(industry);
            await _unitOfWork.SaveChangesAsync();
            return result;

        }
    }
}
