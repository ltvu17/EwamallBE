using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Ewamall.Domain.IRepository
{
    public interface IBaseRepo<T> where T : class
    {
        public Task<bool> AddAsync(T entity);


        public Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> expression, int entry, int page, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
                        string includeProperties = "");

        public Task<IEnumerable<T>> GetAllAsync();

        public Task<T> GetByIdAsync(int id);

        public Task<bool> RemoveAsync(int id);
        public Task<bool> RemoveEntityAsync(T entity);
        public Task<bool> RemoveRange(IEnumerable<T> entities);
        public Task<bool> UpdateAsync(T entity);
    }
}
