using Ewamall.Domain.Entities;
using Ewamall.Domain.Shared;

namespace Ewamall.WebAPI.Services
{
    public interface IBaseSetupService<T> where T : class
    {
        public Task<Result<IEnumerable<T>>> GetAllAsync();
        public Task<Result<T>> AddAsync(T entity);
        public Task<Result<T>> UpdateAsync(int id, T entity);
        public Task<Result<T>> DeleteAsync(int id);
    }
}
