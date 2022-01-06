using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;

namespace XebecPortal.Client.Data_Analytics_Tool.Candidate_Analytics
{
    public partial class NewIdealCandidatePage : ComponentBase
    {
        public IdealCandidateDto IdealCandidate { get; set; }
        public IdealCandidateDto TempIdealCandidate { get; set; } = new();
        
        private string PopupDisplay { get; set; } = "none";
        
        public int GlobalMax { get; set; } = 100;
        public int GlobalMin { get; set; } = 0;

        public int SalaryImportance { get; set; } = 0;
        public int StartDateImportance { get; set; } = 0;

        public int NoticeImportance { get; set; } = 0;
        public int ExperienceImportance { get; set; } = 0;

        public int UniversityImportance { get; set; } = 0;
        public int PlatformImportance { get; set; } = 0;

        public int CitizenshipImportance { get; set; } = 0;
        public int VisaImportance { get; set; } = 0;
        public int PermitImportance { get; set; } = 0;

        
        private void HandleValidSubmit()
        {
            Console.WriteLine("HandleValidSubmit called");

            // Process the valid form
        }
        private void ShowPopUp()
        {
            PopupDisplay = "block";
        }

        private void ClosePopUp()
        {
            PopupDisplay = "none";
        }
        
    }
}
