using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using XebecPortal.Shared;

namespace XebecPortal.Server.JobPortalTestEnv.Helpers.Repositories
{
    public interface IJobTestRepo
    {
        Task<List<Job>> SearchJobs(string SearchQuery, string SearchLocation, string jobtypeQuery);
    }
}
