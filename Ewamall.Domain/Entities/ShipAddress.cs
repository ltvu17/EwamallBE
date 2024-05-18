﻿using Ewamall.Domain.Primitives;
using Ewamall.Domain.Shared;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ewamall.Domain.Entities
{
    public sealed class ShipAddress : Entity
    {
        protected ShipAddress()
        {
            
        }
        internal ShipAddress(string name, string address, string phoneNumber, bool isDefault, int userId) 
        {
            Name = name;
            Address = address;
            PhoneNumber = phoneNumber;
            IsDefault = isDefault;
            UserId = userId;
        }

        public string Name { get; private set; }
        public string Address { get; private set; }
        public string PhoneNumber { get; private set; }
        public bool IsDefault { get; private set; }
        [ForeignKey("User")]
        public int UserId { get; private set; }
        public User User { get; private set; }
        public static Result<ShipAddress> Create(string name, string address, string phoneNumber, bool isDefault, int userId, IEnumerable<ShipAddress> shipAddresses)
        {
            if (isDefault)
            {
                foreach (ShipAddress shipAddress in shipAddresses)
                {
                    shipAddress.IsDefault = false;
                }
            }
            var newShipAddress = new ShipAddress(name, address, phoneNumber, isDefault, userId);
            return newShipAddress;
        }
        public ShipAddress ChangDefault(bool status)
        {
            IsDefault = status;
            return this;
        }
    }
}
