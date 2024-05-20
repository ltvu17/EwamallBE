using Ewamall.Domain.Primitives;
using Ewamall.Domain.Shared;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Net.Mime;
using System.Text;
using System.Threading.Tasks;

namespace Ewamall.Domain.Entities
{
    public class Account : Entity
    {
        protected Account()
        {

        }
        internal Account(string email, string password, string? emailConfirmed, string phoneNumber, DateTime created, DateTime updated, int roleId)
        {
            Email = email;
            Password = password;
            EmailConfirmed = emailConfirmed;
            PhoneNumber = phoneNumber;
            Created = created;
            Updated = updated;
            RoleId = roleId;
        }
        public User? User { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string? EmailConfirmed { get; set; }
        public string PhoneNumber { get; set; }
        public DateTime Created { get; set; }
        public DateTime Updated { get; set; }
        public bool IsActive { get; set; } = false;
        [ForeignKey("Role")]
        public int RoleId { get; set; }
        public Role Role { get; set; }
        public static Result<Account> Create(string email, string password, string? emailConfirmed, string phoneNumber, DateTime created, DateTime updated, int roleId)
        {
            var account = new Account(email, password, emailConfirmed, phoneNumber, created, updated, roleId);
            return account;
        }
        public Result<Account> AddUser(string name, DateTime dateOfBirth, string gender, string address, Guid imageId)
        {
            var result = User.Create(name, dateOfBirth, gender, address, imageId, this);
            if (result.IsFailure) {
                return Result.Failure<Account>(result.Error);
            }
            User = result.Value;
            return this;
        }
    }
}