using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace XebecPortal.Client.GamifiedEnvBeta.Utils
{
    public class State
    {
        public int Id { get; set; }

        public string UserEmail { get; set; }

        public string Role { get; set; }

        public int PageNumber { get; set; } = 1;

        public bool IsSaved { get; set; } = false;

        public int EducationEditId { get; set; } = -1;

        public int WorkEditId { get; set; } = -1;

        public int DocumentEditId { get; set; } = -1;

        public bool IsUserProfileComplete { get; set; }

        public int JobPostEditId { get; set; } = -1;

        public bool QuestionnaireFilled { get; set; } = false;

    }

}