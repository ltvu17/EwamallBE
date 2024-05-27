using AutoMapper;
using Ewamall.Business.IRepository;
using Ewamall.Domain.Entities;
using Ewamall.Domain.Shared;

namespace Ewamall.WebAPI.Services.Implements
{
    public class WalletService : IWalletService
    {
        private readonly IWalletRepo _walletRepo;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IEmailSender _emailSender;
        private readonly IMapper _mapper;
        public WalletService(IWalletRepo walletRepo, IUnitOfWork unitOfWork, IEmailSender emailSender, IMapper mapper)
        {
            _emailSender = emailSender;
            _walletRepo = walletRepo;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<Result<Wallet>> DepositBalance(int sellerId, float amount)
        {
            var wallet = await _walletRepo.GetByIdAsync(sellerId);
            if (wallet is null)
            {
                return Result.Failure<Wallet>(new Error("Get balance", "Wallet of seller not existed"));
            }
            wallet.Balance += amount;
            await _walletRepo.UpdateAsync(wallet);
            await _unitOfWork.SaveChangesAsync();
            return wallet;
        }

        public async Task<Result<Wallet>> GetBalance(int sellerId)
        {
            var wallet = await _walletRepo.GetByIdAsync(sellerId);
            if (wallet is null)
            {
                return Result.Failure<Wallet>(new Error("Get balance", "Wallet of seller not existed"));
            }
            return wallet;
        }

        public async Task<Result<Wallet>> WithdrawBalance(int sellerId, float amount)
        {
            var wallet = await _walletRepo.GetByIdAsync(sellerId);
            if (wallet is null)
            {
                return Result.Failure<Wallet>(new Error("Get balance", "Wallet of seller not existed"));
            }
            if ( wallet.Balance < amount)
            {
                return Result.Failure<Wallet>(new Error("Withdraw balance", "Insufficient balance for payment"));
            }
            wallet.Balance -= amount;
            await _walletRepo.UpdateAsync(wallet);
            await _unitOfWork.SaveChangesAsync();
            return wallet;
        }
    }
}
