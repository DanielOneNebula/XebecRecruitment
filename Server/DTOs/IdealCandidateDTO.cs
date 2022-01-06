using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XebecPortal.Server.DTOs
{
    public class IdealCandidateDTO
    {
        public int Id { get; set; }

        public double ExpectedSalary { get; set; }

        public DateTime StartDate { get; set; }

        public int NoticePeriodId { get; set; }

        public NoticePeriodDTO NoticePeriod { get; set; }

        public int LocationId { get; set; }

        public LocationDTO Location { get; set; }

        public int Experience { get; set; }

        public int PermissionId { get; set; }

        public PermissionDTO Permission { get; set; }

        public int CitizenshipId { get; set; }

        public CitizenshipDTO Citizenship { get; set; }

        public int VisaId { get; set; }

        public VisaDTO Visa { get; set; }

        public int WorkPermitId { get; set; }

        public WorkPermitDTO WorkPermit { get; set; }

        public string University { get; set; }

        public int JobPlatformId { get; set; }

        public JobPlatformDTO JobPlatform { get; set; }

        public int JobId { get; set; }

        public JobDTO Job { get; set; }
    }
}
