using Ewamall.Domain.Primitives;
using Ewamall.Domain.Shared;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ewamall.Domain.Entities
{
    public sealed class Product : Entity
    {
        internal List<ProductDetail> _productSellDetails = new List<ProductDetail>();
        internal List<ProductSellDetail> _productSellerDetails = new List<ProductSellDetail>();
        protected Product()
        {
            
        }
        internal Product(string productName, string productDescription, Guid coverImageId, Guid imagesId, Guid videoId, int industryId, int sellerId)
        {
            ProductName = productName;
            ProductDescription = productDescription;
            CoverImageId = coverImageId;
            ImagesId = imagesId;
            VideoId = videoId;
            IndustryId = industryId;
            SellerId = sellerId;
        }

        public string ProductName { get; private set; }
        public string ProductDescription { get; private set; }
        public Guid CoverImageId { get; private set; }  
        public Guid ImagesId { get; private set; }
        public Guid VideoId { get; private set; }
        [ForeignKey("Industry")]
        public int IndustryId { get; private set; }
        public Industry Industry { get; private set; }
        public IEnumerable<ProductDetail> ProductSellDetails => _productSellDetails;
        public IEnumerable<ProductSellDetail> ProductSellerDetails => _productSellerDetails;
        [ForeignKey("Seller")]
        public int SellerId { get; private set; }
        public Seller Seller { get; set; }
        public int ProductStatus { get; private set; } = 1;

        public static Result<Product> Create(string productName, string productDescription, Guid coverImageId, Guid imagesId, Guid videoId, int industryId, int sellerId)
        {
            var product = new Product(productName,productDescription, coverImageId, imagesId, videoId ,industryId, sellerId);
            return product;
        }
        public Result<Product> AddProductDetail(int productDetailId, string description)
        {
            _productSellDetails.Add(ProductDetail.Create(this, productDetailId, description));
            return this;
        }
        public Result<Product> AddProductSellDetail(IEnumerable<ProductSellDetail> items)
        {
            _productSellerDetails.AddRange(items);
            return this;
        }
        public Result<Product> ChangeStatusProduct(int status)
        {
            ProductStatus = status;
            return this;
        }
    }
}
 