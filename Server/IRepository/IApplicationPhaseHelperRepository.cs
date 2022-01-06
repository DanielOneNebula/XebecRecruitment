using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using XebecPortal.Server.DTOs.ViewModels;
using XebecPortal.Shared;
using XebecPortal.Shared.Security;

namespace Server.IRepository
{
    public interface IApplicationPhaseHelperRepository
    {
        Task<List<ApplicationPhaseHelper>> GetApplicationPhaseInfo(int AppUserId);

        Task<List<ApplicationPhaseHelper>> GetApplicationPhaseInfoDetailed(int AppUserId, int jobId);

        Task<List<myJobsViewModel>> GetApplicationPhaseInfoForUser(int AppUserId, int PhaseId);
    }
}
