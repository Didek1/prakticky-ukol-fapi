namespace Eshop.Api.Models
{
    public class OrderItemDto
    {
        public uint OrderId { get; set; }
        public uint BookId { get; set; }
        public uint Quantity { get; set; }
        public decimal TotalPrice { get; set; } = 0;
        public virtual BookDto? Book { get; set; }
    }
}
