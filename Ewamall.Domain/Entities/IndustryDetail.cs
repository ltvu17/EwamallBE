using Ewamall.Domain.Primitives;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ewamall.Domain.Entities
{
    public sealed class IndustryDetail : Entity
    {
        protected IndustryDetail()
        {
            
        }
        public IndustryDetail(int id, Detail detail, Industry industry) : base(id)
        {
            Detail = detail;
            Industry = industry;
        }

        public Detail Detail { get; set; }
        public Industry Industry { get; set; }

    }
}
