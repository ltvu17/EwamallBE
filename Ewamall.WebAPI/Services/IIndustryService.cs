using Ewamall.Domain.Entities;
using Ewamall.Domain.Shared;
using Ewamall.WebAPI.DTOs;

namespace Ewamall.WebAPI.Services
{
    public interface IIndustryService
    {
        public Task<Result<IEnumerable<Industry>>> GetAllIndustry();
        public Task<Result<Industry>> GetIndustryById(int id);
        public Task<Result<IEnumerable<Industry>>> GetAllSubIndustry(int industryId);
        public Task<Result<Industry>> CreateIndustry(CreateIndustryAndDetailCommand request);
        public Task<Result<Industry>> UpdateIndustry(int id, CreateIndustryAndDetailCommand request);
        public Task<Result<bool>> DeleteIndustry(int id);
        
    }
}
