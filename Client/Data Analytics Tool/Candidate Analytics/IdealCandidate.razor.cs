using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;

namespace XebecPortal.Client.Data_Analytics_Tool.Candidate_Analytics
{
    public partial class IdealCandidate : ComponentBase
    {
        [Parameter]
        public int DepartmentId { get; set; }
        [Parameter]
        public int JobId { get; set; }

        public int GlobalMax { get; set; } = 100;
        public int GlobalMin {get; set; } = 0;
        
        public int SalaryImportance {get; set;} = 0;
        public int StartDateImportance { get; set; } = 0;
        
        public int NoticeImportance {get; set;} = 0;
        public int ExperienceImportance { get; set; } = 0;
        
        public int UniversityImportance { get; set; } = 0;
        public int PlatformImportance { get; set; } = 0;
        
        public int CitizenshipImportance { get; set; } = 0;
        public int VisaImportance { get; set; } = 0;
        public int PermitImportance { get; set; } = 0;

        private void UpdateMax(int importance)
        {
            GlobalMax -= importance;
        }
    }
    
    
}
