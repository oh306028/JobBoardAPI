using System.ComponentModel.DataAnnotations;

namespace JobBoardAPI.Models
{
    public class RegisterUserDto
    {   
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [MaxLength(25)]
        public string Name { get; set; }
        [MinLength(6)]
        [Required]
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }


        public int RoleId { get; set; } = 1;
    }
}
