using Ewamall.Domain.Primitives;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ewamall.Domain.Entities
{
    public sealed class ProductDetail : Entity
    {
        protected ProductDetail()
        {
            
        }
        internal ProductDetail(Product product, int detailId, string description)
        {
            Product = product;
            DetailId = detailId;
            Description = description;
        }
       
        public Product Product { get; private set; }
        [ForeignKey("Detail")]
        public int DetailId { get; private set; }
        public Detail Detail { get; private set; }
        public string Description { get; private set; }

        internal static ProductDetail Create(Product product, int detailId, string description)
        {
            var productDetail = new ProductDetail(product, detailId, description);
            return productDetail;
        }
    }
}
