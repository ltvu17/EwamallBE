using Ewamall.Domain.Entities;
using Ewamall.Domain.Shared;
using Ewamall.WebAPI.DTOs;

namespace Ewamall.WebAPI.Services
{
    public interface IAccountService
    {
        public Task<Result<IEnumerable<Account>>> GetAllAccount();
        public Task<Result<Account>> CreateUserAccount(CreateUserAccount request);
        public Task<Result<bool>> ConfirmAccount(string email);
        public Task<Result<AuthenticationResponse>> Login(Authentication request);
        public Task<Result<Seller>> RegisterSeller(int userId, CreateSeller request);
        public Task<Result<Account>> UpdateAccountPassword(int accountId, UpdateUserAccount request);
        public Task<Result<Seller>> UpdateSellerAccount(int sellerId, CreateSeller request);
        public Task<Result<User>> UpdateUserInformation(int userId, UserInformation request);
        public Task<Result<User>> GetUserInformationById(int userId);
        public Task<Result<Seller>> GetSellerById(int sellerId);
    }
}
