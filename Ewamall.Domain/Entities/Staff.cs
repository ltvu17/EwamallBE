using Ewamall.Domain.Primitives;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ewamall.Domain.Entities
{
    public sealed class Staff : Entity
    {
        protected Staff()
        {
            
        }
        public Staff(Account account)
        {
            Account = account;
        }

        public Account Account { get; set; }
        public string Name { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public DateTime StartWorkingDate { get; set; }
        public DateTime? EndWorkingDate { get; set;}
        public IEnumerable<Voucher> Vouchers { get; set; }
    }
}
