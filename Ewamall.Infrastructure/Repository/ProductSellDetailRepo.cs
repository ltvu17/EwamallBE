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
    public class ProductSellDetailRepo : BaseRepo<ProductSellDetail>, IProductSellDetailRepo
    {
        private readonly EwamallDBContext _context;
        public ProductSellDetailRepo(EwamallDBContext context) : base(context)
        {
            _context = context;
        }
        public override async Task<ProductSellDetail> GetByIdAsync(int id)
        {
            return await _context.ProductSellDetails.Where(s => s.Id == id).Include(s => s.Product).FirstOrDefaultAsync();
        }
    }
}
