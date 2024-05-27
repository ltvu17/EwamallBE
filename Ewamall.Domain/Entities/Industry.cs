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
    public sealed class Industry : Entity
    {
        List<IndustryDetail> _industryDetails = new();
        protected Industry()
        {
            
        }
        internal Industry(string industryName, bool isActive, int level, bool isLeaf, string path, int? parentNodeId)
        {
            IndustryName = industryName;
            IsActive = isActive;
            Level = level;
            IsLeaf = isLeaf;
            Path = path;
            ParentNodeId = parentNodeId;
        }

        public string IndustryName { get; private set; }
        public bool IsActive { get; private set; }
        public int Level { get; private set; }
        public bool IsLeaf { get; private set; }
        public string Path { get; private set; }
        [ForeignKey("ParentNode")]
        public int? ParentNodeId { get; private set; }
        public Industry? ParentNode { get; private set; }
        public int LocalId { get; private set; }
        public IEnumerable<IndustryDetail> IndustryDetails => _industryDetails;

        public static Result<Industry> Create(string industryName, bool isActive, int level, bool isLeaf, string path, int? parentNodeId)
        {
            if(parentNodeId == 0)
            {
                parentNodeId = null;
            }
            var industry = new Industry(industryName,isActive,level,isLeaf,path,parentNodeId);
            return industry;
        }
        public Result<Industry> AddIndustryDetail(int detailId)
        {
            _industryDetails.Add(IndustryDetail.Create(detailId, this));
            return this;
        }
        public Result<Industry> AddIndustryDetailWithNewDetail(string detailName, string detailDescription)
        {
            var createDetailResult = Detail.Create(detailName, detailDescription);
            if (createDetailResult.IsFailure)
            {
                return Result.Failure<Industry>(createDetailResult.Error);
            }
            var detail = createDetailResult.Value;
            _industryDetails.Add(IndustryDetail.Create(detail, this).Value);
            return this;
        }
    }
}
