using System.ComponentModel.DataAnnotations;

namespace JobBoardAPI.Models
{
    public class CreateRequirementDto
    {
        public int Id { get; set; }
        [Required]
        public string Education { get; set; }

        public int Age { get; set; }

        [Required]
        public int Experience { get; set; }
    }
}
