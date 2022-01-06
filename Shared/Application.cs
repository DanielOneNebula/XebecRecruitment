using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XebecPortal.Shared.Security;

namespace XebecPortal.Shared
{
    public class Application
    {
        public int Id { get; set; }
        public int JobId { get; set; }
        public Job Job { get; set; }
       
        //foreign key
        public int AppUserId { get; set; }
        public AppUser AppUser { get; set; }
        public DateTime TimeApplied { get; set; }

        public DateTime beginApplication { get; set; }

    }
}
