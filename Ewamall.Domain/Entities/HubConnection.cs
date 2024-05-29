using Ewamall.Domain.Primitives;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ewamall.Business.Entities
{
    public class HubConnection : Entity
    {


        public HubConnection()
        {
        }
        public string ConnectionId { get; set; } = null!;
        public string Username { get; set; } = null!;
        public int RoleId { get; set; }
    }
}
