using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XebecPortal.Shared.Security;

namespace XebecPortal.Shared
{
    public class LoginHelper
    {
        public int Id { get; set; }
        public DateTime TimeDateLogin { get; set; }
        public DateTime TimeDateLogOut { get; set; } 
        //foreign key
        public int AppUserId { get; set; }

        public AppUser AppUser { get; set; }
    }
}
