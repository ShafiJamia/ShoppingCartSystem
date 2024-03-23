using System.ComponentModel.DataAnnotations.Schema;

namespace ShoppingCartSystem.Models
{
    [NotMapped]
    public class Response
    {
        public bool IsSuccess { get; set; }
        public string Message { get; set; }
    }
}
