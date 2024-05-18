using Ewamall.Business.IRepository;
using Ewamall.Domain.Entities;
using Ewamall.Infrastructure.Dbcontext;
using Ewamall.Infrastructure.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Ewamall.DataAccess.Repository
{
    public class IndustryAggrateRepo : BaseRepo<Industry>, IIndustryAggrateRepo
    {
        private readonly EwamallDBContext _context;
        public IndustryAggrateRepo(EwamallDBContext context) : base(context) 
        {
            _context = context;
        }

        public bool IsDetailExist(int detailId)
        {
            return _context.Details.Any(s => s.Id == detailId);
        }

        public bool IsIndustryExist(int industryId)
        {
            return _context.Industries.Any(s => s.Id == industryId);
        }
    }
}
