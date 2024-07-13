using AutoMapper;
using Ewamall.Business.Enums;
using Ewamall.Business.IRepository;
using Ewamall.DataAccess.Repository;
using Ewamall.Domain.Entities;
using Ewamall.Domain.IRepository;
using Ewamall.Domain.Shared;
using Ewamall.Infrastructure.Repository;
using Ewamall.WebAPI.DTOs;
using System.Security.Principal;

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
                        <p>Vui lòng nhấn vào <a href='https://ewamallbe.onrender.com/api/Account/ConfirmAccount/{account.Email}'>liên kết này</a> để xác thực tài khoản.</p>
                    </body>
                    </html>
                ";
            /*            string htmlMessage = $@" <div style=""max-width: 600px; margin: 20px auto; padding: 20px; background-color: #fff; border: 1px solid #ddd; border-radius: 5px;"">
                    <h2 style=""background-color: #E9BB45; color: #242058; text-align: center; padding: 10px 0;"">Giao dịch thành công</h2>
                    <p>Đơn hàng của bạn đã được nhận và hiện đang được xử lý. Chi tiết đơn hàng của bạn được hiển thị bên dưới để bạn tham khảo:</p>
                    <h3>Order: #OrderCode</h3>
                    <table style=""width: 100%; border-collapse: collapse;"">
                        <tr>
                            <th style=""border: 1px solid #ddd; padding: 8px; text-align: left;"">Sản phẩm</th>
                            <th style=""border: 1px solid #ddd; padding: 8px; text-align: left;"">Số lượng</th>
                            <th style=""border: 1px solid #ddd; padding: 8px; text-align: left;"">Giá tiền</th>
                        </tr>
                        <tr>
                            <td style=""border: 1px solid #ddd; padding: 8px;"">Tên sản phẩm</td>
                            <td style=""border: 1px solid #ddd; padding: 8px;"">1</td>
                            <td style=""border: 1px solid #ddd; padding: 8px;"">$99.99</td>
                        </tr>
                        <tr>
                            <td colspan=""2"" style=""border: 1px solid #ddd; padding: 8px; text-align: right;"">Phương thức thanh toán:</td>
                            <td style=""border: 1px solid #ddd; padding: 8px;"">Ship code</td>
                        </tr>
                        <tr>
                            <td colspan=""2"" style=""border: 1px solid #ddd; padding: 8px; text-align: right; font-weight: bold;"">Order Total:</td>
                            <td style=""border: 1px solid #ddd; padding: 8px;"">$99.99</td>
                        </tr>
                    </table>
                    <h3>Customer details</h3>
                    <p>Email: Email của customer<br>Sdt: 00000000000</p>
                   <h3>Địa chỉ ship</h3>
                    <p>Walter White<br>308 Negra Arroyo Lane<br>Albuquerque, New Mexico 87104</p>

                </div>";*/

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
                request.ProvinceId,
                request.DistrictId,
                request.WardId,
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

        public async Task<Result<Account>> UpdateAccountPassword(int accountId, UpdateUserAccount request)
        {
            var account = await _accountRepo.GetByIdAsync(accountId);
            if(account is null)
            {
                return Result.Failure<Account>(new Error("Update Password", "Can not find account"));
            }
            var updateUserAccount = _mapper.Map(request, account);
            updateUserAccount.Updated = DateTime.UtcNow;

            await _accountRepo.UpdateAsync(updateUserAccount);
            await _unitOfWork.SaveChangesAsync();
            return updateUserAccount;
        }

        public async Task<Result<Seller>> UpdateSellerAccount(int sellerId, CreateSeller request)
        {
            var seller = await _sellerRepo.GetByIdAsync(sellerId);
            if(seller is null)
            {
                return Result.Failure<Seller>(new Error("Update seller", "can not find seller"));
            }
            var updateSeller = _mapper.Map(request, seller);
            await _sellerRepo.UpdateAsync(updateSeller);
            await _unitOfWork.SaveChangesAsync();
            return updateSeller;
        }

        public async Task<Result<User>> UpdateUserInformation(int userId, UserInformation request)
        { 
            var user = await _userRepo.GetByIdAsync(userId);
            if (user is null)
            {
                return Result.Failure<User>(new Error("Update User", "Can not find user"));
            }
            var updateUser = _mapper.Map(request, user);
            await _userRepo.UpdateAsync(updateUser);
            await _unitOfWork.SaveChangesAsync();
            return updateUser;
        }

        public async Task<Result<User>> GetUserInformationById(int userId)
        {
            var user = await _userRepo.GetByIdAsync(userId);
            if (user is null)
            {
                return Result.Failure<User>(new Error("Get User Infor", "Can not find user"));
            }
            return user;
        }

        public async Task<Result<Seller>> GetSellerById(int sellerId)
        {
            var seller = await _sellerRepo.GetByIdAsync(sellerId);
            if (seller is null)
            {
                return Result.Failure<Seller>(new Error("Get Seller Infor", "Can not find seller"));
            }
            return seller;
        }
    }
}
