using System;

namespace XebecPortal.Client.Data_Analytics_Tool.Candidate_Analytics
{
    public class IdealCandidateDto
    {
        public int SalaryExpectation { get; set; }
        public DateTime Availability { get; set; }
        public int Notice { get; set; }
        public int Experience { get; set; }
        public string University { get; set; }
        public string Platform { get; set; }
        public string Citizenship { get; set; }
        public string Permit { get; set; }
        public string Visa { get; set; }

        public int SalaryImportance { get; set; } = 0;
        public int StartDateImportance { get; set; } = 0;

        public int NoticeImportance { get; set; } = 0;
        public int ExperienceImportance { get; set; } = 0;

        public int UniversityImportance { get; set; } = 0;
        public int PlatformImportance { get; set; } = 0;

        public int CitizenshipImportance { get; set; } = 0;
        public int VisaImportance { get; set; } = 0;
        public int PermitImportance { get; set; } = 0;
    }
}