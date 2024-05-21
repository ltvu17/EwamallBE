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
    public class VoucherRepo : BaseRepo<Voucher>, IVoucherRepo
    {
        private readonly EwamallDBContext _context;
        public VoucherRepo(EwamallDBContext context) : base(context)
        {
            _context = context;
        }
        public override async Task<IEnumerable<Voucher>> GetAllAsync()
        {
            return await _context.Vouchers.Include(s => s.Staff).AsNoTracking().ToListAsync();
        }
    }
}
