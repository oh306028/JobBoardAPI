using System.ComponentModel.DataAnnotations;

namespace JobBoardAPI.Models
{
    public class CreateOfferDto
    {
        public int Id { get; set; }
        [Required]
        [MaxLength(30)]
        public string Title { get; set; }

        [Required]
        [MaxLength(30)]
        public string Description { get; set; }

        [Required]
        [MaxLength(25)]
        public string Location { get; set; }

        [Required]
        public double Salary { get; set; }

        [Required]
        [MaxLength(25)]
        public string CompanyName { get; set; }

        [Required]
        public string JobTime { get; set; }

        [Required]
        public string JobType { get; set; }

        public string Education { get; set; }
        public int Experience { get; set; }

        public int Age { get; set; }
    }
}
