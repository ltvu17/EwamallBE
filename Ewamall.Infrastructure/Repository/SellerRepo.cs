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

        public async Task<IEnumerable<SellerDTO>> GetSellerDTOs()
        {
            var sellerDtos = _context.Sellers.Select(s => new SellerDTO
            {
                Address = s.Address,
                CreateDate = s.CreateDate,
                Description = s.Description,
                DistrictId = s.DistrictId,
                Email = s.Email,    
                id = s.Id,  
                PhoneNumber = s.PhoneNumber,
                ProvinceId = s.ProvinceId,
                ShopName = s.ShopName,
                WardId = s.WardId,
                Revenue = 0
            }).ToList();
            foreach (var seller in sellerDtos)
            {
                var shopRevenue = await _context.Orders.Include(s=> s.OrderDetails).ThenInclude(s=>s.ProductSellDetail).ThenInclude(s=>s.Product).ThenInclude(s=>s.Seller)
                    .Where(s=>s.OrderDetails.FirstOrDefault().ProductSellDetail.Product.Seller.Id == seller.id).ToListAsync();
                var revenue = shopRevenue.Sum(s => s.TotalCost);
                seller.Revenue = revenue;
            }
            return sellerDtos;
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
