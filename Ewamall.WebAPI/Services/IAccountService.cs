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
    }
}
