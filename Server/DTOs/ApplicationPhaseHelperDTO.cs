using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XebecPortal.Server.DTOs
{
    public class ApplicationPhaseHelperDTO
    {
        public int Id { get; set; }
        public int ApplicationId { get; set; }
        public ApplicationDTO Application { get; set; }

        public int ApplicationPhaseId { get; set; }
        public ApplicationPhaseDTO ApplicationPhase { get; set; }
        public int StatusId { get; set; }

        public StatusDTO Status { get; set; }
        public DateTime TimeMoved { get; set; }
        public string Comments { get; set; }
        
    }
}
