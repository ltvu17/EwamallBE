using Ewamall.Business.IRepository;
using Ewamall.Domain.Entities;
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
    public class UserRepo : BaseRepo<User>, IUserRepo
    {
        private readonly EwamallDBContext _context;
        public UserRepo(EwamallDBContext context) : base(context)
        {
            _context = context;
        }

        public override async Task<User> GetByIdAsync(int id)
        {
            return await _context.Users.Where(s => s.Id == id).Include(s => s.Account).Include(s => s.Seller).FirstOrDefaultAsync();
        }

        public async Task<Seller> GetSellerByProduct(int productDetailId)
        {
            return await _context.ProductSellDetails.Where(s=>s.Id == productDetailId).Select(s=>s.Product.Seller).FirstOrDefaultAsync();
        }

        public bool IsUserExist(int userId)
        {
            return _context.Users.Any(s => s.Id == userId);
        }
    }
}
