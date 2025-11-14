namespace Eshop.Api.Models
{
    public class BookDto
    {
        public uint Id { get; set; }
        public string Title { get; set; } = "";
        public string? Author { get; set; }
        public decimal Price { get; set; }
    }
}
