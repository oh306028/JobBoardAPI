using JobBoardAPI.Entities;
using JobBoardAPI.Forms;
using System.ComponentModel.DataAnnotations;

namespace JobBoardAPI.Models
{
    public class SeekerDto
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int Age { get; set; }

        public string Education { get; set; }   

        public int Experience { get; set; }

        public string Email { get; set; }

        public List<JobOfferDto> JobOfferts { get; set; }
    }
}
