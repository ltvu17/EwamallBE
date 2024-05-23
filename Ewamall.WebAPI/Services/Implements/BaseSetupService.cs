using AutoMapper;
using Ewamall.Business.Exceptions;
using Ewamall.Business.IRepository;
using Ewamall.Domain.Entities;
using Ewamall.Domain.IRepository;
using Ewamall.Domain.Shared;
using Microsoft.AspNetCore.Authentication;

namespace Ewamall.WebAPI.Services.Implements
{
    public class BaseSetupService<T> : IBaseSetupService<T> where T : class
    {
        private readonly IBaseRepo<T> _baseRepo;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private bool access = false;

        public BaseSetupService(IBaseRepo<T> baseRepo, IUnitOfWork unitOfWork, IMapper mapper)
        {
            if(typeof(T).Equals(typeof(Payment))
                || typeof(T).Equals(typeof(OrderStatus))
               ){
                access = true;
            }
            if (!access)
            {
                throw new AccessNotAllowException("Access Deny");
            }

            _baseRepo = baseRepo;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Result<T>> AddAsync(T entity)
        {
            if(entity == null)
            {
                return Result.Failure<T>(new Error("AddAsync", "No entity"));
            }
            await _baseRepo.AddAsync(entity);
            await _unitOfWork.SaveChangesAsync();
            return entity;
        }

        public async Task<Result<T>> DeleteAsync(int id)
        {
            var entity = await _baseRepo.GetByIdAsync(id);
            await _baseRepo.RemoveEntityAsync(entity);
            await _unitOfWork.SaveChangesAsync();
            return entity;
        }

        public async Task<Result<IEnumerable<T>>> GetAllAsync()
        {
            var result = (await _baseRepo.GetAllAsync()).ToList();
            if(result.Count == 0)
            {
                return Result.Failure<IEnumerable<T>>(new Error("AddAsync", "Not found items"));
            }
            return result;
        }

        public async Task<Result<T>> UpdateAsync(int id, T entity)
        {
            var oldEntity = await _baseRepo.GetByIdAsync(id);
            oldEntity = _mapper.Map(entity,oldEntity);
            await _baseRepo.UpdateAsync(oldEntity);
            await _unitOfWork.SaveChangesAsync();
            return Result.Success<T>(oldEntity);
        }
    }
}
