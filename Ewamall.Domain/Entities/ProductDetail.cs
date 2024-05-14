﻿using Ewamall.Domain.Primitives;
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
        public ProductDetail(Product product, int detailId, string description)
        {
            Product = product;
            DetailId = detailId;
            Description = description;
        }
       
        public Product Product { get; set; }
        [ForeignKey("Detail")]
        public int DetailId { get; set; }
        public Detail Detail { get; set; }
        public string Description { get; set; }

        internal static ProductDetail Create(Product product, int detailId, string description)
        {
            var productDetail = new ProductDetail(product, detailId, description);
            return productDetail;
        }
    }
}
