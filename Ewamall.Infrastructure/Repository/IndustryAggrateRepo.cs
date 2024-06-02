using Ewamall.Business.IRepository;
using Ewamall.Domain.Entities;
using Ewamall.Infrastructure.Dbcontext;
using Ewamall.Infrastructure.Repository;
using Microsoft.EntityFrameworkCore;
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
        public override async Task<IEnumerable<Industry>> GetAllAsync()
        {
            return await _context.Industries.Include(s=>s.IndustryDetails).ThenInclude(s=>s.Detail).AsNoTracking().ToListAsync();
        }

        public async Task<IEnumerable<Industry>> GetAllIndustriesByRootId(int rootId)
        {
            return await _context.Industries.Where(s =>s.ParentNodeId == rootId).ToListAsync();
        }

        public async Task<Industry> GetIndustryById(int id)
        {
            return await _context.Industries.Where(s => s.Id == id).Include(s => s.IndustryDetails).ThenInclude(s => s.Detail).FirstOrDefaultAsync();
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
