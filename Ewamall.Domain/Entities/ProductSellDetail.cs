using Ewamall.Domain.Primitives;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ewamall.Domain.Entities
{
    public sealed class ProductSellDetail : Entity
    {
        protected ProductSellDetail()
        {
            
        }
        public ProductSellDetail(int id, string name, float? price, string? inventory, int inventoryNumber, string path, ProductSellDetail? parentNode, Product product) : base(id)
        {
            Name = name;
            Price = price;
            Inventory = inventory;
            InventoryNumber = inventoryNumber;
            Path = path;
            ParentNode = parentNode;
            Product = product;
        }

        public string Name { get; set; }
        public float? Price { get; set; }
        public string? Inventory { get; set; }
        public int InventoryNumber { get; set; }
        public string Path { get; set; }
        public ProductSellDetail? ParentNode { get; set; }
        [ForeignKey("Product")]
        public int ProductId { get; set; }
        public Product Product { get; set; }
        public IEnumerable<FeedBack> FeedBacks { get; set; }
        public IEnumerable<Cart> Carts { get; set; }
    }
}
