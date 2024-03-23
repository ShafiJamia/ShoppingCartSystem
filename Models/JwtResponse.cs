using System.ComponentModel.DataAnnotations.Schema;

namespace ShoppingCartSystem.Models
{
    [NotMapped]
    public class JwtResponse
    {
        public string? Jwt { get; set; }
        public string Message { get; set; }
    }
}
