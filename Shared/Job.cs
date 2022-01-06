using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XebecPortal.Shared
{
    public class Job
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public decimal Compensation { get; set; }

        public int MinimumExperience { get; set; }

        public int LocationId { get; set; }

        public int DepartmentId { get; set; }

        public Department Department { get; set; }

        public Location Location { get; set; }

        public DateTime DueDate { get; set; }
        //public string JobType { get; set; }
        public DateTime CreationDate { get; set; }

    }
}
