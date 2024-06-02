using Ewamall.Domain.Entities;
using Ewamall.Domain.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ewamall.Business.IRepository
{
    public interface IIndustryAggrateRepo : IBaseRepo<Industry>
    {
        public bool IsDetailExist(int detailId);
        public bool IsIndustryExist(int industryId);
        public Task<IEnumerable<Industry>> GetAllIndustriesByRootId(int rootId);
        public Task<Industry> GetIndustryById(int id);
    }
}
