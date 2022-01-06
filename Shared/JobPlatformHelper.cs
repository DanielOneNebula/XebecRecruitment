using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace XebecPortal.Shared
{
    public class JobPlatformHelper
    {
        public int Id { get; set; }        
        public int JobID { get; set; }
        public Job Job { get; set; }
        public int JobPlatformId { get; set; }
        public JobPlatform JobPlatform { get; set; }
        
    }
}