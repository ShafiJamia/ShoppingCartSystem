using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Microsoft.VisualBasic;

namespace ShoppingCartSystem.Models
{
    public class Order
    {
        [Key]
        public int OrderId { get; set; }
        [ForeignKey("User")]
        [Required]
        public int UserId { get; set; }
        public User User { get; set; }

        [ForeignKey("Product")]
        [Required]
        public int ProductId { get; set; }
        public Product Product { get; set; }
        [Required]
        public int Quantity { get; set; }
        [Required]
        public string OrderDate { get; set; }

    }
}
