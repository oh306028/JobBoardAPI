using JobBoardAPI.Entities;
using JobBoardAPI.Forms;
using System.ComponentModel.DataAnnotations;

namespace JobBoardAPI.Models
{
    public class JobOfferDto
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }
 
        public string Location { get; set; }

        public double Salary { get; set; }

        public string CompanyName { get; set; }

        public string JobTime { get; set; }

        public string JobType { get; set; }
        public string Education { get; set; }
        public int Experience { get; set; }

        public int Age { get; set; }    
       

    }
}
