using Ewamall.Domain.Primitives;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Ewamall.Domain.Entities
{
    public sealed class OrderStatus : Entity
    {
        protected OrderStatus()
        {
            
        }
        public OrderStatus(int id, string name, string description) : base(id)
        {
            Name = name;
            Description = description;
        }

        public string Name { get; set; }
        public string Description { get; set; }
        [JsonIgnore]
        public IEnumerable<Order>? Orders { get; set; }
    }
}
