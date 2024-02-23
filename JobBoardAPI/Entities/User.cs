using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace JobBoardAPI.Entities
{
    public class User
    {
        public int Id { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [MaxLength(25)]
        public string Name { get; set; }
        public string PasswordHash { get; set; }


        public int RoleId { get; set; } 
        public virtual Role Role { get; set; }
    }
}
