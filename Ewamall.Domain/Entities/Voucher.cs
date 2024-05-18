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
    public sealed class Voucher : Entity
    {
        protected Voucher()
        {
            
        }
        public Voucher(string voucherCode, bool type, string name, string description, float discount, DateTime startDate, DateTime endDate, float minOrder, float maxDiscount, int staffId)
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
            StaffId = staffId;
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
        [ForeignKey("Staff")]
        private int StaffId { get; set; }
        public Staff Staff { get; set; }
        public IEnumerable<Order> Orders { get; set; }
        public static Result<Voucher> Create(string voucherCode, bool type, string name, string description, float discount, DateTime startDate, DateTime endDate, float minOrder, float maxDiscount, int staffId)
        {
            var voucher = new Voucher(voucherCode, type, name, description, discount, startDate, endDate, minOrder, maxDiscount, staffId);
            return voucher;
        }
    }
}
