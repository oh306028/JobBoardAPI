using JobBoardAPI.Forms;
using System.ComponentModel.DataAnnotations;

namespace JobBoardAPI.Entities
{
    public class Seeker 
    {
        public int Id { get; set; }
        [Required]
        [MaxLength(30)]
        public string Name { get; set; }
        [Required]
        public int Age { get; set; }

        public Education Education { get; set; }
        [Required]
        public int Experience { get; set; }
        [Required]
        [EmailAddress]
        [MaxLength(35)]
        public string Email { get; set; }

        public virtual List<JobOffert> JobOfferts { get; set; } = new List<JobOffert>(); 

    }
}
