using Ewamall.Domain.Primitives;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ewamall.Domain.Entities
{
    public sealed class Account : Entity
    {
        protected Account() 
        {
            
        }
        public Account(int id, string email, string password, string? emailConfirmed, string phoneNumber, DateTime created, DateTime updated, Role role) : base(id)
        {
            Email = email;
            Password = password;
            EmailConfirmed = emailConfirmed;
            PhoneNumber = phoneNumber;
            Created = created;
            Updated = updated;
            Role = role;
        }

        public string Email { get; set; }
        public string Password { get; set; }
        public string? EmailConfirmed { get; set; }
        public string PhoneNumber { get; set; }
        public DateTime Created { get; set; }
        public DateTime Updated { get; set; }
        public bool IsActive { get; set; } = true;
        public Role Role { get; set; }
    }
}
