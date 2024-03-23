using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ShoppingCartSystem.Models
{
    public class Cart
    {
        [Key]
        public int CartId { get; set; }
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

    }
}
