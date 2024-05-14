using Ewamall.Domain.Primitives;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ewamall.Domain.Entities
{
    public sealed class Payment : Entity
    {
        protected Payment()
        {
            
        }
        public Payment(int id, string name, string description, int? paymentType) : base(id)
        {
            Name = name;
            Description = description;
            PaymentType = paymentType;
        }

        public string Name { get; set; }
        public string Description { get; set; }
        public int? PaymentType { get; set; }
        public IEnumerable<Order> Orders { get; set; }
    }
}
