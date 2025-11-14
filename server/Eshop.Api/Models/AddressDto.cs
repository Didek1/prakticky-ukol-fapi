namespace Eshop.Api.Models
{
    public class AddressDto
    {
        public uint OrderId { get; set; }
        public string City { get; set; } = "";
        public string Street { get; set; } = "";
    }
}
