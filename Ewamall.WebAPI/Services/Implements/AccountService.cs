using AutoMapper;
using Ewamall.Business.Enums;
using Ewamall.Business.IRepository;
using Ewamall.Domain.Entities;
using Ewamall.Domain.IRepository;
using Ewamall.Domain.Shared;
using Ewamall.Infrastructure.Repository;
using Ewamall.WebAPI.DTOs;

namespace Ewamall.WebAPI.Services.Implements
{
    public class AccountService : IAccountService
    {
        private readonly IAccountRepo _accountRepo;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public AccountService(IAccountRepo accountRepo, IUnitOfWork unitOfWork, IMapper mapper)
        {
            _accountRepo = accountRepo;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<Result<IEnumerable<Account>>> GetAllAccount()
        {
            Result<IEnumerable<Account>> result = (await _accountRepo.GetAllAsync()).ToList();
            if (result.IsFailure)
            {
                return Result.Failure<IEnumerable<Account>>(new Error("IEnumerable<Account>.GetAll()", "Fail to load account"));
            }
            return result;
        }

        public async Task<Result<Account>> CreateUserAccount(CreateUserAccount request)
        {
            var resultAccount = Account.Create(
            request.Email,
            request.Password,
            request.EmailConfirmed,
            request.PhoneNumber,
            DateTime.Now,
            DateTime.Now,
            4
            );
            if (resultAccount.IsFailure)
            {
                return Result.Failure<Account>(new Error("Create account", "Fail to create account"));
            }
            var account = resultAccount.Value;
            account.AddUser(
                request.UserInformation.Name,
                request.UserInformation.DateOfBirth,
                Gender.ToString(request.UserInformation.Gender),
                request.UserInformation.Address,
                request.UserInformation.ImageId
                );
            await _accountRepo.AddAsync( account );
            await _unitOfWork.SaveChangesAsync();
            return account;
        }

        public async Task<Result<bool>> ConfirmAccount(string email)
        {
            var result = _accountRepo.ConfirmAccount(email);
            if (!result)
            {
                return (Result<bool>)Result.Failure(new Error("Confirm account", "Fail to confirm account"));
            }
            return Result.Success(true);
        }

        public Task<Result<bool>> Login(Login request)
        {
            throw new NotImplementedException();
        }
    }
}
