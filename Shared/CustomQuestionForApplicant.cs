using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XebecPortal.Shared.Security;

namespace XebecPortal.Shared
{
    public class CustomQuestionForApplicant
    {
        public int Id { get; set; }
        public int CustomQuestionForHRId { get; set; }

        public string ApplicantAnswer { get; set; }
        public int AppUserId { get; set; }

        public CustomQuestionForHR CustomQuestionForHR { get; set; }
        public AppUser User { get; set; }

    }
}
