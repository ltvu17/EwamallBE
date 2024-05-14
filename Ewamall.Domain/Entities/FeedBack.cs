using Ewamall.Domain.Primitives;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ewamall.Domain.Entities
{
    public sealed class FeedBack : Entity
    {
        public FeedBack()
        {
            
        }
        public FeedBack(int id, int rate, string? quality, string? correctWithSellProduct, string description, DateTime createDate, DateTime? updateDate, User user, ProductSellDetail productSellDetail) : base(id)
        {
            Rate = rate;
            Quality = quality;
            CorrectWithSellProduct = correctWithSellProduct;
            Description = description;
            CreateDate = createDate;
            UpdateDate = updateDate;
            User = user;
            ProductSellDetail = productSellDetail;
        }

        public int Rate { get; set; }
        public string? Quality { get; set; }
        public string? CorrectWithSellProduct { get; set; }
        public string Description { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime? UpdateDate { get; set; }
        public User User { get; set; }
        public ProductSellDetail ProductSellDetail { get; set; }
    }
}
