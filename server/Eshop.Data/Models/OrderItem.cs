using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Eshop.Data.Models
{
    public class OrderItem
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity), Key]
        public uint Id { get; set; }

        public uint OrderId { get; set; }
        public virtual Order? Order { get; set; }

        public uint BookId { get; set; }
        public virtual Book? Book { get; set; }

        [Required]
        [Range(1, 1000)]
        public int Quantity { get; set; }

        [Required]
        [Range(0, double.MaxValue)]
        [Precision(18, 2)]
        public decimal TotalPrice { get; set; }
    }
}
