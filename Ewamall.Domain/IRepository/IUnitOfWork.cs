using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ewamall.Business.IRepository
{
    public interface IUnitOfWork
    {
        public Task SaveChangesAsync();
        public Task<int> SaveChangesAsyncResult();
    }
}
