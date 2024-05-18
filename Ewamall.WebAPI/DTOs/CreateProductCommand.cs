using Ewamall.Domain.Entities;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ewamall.WebAPI.DTOs
{
    public class CreateProductCommand
    {
        public string ProductName { get;  set; }
        public string ProductDescription { get;  set; }
        public Guid CoverImageId { get;  set; }
        public Guid ImagesId { get;  set; }
        public Guid VideoId { get;  set; }
        public int IndustryId { get;  set; }
        public int SellerId { get;  set; }
        public List<ProductDetailCommand> ProductSellDetails { get; set; }
        public List<ProductSellCommand> ProductSellCommand { get; set; }
    }
    public class ProductDetailCommand
    {
        public int ProductId { get;  set; }
        public int DetailId { get;  set;}
        public string Description { get;  set; }
    }
    public class ProductSellCommand
    {
        public string Name { get; set; }
        public float? Price { get; set; }
        public int InventoryNumber { get; set; }
        public string Path { get;  set; }
        public int? ParentNodeId { get;  set; }
    }
}
