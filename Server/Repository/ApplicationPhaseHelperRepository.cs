using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Server.Data;
using Server.IRepository;
using Server.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using XebecPortal.Server.DTOs.ViewModels;
using XebecPortal.Shared;
using XebecPortal.Shared.Security;

namespace Server.Repository
{
    public class ApplicationPhaseHelperRepository : GenericRepository<ApplicationPhaseHelper>, IApplicationPhaseHelperRepository
    {
        public ApplicationPhaseHelperRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<List<ApplicationPhaseHelper>> GetApplicationPhaseInfo(int AppUserId)
        {
            IQueryable<ApplicationPhaseHelper> queryFinal;
            //var job = new SqlParameter("jobId", JobId);
            //IQueryable<PersonalInformation> queryFinal = _context.PersonalInformations.
            //    FromSqlRaw("SELECT * from PersonalInformations Where UserId IN (SELECT UserId FROM Applications where JobId = @jobId)", job);
            queryFinal = from users in _context.ApplicationPhasesHelpers
                         join applications in _context.Applications.Where(a => a.AppUserId == AppUserId)
                             on users.ApplicationId equals applications.Id
                         select users;
            return await queryFinal.AsNoTracking().ToListAsync();
        }

        public async Task<List<ApplicationPhaseHelper>> GetApplicationPhaseInfoDetailed(int AppUserId, int jobId)
        {
            IQueryable<ApplicationPhaseHelper> queryFinal;
            //var job = new SqlParameter("jobId", JobId);
            //IQueryable<PersonalInformation> queryFinal = _context.PersonalInformations.
            //    FromSqlRaw("SELECT * from PersonalInformations Where UserId IN (SELECT UserId FROM Applications where JobId = @jobId)", job);
            queryFinal = from users in _context.ApplicationPhasesHelpers
                         join applications in _context.Applications.Where(a => a.AppUserId == AppUserId && a.JobId == jobId)
                             on users.ApplicationId equals applications.Id
                         select users;
            queryFinal = queryFinal.Include(a => a.Application).Include(s => s.Status).Include(p => p.ApplicationPhase);

            return await queryFinal.AsNoTracking().ToListAsync();
        }

        public async Task<List<myJobsViewModel>> GetApplicationPhaseInfoForUser(int AppUserId, int PhaseId)
        {
            IQueryable<ApplicationPhaseHelper> queryphase;
            IQueryable<myJobsViewModel> queryFinal = null;
            IQueryable<Job> queryJobs = null;
            if (PhaseId <1 || PhaseId > 4)
            {
                queryphase = from users in _context.AppUser
                             join applications in _context.Applications.Where(a => a.AppUserId == AppUserId)
                                 on users.Id equals applications.AppUserId
                             join phases in _context.ApplicationPhasesHelpers
                                  on applications.Id equals phases.ApplicationId
                             select phases;
            }
            else
            {
                queryphase = from users in _context.AppUser
                             join applications in _context.Applications.Where(a => a.AppUserId == AppUserId)
                                 on users.Id equals applications.AppUserId
                             join phases in _context.ApplicationPhasesHelpers.Where(p => p.ApplicationPhaseId == PhaseId)
                                  on applications.Id equals phases.ApplicationId
                             select phases;
            }
            queryphase = queryphase.Include(s => s.Status).Include(p => p.ApplicationPhase);
            if (queryphase != null)
            {
                queryJobs = from phases in queryphase
                            join jobs in _context.Jobs
                            on phases.Application.JobId equals jobs.Id
                            select jobs;
            }
            if (queryJobs != null)
            {
                queryFinal = from phases in queryphase
                             join jobs in _context.Jobs
                             on phases.Application.JobId equals jobs.Id
                             select new myJobsViewModel() { Application = phases, Job = jobs };
            }


            return await queryFinal.AsNoTracking().ToListAsync();
        }


    }
}
