using JobBoardAPI.Forms;
using System.ComponentModel.DataAnnotations;

namespace JobBoardAPI.Models
{
    public class RequirementDto
    {
        public int Id { get; set; }
        public string Education { get; set; }   

        public int Age { get; set; }

        public int Experience { get; set; }

    }
}
