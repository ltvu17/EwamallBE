using Ewamall.Domain.Entities;
using Ewamall.Domain.IRepository;
using Ewamall.Infrastructure.Dbcontext;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
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
        public async Task<IEnumerable<ProductDTO>> GetAllDTOAsync()
        {
            return await _context.Products.Include(s => s.Seller).IgnoreAutoIncludes().Include(s=>s.ProductSellDetails).Include(s=>s.Industry).Select(s=> new ProductDTO
            {
                ProductName = s.ProductName,CoverImageId = s.CoverImageId,ImagesId = s.ImagesId,Industry = s.Industry,IndustryId = s.IndustryId,
                ProductDescription = s.ProductDescription, SellerId = s.SellerId,SellerName = s.Seller.ShopName, VideoId = s.VideoId
                ,MinPrice =s.ProductSellerDetails.Where(s=>s.Price > 0 && s.Price != null).OrderBy(s=>s.Price).FirstOrDefault().Price,
                SellerAddress = s.Seller.Address,
                Id = s.Id,
                ProductStatus= s.ProductStatus,
                TotalQuantity = s.ProductSellerDetails.Sum(s=>s.InventoryNumber)
            }).AsNoTracking().ToListAsync();
        }
        public async Task<IEnumerable<ProductDTO>> GetAllDTOBySellerIdAsync(int sellerId)
        {
            return await _context.Products.Where(s=>s.SellerId==sellerId).Include(s => s.Seller).IgnoreAutoIncludes().Include(s => s.ProductSellDetails).Include(s => s.Industry).Select(s => new ProductDTO
            {
                ProductName = s.ProductName,
                CoverImageId = s.CoverImageId,
                ImagesId = s.ImagesId,
                Industry = s.Industry,
                IndustryId = s.IndustryId,
                ProductDescription = s.ProductDescription,
                SellerId = s.SellerId,
                SellerName = s.Seller.ShopName,
                VideoId = s.VideoId
                ,
                ProductStatus = s.ProductStatus,
                MinPrice = s.ProductSellerDetails.Where(s => s.Price > 0 && s.Price != null).OrderBy(s => s.Price).FirstOrDefault().Price,
                SellerAddress = s.Seller.Address,
                Id = s.Id,
                TotalQuantity = s.ProductSellerDetails.Sum(s=>s.InventoryNumber),
            }).AsNoTracking().ToListAsync();
        }

        public async Task<IEnumerable<ProductDTO>> GetAllDTOByIndustryIdAsync(int industryId)
        {
            return await _context.Products.Include(s => s.Seller).IgnoreAutoIncludes().Include(s => s.ProductSellDetails).Include(s => s.Industry).Where(s=>s.IndustryId == industryId).Select(s => new ProductDTO
            {
                ProductName = s.ProductName,
                CoverImageId = s.CoverImageId,
                ImagesId = s.ImagesId,
                Industry = s.Industry,
                IndustryId = s.IndustryId,
                ProductDescription = s.ProductDescription,
                SellerId = s.SellerId,
                SellerName = s.Seller.ShopName,
                VideoId = s.VideoId,
                SellerAddress = s.Seller.Address,
                MinPrice = s.ProductSellerDetails.Where(s => s.Price > 0 && s.Price != null).OrderBy(s => s.Price).FirstOrDefault().Price,
                Id = s.Id,
            }).AsNoTracking().ToListAsync();
        }

        public override async Task<Product> GetByIdAsync(int id)
        {
            return await _context.Products.Where(s=>s.Id == id).Include(s => s.Seller).Include(s=>s.ProductSellerDetails).Include(s => s.ProductSellDetails).Include(s => s.Industry).FirstOrDefaultAsync();
        }
    }
}
