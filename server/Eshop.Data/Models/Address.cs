using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Eshop.Data.Models
{
    public class Address
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity), Key]
        public uint Id { get; set; }

        public uint OrderId { get; set; }
        public virtual Order? Order { get; set; }

        [Required]
        [StringLength(100)]
        public string City { get; set; } = "";

        [Required]
        [StringLength(100)]
        public string Street { get; set; } = "";
    }
}
