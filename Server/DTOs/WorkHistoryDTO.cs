using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XebecPortal.Server.DTOs
{
  public  class WorkHistoryDTO
    {
        public int Id { get; set; }

        public string CompanyName { get; set; }

        public string JobTitle { get; set; }

        public string Description { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; } 
        
        //foreign key
       public int AppUserId { get; set; }

        public AppUserDTO AppUser { get; set; }


     }
}
