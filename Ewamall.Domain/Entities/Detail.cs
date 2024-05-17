using Ewamall.Domain.Primitives;
using Ewamall.Domain.Shared;
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
        public Detail(string detailName, string detailDescription) 
        {
            DetailName = detailName;
            DetailDescription = detailDescription;
        }

        public string DetailName { get; set; }
        public string DetailDescription { get; set; }
        public IEnumerable<IndustryDetail> IndustryDetails { get; set; }
        public IEnumerable<ProductDetail> ProductDetails { get; set; }
        internal static Result<Detail> Create(string detailName, string detailDescription)
        {
            if( string.IsNullOrEmpty(detailName) && string.IsNullOrEmpty(detailDescription))
            {
                return Result.Failure<Detail>(new Error("Detail.Create()", "Create detail error"));
            }
            return new Detail(detailName, detailDescription);
        }
    }
}
