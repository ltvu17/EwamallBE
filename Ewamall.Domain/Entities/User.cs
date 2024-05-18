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
    public sealed class User : Entity
    {
        protected User()
        {
            
        }
        internal User(string name, DateTime dateOfBirth, string gender, string address, Guid imageId, Account account)
        {
            Name = name;
            DateOfBirth = dateOfBirth;
            Gender = gender;
            Address = address;
            ImageId = imageId;
            Account = account;
        }

        public string Name { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Gender { get; set; }
        public string Address { get; set; }
        public Guid ImageId { get; set; }
        [ForeignKey("Account")]
        public int AccountId { get; set; }
        public Account Account { get; set; }
        public Seller? Seller { get; set; }
        public IEnumerable<Order> Orders { get; set; }
        public IEnumerable<ShipAddress> ShipAddresses { get; set; }
        public IEnumerable<FeedBack> FeedBacks { get; set; }
        public IEnumerable<Cart> Carts { get; set; }
        internal static Result<User> Create(string name, DateTime dateOfBirth, string gender, string address, Guid imageId, Account account)
        {
            var user = new User(name, dateOfBirth, gender, address, imageId, account);
            return user;
        }
        public Result<User> AddSeller(string shopName, string address, string phoneNumber, string email, string description)
        {
            var result = Seller.Create(shopName, address, phoneNumber, email, description, this);
            if (result.IsFailure)
            {
                return Result.Failure<User>(result.Error);
            }
            Seller = result.Value;
            return this;
        }
    }
}
