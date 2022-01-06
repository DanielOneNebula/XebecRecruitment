using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XebecPortal.Shared
{
    public class JobTypeHelper
    {
        public int Id { get; set; }
        public int JobID { get; set; }
        public Job Job { get; set; }
        public int JobTypeID { get; set; }
        public JobType JobType { get; set; }
    }
}
