using Ewamall.Domain.Primitives;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ewamall.Domain.Entities
{
    public sealed class Role: Entity
    {
        protected Role()
        {
            
        }
        public Role(int id, string roleName, string description) : base(id)
        {
            RoleName = roleName;
            Description = description;
        }

        public string RoleName { get; set; }
        public string Description { get; set; }
        public IEnumerable<Account> Accounts { get; set; }    
    }
}
