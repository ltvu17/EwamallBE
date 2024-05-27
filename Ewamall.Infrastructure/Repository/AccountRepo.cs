using Ewamall.Business.IRepository;
using Ewamall.Domain.Entities;
using Ewamall.Domain.IRepository;
using Ewamall.Infrastructure.Dbcontext;
using Ewamall.Infrastructure.Repository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ewamall.DataAccess.Repository
{
    public class AccountRepo : BaseRepo<Account>, IAccountRepo
    {
        private readonly EwamallDBContext _context;
        public AccountRepo(EwamallDBContext context) : base(context)
        {
            _context = context;
        }
        public Account GetAccountLogin(string email, string password)
        {
            var account = _context.Accounts.Include(s=>s.Role).Include(s=>s.User).AsNoTracking()
                             .Where(s => s.Email == email && s.Password == password).FirstOrDefault();
            return account;
        }
        public override async Task<IEnumerable<Account>> GetAllAsync()
        {
            return await _context.Accounts.Include(s => s.Role).AsNoTracking().ToListAsync();
        }
        public bool IsEmailExist(string email)
        {
            return _context.Accounts.Any(s => s.Email == email);
        }
        public bool ConfirmAccount(string email)
        {
            var account = _context.Accounts.FirstOrDefault(x => x.Email == email);
            account.IsActive = true;
            _context.SaveChanges();
            return true;
        }

        public bool IsPhoneExist(string phone)
        {
            return _context.Accounts.Any(s => s.PhoneNumber == phone);
        }
        public override async Task<Account> GetByIdAsync(int id)
        {
            return await _context.Accounts.Where(s => s.Id == id).FirstOrDefaultAsync();
        }
    }
}
