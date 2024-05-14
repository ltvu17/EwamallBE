using Ewamall.Domain.Primitives;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Ewamall.Domain.Entities
{
    public class Seller : Entity
    {
        protected Seller()
        {
            
        }
        public Seller(int id, string shopName, string address, string phoneNumber, string email, string description, User user) : base(id)
        {
            ShopName = shopName;
            Address = address;
            PhoneNumber = phoneNumber;
            Email = email;
            Description = description;
            User = user;
        }

        public string ShopName { get; set; }
        public string Address { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string Description { get; set; }
        public User User { get; set; }
        [JsonIgnore]
        public virtual IEnumerable<Product> Products { get; set; }
    }
}
