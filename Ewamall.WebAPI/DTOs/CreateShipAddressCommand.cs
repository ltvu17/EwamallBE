namespace Ewamall.WebAPI.DTOs
{
    public class CreateShipAddressCommand
    {
        public string Name { get;  set; }
        public string Address { get;  set; }
        public string PhoneNumber { get;  set; }
        public bool IsDefault { get;  set; }
        public int ProvinceId { get;  set; }
        public int DistrictId { get;  set; }
        public int WardId { get;  set; }
    }
}
