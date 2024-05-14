using Ewamall.Domain.Primitives;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ewamall.Domain.Entities
{
    public sealed class Voucher : Entity
    {
        protected Voucher()
        {
            
        }
        public Voucher(int id, string voucherCode, bool type, string name, string description, float discount, DateTime startDate, DateTime endDate, float minOrder, float maxDiscount, Staff staff) : base(id)
        {
            VoucherCode = voucherCode;
            Type = type;
            Name = name;
            Description = description;
            Discount = discount;
            StartDate = startDate;
            EndDate = endDate;
            MinOrder = minOrder;
            MaxDiscount = maxDiscount;
            Staff = staff;
        }

        public string VoucherCode { get; set; }
        public bool Type {  get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public float Discount { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public float MinOrder {  get; set; }
        public float MaxDiscount { get; set; }
        public Staff Staff { get; set; }
        public IEnumerable<Order> Orders { get; set; }
    }
}
