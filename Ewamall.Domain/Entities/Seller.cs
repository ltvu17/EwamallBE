using Ewamall.Domain.Primitives;
using Ewamall.Domain.Shared;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
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
        public Seller(string shopName, string address, string phoneNumber, string email, string description, User user)
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
        public Wallet? Wallet { get; set; }
        [ForeignKey("User")]
        public int UserId { get; set; }
        public User User { get; set; }
        [JsonIgnore]
        public virtual IEnumerable<Product> Products { get; set; }

        public static Result<Seller> Create(string shopName, string address, string phoneNumber, string email, string description, User user)
        {
            var seller = new Seller(shopName, address, phoneNumber, email, description, user);
            return seller;
        }
        public Result<Seller> AddWallet(float balance)
        {
            var result = Wallet.Create(balance, this);
            if (result.IsFailure)
            {
                return Result.Failure<Seller>(result.Error);
            }
            Wallet = result.Value;
            return this;
        }
    }
}
