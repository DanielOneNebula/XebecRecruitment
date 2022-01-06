using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XebecPortal.Server.DTOs
{
    public class JobTypeHelperDTO
    {
        public int Id { get; set; }
        public int JobID { get; set; }
        public JobDTO Job { get; set; }
        public int JobTypeID { get; set; }
        public JobTypeDTO JobType { get; set; }
    }
}
