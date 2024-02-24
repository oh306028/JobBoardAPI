using JobBoardAPI.Forms;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace JobBoardAPI.Entities
{
    public class JobOffert
    {
        public int Id { get; set; }
        [Required]
        [MaxLength(30)]
        public string Title { get; set; }

        public int? CreatedById { get; set; }
        public virtual User CreatedBy { get; set; }         

        public string Description { get; set; }
        [MaxLength(30)]
        public string Location { get; set; }
        [Required]
        public double Salary { get; set; }
        [Required]
        [MaxLength(25)]
        public string CompanyName { get; set; }
        [Required]
        public JobTime JobTime { get; set; }
        [Required]
        public JobType JobType { get; set; }

        [Required]
        public virtual Requirement Requirement { get; set; }    
        public virtual List<Seeker> Seekers { get; set; } = new List<Seeker>(); 

    }
}
