namespace Ewamall.WebAPI.DTOs
{
    public class CreateShipAddressCommand
    {
        public string Name { get; private set; }
        public string Address { get; private set; }
        public string PhoneNumber { get; private set; }
        public bool IsDefault { get; private set; }
    }
}
