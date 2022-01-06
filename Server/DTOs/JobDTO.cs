using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XebecPortal.Server.DTOs
{
    public class JobDTO
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public string Company { get; set; }

        public string Location { get; set; }

        public string Department { get; set; }

        public DateTime DueDate { get; set; }
        //public string JobType { get; set; }
        public DateTime CreationDate { get; set; }

    }
}
