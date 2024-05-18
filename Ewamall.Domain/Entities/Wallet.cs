using Ewamall.Domain.Primitives;
using Ewamall.Domain.Shared;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
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
        public Wallet(float balance, Seller seller)
        {
            Balance = balance;
            Seller = seller;
        }

        public float Balance { get; set; }
        [ForeignKey("Seller")]
        public int SellerId { get; set; }
        public Seller Seller { get; set; }
        internal static Result<Wallet> Create(float balance, Seller seller)
        {
            var wallet = new Wallet(balance, seller);
            return wallet;
        }
    }
}
