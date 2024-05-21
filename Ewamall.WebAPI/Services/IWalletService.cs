using Ewamall.Domain.Entities;
using Ewamall.Domain.Shared;

namespace Ewamall.WebAPI.Services
{
    public interface IWalletService
    {
        public Task<Result<Wallet>> GetBalance(int sellerId);
        public Task<Result<Wallet>> DepositBalance(int sellerId, float amount);
        public Task<Result<Wallet>> WithdrawBalance(int sellerId, float amount);
    }
}
