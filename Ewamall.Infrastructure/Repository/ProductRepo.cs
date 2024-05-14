using Ewamall.Domain.Entities;
using Ewamall.Domain.IRepository;
using Ewamall.Infrastructure.Dbcontext;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ewamall.Infrastructure.Repository
{
    public class ProductRepo : BaseRepo<Product>, IProductRepo
    {
        private readonly EwamallDBContext _context;
        public ProductRepo(EwamallDBContext context) : base(context)
        {
            _context = context;
        }
        public override async Task<IEnumerable<Product>> GetAllAsync()
        {
            return await _context.Products.Include(s => s.Seller).IgnoreAutoIncludes().Include(s=>s.ProductSellDetails).Include(s=>s.Industry).AsNoTracking().ToListAsync();
        }
        public override async Task<Product> GetByIdAsync(int id)
        {
            return await _context.Products.Where(s=>s.Id == id).Include(s => s.Seller).Include(s => s.ProductSellDetails).Include(s => s.Industry).FirstOrDefaultAsync();
        }
    }
}
