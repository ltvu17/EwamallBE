using Ewamall.Domain.Primitives;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ewamall.Domain.Entities
{
    public sealed class ShipAddress : Entity
    {
        protected ShipAddress()
        {
            
        }
        public ShipAddress(int id, string name, string address, string phoneNumber, bool isDefault, User user) : base(id) 
        {
            Name = name;
            Address = address;
            PhoneNumber = phoneNumber;
            IsDefault = isDefault;
            User = user;
        }

        public string Name { get; set; }
        public string Address { get; set; }
        public string PhoneNumber { get; set; }
        public bool IsDefault { get; set; }
        public User User { get; set; }
    }
}
