﻿using AutoMapper;
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
        private readonly IUserRepo _userRepo;
        private readonly ISellerRepo _sellerRepo;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IEmailSender _emailSender;
        private readonly IMapper _mapper;
        public AccountService(IAccountRepo accountRepo, IUnitOfWork unitOfWork, IMapper mapper, IUserRepo userRepo, ISellerRepo sellerRepo, IEmailSender emailSender)
        {
            _accountRepo = accountRepo;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _userRepo = userRepo;
            _sellerRepo = sellerRepo;
            _emailSender = emailSender;
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

            // Tạo message HTML
            string htmlMessage = $@"
        <!DOCTYPE html>
        <html>
        <head>
            <title>Xác thực tài khoản</title>
        </head>
        <body>
            <h2>Xin chào, {account.User.Name}!</h2>
            <p>Bạn đã tạo tài khoản thành công cho dịch vụ EWaMall của chúng tôi.</p>
            <p>Vui lòng nhấn vào <a href='https://localhost:7289/api/Account/ConfirmAccount/{account.Email}'>liên kết này</a> để xác thực tài khoản.</p>
        </body>
        </html>
    ";

            await _emailSender.SendEmailAsync(request.Email, "Xác thực đăng kí tài khoản EWaMall", htmlMessage);

            return account;
        }

        public async Task<Result<bool>> ConfirmAccount(string email)
        {
            var checkEmailExist = _accountRepo.IsEmailExist( email );

            if (!checkEmailExist)
            {
                return Result.Failure<bool>(new Error("Confirm account", "Email not existed!"));
            }
            var result = _accountRepo.ConfirmAccount(email);
            return result;
        }

        public async Task<Result<AuthenticationResponse>> Login(Authentication request)
        {
            var result =  _accountRepo.GetAccountLogin(request.Email, request.Password);
            var loginResponse = _mapper.Map<AuthenticationResponse>(result);

            if (result is null)
            {
                return Result.Failure<AuthenticationResponse>(new Error("User login", "Fail to login account"));
            }
            if (!result.IsActive)
            {
                return Result.Failure<AuthenticationResponse>(new Error("User login", "This account inactive"));
            }
            return loginResponse;
        }

        public async Task<Result<Seller>> RegisterSeller(int userId, CreateSeller request)
        {
            var user = await _userRepo.GetByIdAsync(userId);

            if (user is null)
            {
                return Result.Failure<Seller>(new Error("Register Seller", "Can not find user"));
            }

            var sellerResult = Seller.Create(
                request.ShopName,
                request.Address,
                request.PhoneNumber,
                request.Email,
                request.Description,
                user
                );
            var seller = sellerResult.Value;
            seller.AddWallet(
                0
                );
            await _sellerRepo.AddAsync( seller );
            await _unitOfWork.SaveChangesAsync();
            // update role
            var account = await _accountRepo.GetByIdAsync(seller.User.AccountId);
            if (account is null)
            {
                return Result.Failure<Seller>(new Error("Update role for user", "Can not find account"));
            }
            account.RoleId = 3;
            await _accountRepo.UpdateAsync( account );
            await _unitOfWork.SaveChangesAsync();

            return seller;
        }
    }
}
