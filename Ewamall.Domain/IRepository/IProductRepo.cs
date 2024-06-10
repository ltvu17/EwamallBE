using Ewamall.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ewamall.Domain.IRepository
{
    public interface IProductRepo : IBaseRepo<Product>
    {
        public Task<IEnumerable<ProductDTO>> GetAllDTOAsync();
        public Task<IEnumerable<ProductDTO>> GetAllDTOByIndustryIdAsync(int industryId);
        public Task<IEnumerable<ProductDTO>> GetAllDTOBySellerIdAsync(int sellerId);
    }
    public class ProductDTO
    {
        public int Id { get; set; } 
        public string ProductName { get;  set; }
        public string ProductDescription { get;  set; }
        public Guid CoverImageId { get;  set; }
        public Guid ImagesId { get;  set; }
        public Guid VideoId { get;  set; }
        public int IndustryId { get;  set; }
        public Industry Industry { get;  set; }
        public IEnumerable<ProductDetail> ProductSellDetails { get;  set; }
        public IEnumerable<ProductSellDetail> ProductSellerDetails { get;  set; }
        public int SellerId { get;  set; }
        public string SellerName { get;  set; }
        public float? MinPrice { get;  set; }
        public string SellerAddress { get; set; }
        public int? TotalQuantity { get; set; }
        public int ProductStatus { get; set; }
    }
}
