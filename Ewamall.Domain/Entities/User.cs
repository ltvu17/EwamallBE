using Ewamall.Domain.Primitives;
using System;
using System.Collections.Generic;
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
        public User(int id, string name, DateTime dateOfBirth, string gender, string address, Guid imageId, Account account) : base(id)
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
        public Account Account { get; set; }
        public IEnumerable<Order> Orders { get; set; }
        public IEnumerable<ShipAddress> ShipAddresses { get; set; }
        public IEnumerable<FeedBack> FeedBacks { get; set; }
        public IEnumerable<Cart> Carts { get; set; }
    }
}
