using Ewamall.Business.IRepository;
using Ewamall.Infrastructure.Dbcontext;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ewamall.DataAccess.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly EwamallDBContext _dbContext;
        public UnitOfWork(EwamallDBContext context)
        {
            _dbContext = context;
        }
        public Task SaveChangesAsync()
        {
            return _dbContext.SaveChangesAsync();
        }

        public async Task<int> SaveChangesAsyncResult()
        {
            return await _dbContext.SaveChangesAsync();
        }
    }
}
