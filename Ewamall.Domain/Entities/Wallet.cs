using Ewamall.Domain.Primitives;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ewamall.Domain.Entities
{
    public sealed class Wallet : Entity
    {
        protected Wallet()
        {
            
        }
        public Wallet(int id,float balance, Seller seller) : base(id)
        {
            Balance = balance;
            Seller = seller;
        }

        public float Balance { get; set; }
        public Seller Seller { get; set; }
    }
}
