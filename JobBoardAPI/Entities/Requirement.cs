using JobBoardAPI.Forms;
using System.ComponentModel.DataAnnotations;

namespace JobBoardAPI.Entities
{
    public class Requirement
    {
        public int Id { get; set; }
        public Education Education { get; set; }
        [Required]
        public int Age { get; set; }
        [Required]
        public int Experience { get; set; }

        public virtual JobOffert JobOffert { get; set; }
        public int JobOffertId { get; set; }

    }
}
