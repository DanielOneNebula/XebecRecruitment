using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using XebecPortal.Shared;

namespace XebecPortal.Server.DTOs.ViewModels
{
    public class CandidateViewModel
    {
        public PersonalInformation PersonalInfo { get; set; }
        public List<WorkHistory> WorkHistories { get; set; }
        public List<Education> Educations { get; set; }
        public AdditionalInformation AdditionalInformation { get; set; }
    }
}
