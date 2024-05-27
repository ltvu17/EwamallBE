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

        internal ProductSellDetail(string name, float? price, int inventoryNumber, Product product)
        {
            Name = name;
            Price = price;
            InventoryNumber = inventoryNumber;
            Product = product;
        }

        public string Name { get; private set; }
        public float? Price { get; private set; }
        public string? Inventory { get; private set; }
        public int InventoryNumber { get; private set; }
        public string Path { get; private set; }
        [ForeignKey("ParentNode")]
        public int? ParentNodeId { get; private set; }
        public ProductSellDetail? ParentNode { get; private set; }
        public int LocalId { get; private set; }
        [ForeignKey("Product")]
        public int ProductId { get; private set; }
        public Product Product { get; private set; }
        public IEnumerable<FeedBack> FeedBacks { get; private set; }
        public IEnumerable<Cart> Carts { get; private set; }

        internal static ProductSellDetail Create(string name, float? price, int inventoryNumber, Product product)
        {
            var productSellDetail = new ProductSellDetail(name, price, inventoryNumber, product);
            return productSellDetail;
        }
    }
}
