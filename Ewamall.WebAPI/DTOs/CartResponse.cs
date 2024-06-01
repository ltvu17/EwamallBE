namespace Ewamall.WebAPI.DTOs
{
    public class CartResponse
    {
        public int Quantity { get; set; }
        public string Name { get; set; }
        public float? Cost { get; set; }
        public string ProductName { get; set; }
        public string SellerName { get; set; }
        public Guid? CoverImageId { get; set; }
        public int SellerId { get; set; }
        public int CartId { get; set; }
        public string NameProductSellDetail { get; set; }
        public int ProductSellDetailId { get; set; }
    }
}
