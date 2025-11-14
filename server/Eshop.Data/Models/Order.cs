using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Eshop.Data.Models
{
    public class Order
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity), Key]
        public uint Id { get; set; }

        [Required]
        [StringLength(100)]
        public string FirstName { get; set; } = "";

        [Required]
        [StringLength(100)]
        public string LastName { get; set; } = "";

        [Required]
        [EmailAddress]
        public string Email { get; set; } = "";

        [Phone]
        public string? Phone { get; set; }

        public virtual List<OrderItem> Items { get; set; } = [];

        public virtual Address? Address { get; set; }
    }
}
