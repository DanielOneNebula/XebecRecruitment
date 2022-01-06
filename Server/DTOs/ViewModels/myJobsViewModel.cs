using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using XebecPortal.Shared;

namespace XebecPortal.Server.DTOs.ViewModels
{
    public class myJobsViewModel
    {
        public Job Job { get; set; }
        public ApplicationPhaseHelper Application { get; set; }
    }
}
