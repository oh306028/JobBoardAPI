using System.ComponentModel.DataAnnotations;

namespace JobBoardAPI.Models
{
    public class UpdateRequirementDto
    {
        public int Id { get; set; }
        public string Education { get; set; }

        public int Age { get; set; }

        public int Experience { get; set; }
    }
}
