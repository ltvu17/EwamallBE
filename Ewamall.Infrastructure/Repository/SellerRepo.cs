using Ewamall.Business.IRepository;
using Ewamall.Domain.Entities;
using Ewamall.Infrastructure.Dbcontext;
using Ewamall.Infrastructure.Repository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Ewamall.DataAccess.Repository
{
    public class SellerRepo : BaseRepo<Seller>, ISellerRepo
    {
        private readonly EwamallDBContext _context;
        public SellerRepo(EwamallDBContext context) : base(context)
        {
            _context = context;
        }
        public override async Task<Seller> GetByIdAsync(int id)
        {
            return await _context.Sellers.Where(s => s.Id == id).Include(s => s.Wallet).FirstOrDefaultAsync();
        }
        public bool IsEmailExist(string email)
        {
            return _context.Sellers.Any(s => s.Email == email);
        }

        public bool IsPhoneExist(string phone)
        {
            return _context.Sellers.Any(s => s.PhoneNumber == phone);
        }
    }
}
