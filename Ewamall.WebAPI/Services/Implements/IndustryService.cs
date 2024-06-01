using AutoMapper;
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
        private readonly IMapper _mapper;

        public IndustryService(IIndustryAggrateRepo industryRepo, IUnitOfWork unitOfWork, IMapper mapper)
        {
            _industryRepo = industryRepo;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
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

        public async Task<Result<Industry>> UpdateIndustry(int id, CreateIndustryAndDetailCommand request)
        {
            var industry = await _industryRepo.GetByIdAsync(id);
            if(industry is null)
            {
                return Result.Failure<Industry>(new Error("UpdateIndustry.GetByIdAsync", "Industry Id is not exist"));
            }
            var newindustry = _mapper.Map(request,industry);
            var industryWithExistDetail = request.ExistDetails;
            if (industryWithExistDetail != null)
            {
                foreach (var detail in industryWithExistDetail)
                {
                    newindustry.AddIndustryDetail(detail.DetailId);
                }
            }
            var industryWithNewDetail = request.NewDetails;
            if (industryWithNewDetail != null)
            {
                foreach (var detail in industryWithNewDetail)
                {
                    newindustry.AddIndustryDetailWithNewDetail(detail.DetailName, detail.DetailDescription);
                }
            }
            
            await _industryRepo.UpdateAsync(newindustry);
            await _unitOfWork.SaveChangesAsync();
            return newindustry;
        }

        public async Task<Result<bool>> DeleteIndustry(int id)
        {
            var industry = (await _industryRepo.FindAsync(s => s.Id == id, 1, 1, includeProperties: "IndustryDetails")).FirstOrDefault();
            if(industry is null)
            {
                return Result.Failure<bool>(new Error("DeleteIndustry.FindAsync()", "Industry id is not exist"));
            }

            var industryChild = await _industryRepo.FindAsync(s => s.ParentNodeId == industry.Id, int.MaxValue, 1, includeProperties: "IndustryDetails");
            if (industryChild is not null)
            {
                await _industryRepo.RemoveRange(industryChild);
            }
            await _industryRepo.RemoveEntityAsync(industry);
            await _unitOfWork.SaveChangesAsync();
            return Result.Success<bool>(true);
        }

        public async Task<Result<IEnumerable<Industry>>> GetAllSubIndustry(int industryId)
        {
            var industry = await _industryRepo.GetByIdAsync(industryId);
            if(industry.IsLeaf == true)
            {
                return Result.Failure<IEnumerable<Industry>>(new Error("GetAllSubIndustry.GetByIdAsync()", "This industry is leaf"));
            }
            var industries = (await _industryRepo.FindAsync(s=>s.Level == industry.Level+1 && s.ParentNodeId == industry.ParentNodeId && s.Path.StartsWith(industry.Path), int.MaxValue, 1)).ToList();
            if(industries.Count == 0)
            {
                return Result.Failure<IEnumerable<Industry>>(new Error("GetAllSubIndustry.FindAsync()", "This industries not found"));
            }
            return industries;
        }

        public async Task<Result<Industry>> GetIndustryById(int id)
        {
            var industry = await _industryRepo.GetIndustryById(id);
            if (industry == null)
            {
                return Result.Failure<Industry>(new Error("GetIndustryById", "Industry not found"));
            }
            return industry;
        }
    }
}
