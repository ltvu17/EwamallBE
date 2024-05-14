using Ewamall.Domain.Primitives;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ewamall.Domain.Entities
{
    public sealed class Industry : Entity
    {
        protected Industry()
        {
            
        }
        public Industry(int id, string industryName, bool isActive, int level, bool isLeaf, string path, Industry? parentNode) : base(id)
        {
            IndustryName = industryName;
            IsActive = isActive;
            Level = level;
            IsLeaf = isLeaf;
            Path = path;
            ParentNode = parentNode;
        }

        public string IndustryName { get; set; }
        public bool IsActive { get; set; }
        public int Level { get; set; }
        public bool IsLeaf { get; set; }
        public string Path { get; set; }
        public Industry? ParentNode { get; set; }
        public IEnumerable<IndustryDetail> IndustryDetails { get; set; }
    }
}
