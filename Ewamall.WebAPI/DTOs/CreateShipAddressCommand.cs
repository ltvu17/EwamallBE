﻿namespace Ewamall.WebAPI.DTOs
{
    public class CreateShipAddressCommand
    {
        public string Name { get;  set; }
        public string Address { get;  set; }
        public string PhoneNumber { get;  set; }
        public bool IsDefault { get;  set; }
    }
}
