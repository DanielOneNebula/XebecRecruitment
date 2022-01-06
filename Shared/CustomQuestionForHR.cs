using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XebecPortal.Shared
{
    public class CustomQuestionForHR
    {
        public int Id { get; set; }
        public string  Question { get; set; }
        public string Answer { get; set; }
        public int JobId { get; set; }
        public Job Job { get; set; }
    }
}
