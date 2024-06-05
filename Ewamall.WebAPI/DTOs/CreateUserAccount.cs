using Ewamall.Business.Enums;

namespace Ewamall.WebAPI.DTOs
{
    public class CreateUserAccount
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public string? EmailConfirmed { get; set; }
        public string PhoneNumber { get; set; }
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

    public class CreateSeller
    {
        public string ShopName { get; set; }
        public string Address { get; set; }
        public int ProvinceId { get; set; }
        public int DistrictId { get; set; }
        public int WardId { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string Description { get; set; }
    }

    public class CreateWallet
    {
        public float Balance { get; set; }
    }

    public class UpdateUserAccount
    {
        public string Password { get; set; }
    }

}
