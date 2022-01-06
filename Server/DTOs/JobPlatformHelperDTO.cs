using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace XebecPortal.Server.DTOs
{
    public class JobPlatformHelperDTO
    {
        public int Id { get; set; }        
        public int JobID { get; set; }
        public JobDTO Job { get; set; }
        public int JobPlatformId { get; set; }
        public JobPlatformDTO JobPlatform { get; set; }
        
    }
}