namespace Ewamall.WebAPI.DTOs
{
    public class CreateOrderCommand
    {
        public string OrderCode { get;  set; }
        public float TotalCost { get;  set; }
        public float ShipCost { get;  set; }
        public int StatusId { get;  set; }
        public int ShipAddressId { get;  set; }
        public int VoucherId { get;  set; }
        public int PaymentId { get; set; }
        public DateTime? OrderDate { get; set; }
        public IEnumerable<CreateOrderDetailCommand> CreateOrderDetailCommands { get; set; }
    }
     public class CreateOrderDetailCommand
    {
        public int Quantity { get;  set; }
        public int ProductSellDetailId { get;  set; }
    }
}
