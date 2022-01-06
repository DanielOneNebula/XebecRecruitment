using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XebecPortal.Shared.Security;

namespace XebecPortal.Shared
{
   public class ProfilePortfolioLink
    {
        public int Id  { get; set; }

        public string GitHubLink { get; set; }

        public string LinkedInLink{ get; set; }

        public string TwitterLink { get; set; }

        public string PersonalWebsiteUrl { get; set; }

        //foreign key 
        public int AppUserId { get; set; }

        public AppUser AppUser { get; set; }


    }
}
