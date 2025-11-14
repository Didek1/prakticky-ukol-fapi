using System.Text.Json.Serialization;

namespace Eshop.Api.Models
{
    public class OrderDto
    {
        public uint Id { get; set; }
        public string FirstName { get; set; } = "";
        public string LastName { get; set; } = "";
        public string Email { get; set; } = "";
        public string? Phone { get; set; }
        public virtual List<OrderItemDto> Items { get; set; } = [];
        public virtual AddressDto? Address { get; set; }

    }
}
