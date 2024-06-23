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
        public Seller(string shopName, string address, int provinceId, int districtId, int wardId, string phoneNumber, string email, string description, User user)
        {
            ShopName = shopName;
            Address = address;
            ProvinceId = provinceId;
            DistrictId = districtId;
            WardId = wardId;
            PhoneNumber = phoneNumber;
            Email = email;
            Description = description;
            User = user;
        }

        public string ShopName { get; set; }
        public string Address { get; set; }
        public int ProvinceId { get; set; }
        public int DistrictId { get; set; }
        public int WardId { get; set; }
        public string PhoneNumber { get; set; }
        public DateTime CreateDate { get; set; } = DateTime.Now;
        public string Email { get; set; }
        public string Description { get; set; }
        public Wallet? Wallet { get; set; }
        [ForeignKey("User")]
        public int UserId { get; set; }
        public User User { get; set; }
        [JsonIgnore]
        public virtual IEnumerable<Product> Products { get; set; }

        public static Result<Seller> Create(string shopName, string address, int provinceId, int districtId, int wardId, string phoneNumber, string email, string description, User user)
        {
            var seller = new Seller(shopName, address,provinceId, districtId, wardId, phoneNumber, email, description, user);
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
