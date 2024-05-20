namespace Ewamall.WebAPI.DTOs
{
    public class CreateVoucherCommand
    {
        public string VoucherCode { get; set; }
        public bool Type { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public float Discount { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public float MinOrder { get; set; }
        public float MaxDiscount { get; set; }
        private int StaffId { get; set; }
    }
}
