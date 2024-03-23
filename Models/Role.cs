using System.ComponentModel.DataAnnotations;

namespace ShoppingCartSystem.Models
{
    public class Role
    {
        [Key]
        public int RoleId { get; set; }
        public string RoleName { get; set; }
    }
}
