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
    public sealed class IndustryDetail : Entity
    {
        protected IndustryDetail()
        {
            
        }
        internal IndustryDetail(int detailId, Industry industry)
        {
            DetailId = detailId;
            Industry = industry;
        }
        internal IndustryDetail(Detail detail, Industry industry)
        {
            Detail = detail;
            Industry = industry;
        }
        [ForeignKey("Detail")]
        public int DetailId { get; private set; }
        public Detail Detail { get; private set; }
        [ForeignKey("Industry")]
        public int IndustryId { get; private set; }
        public Industry Industry { get; private set; }

        internal static IndustryDetail Create(int detailId, Industry industry)
        {
            return new IndustryDetail(detailId, industry);
        }
        internal static Result<IndustryDetail> Create(Detail detail, Industry industry)
        {
            return new IndustryDetail(detail, industry);
        }

    }
}
