using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XebecPortal.Shared
{
    public class ApplicationPhaseHelper
    {
        public int Id { get; set; }
        public int ApplicationId { get; set; }
        public Application Application { get; set; }

        public int ApplicationPhaseId { get; set; }
        public ApplicationPhase ApplicationPhase { get; set; }

        public int StatusId { get; set; }
        public Status Status { get; set; }
        public DateTime TimeMoved { get; set; }
        public string Comments { get; set; }
        public float Rating { get; set; }

        public override string ToString()
        {
            return $"{nameof(Id)}={Id.ToString()} \n " +
                $"{nameof(ApplicationId)}={ApplicationId.ToString()} \n" +
                $" {nameof(Application)}={Application} \n " +
                $"{nameof(ApplicationPhaseId)}={ApplicationPhaseId.ToString()} " +
                $"\n {nameof(ApplicationPhase)}={ApplicationPhase} \n" +
                $" {nameof(StatusId)}={StatusId.ToString()} \n " +
                $"{nameof(Status)}={Status} \n" +
                $" {nameof(TimeMoved)}={TimeMoved.ToString()} \n" +
                $" {nameof(Comments)}={Comments}";
        }
    }

}
