using Npgsql;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ShoppingCartSystem.Models
{
    public class Product
    {
        [Key]
        public int ProductId { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public string Price { get; set; }
        [Required]
        public int Stock { get; set; }
        public int Review { get; set; }
        public int ReviewCount { get; set; }
        [ForeignKey("Category")]
        [Required]
        public int CategoryId { get; set; }
        public Category Category { get; set; }




    }
}
