using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XebecPortal.Server.DTOs
{
    public class ApplicationDTO
    {
        public int Id { get; set; }
        public int JobId { get; set; }
        public JobDTO Job { get; set; }

        public DateTime beginApplication { get; set; }
       
        //foreign key
        public int AppUserId { get; set; }
        public AppUserDTO AppUser { get; set; }
        public DateTime TimeApplied { get; set; }
        
    }
}
