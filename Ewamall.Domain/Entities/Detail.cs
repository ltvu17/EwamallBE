using Ewamall.Domain.Primitives;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ewamall.Domain.Entities
{
    public sealed class Detail : Entity
    {
        protected Detail()
        {
            
        }
        public Detail(int id,string detailName, string detailDescription) : base(id)
        {
            DetailName = detailName;
            DetailDescription = detailDescription;
        }

        public string DetailName { get; set; }
        public string DetailDescription { get; set; }
        public IEnumerable<IndustryDetail> IndustryDetails { get; set; }
        public IEnumerable<ProductDetail> ProductDetails { get; set; }
    }
}
