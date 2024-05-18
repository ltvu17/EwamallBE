using Ewamall.Business.Enums;

namespace Ewamall.WebAPI.DTOs
{
    public class CreateUserAccount
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public string? EmailConfirmed { get; set; }
        public string PhoneNumber { get; set; }
        public bool IsActive { get; set; } = true;
        public UserInformation UserInformation { get; set; }
    }
    public class UserInformation
    {
        public string Name { get; set; }
        public DateTime DateOfBirth { get; set; }
        public GenderEnum Gender { get; set; }
        public string Address { get; set; }
        public Guid ImageId { get; set; }
    }

}
