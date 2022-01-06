using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Server.Data;
using Server.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using XebecPortal.Shared;
using XebecPortal.Shared.Security;

namespace XebecPortal.Server.JobPortalTestEnv.Helpers.Repositories
{
    public class CandidateTestRepo : GenericRepository<AppUser>, ICandidateTestRepo
    {
        public CandidateTestRepo(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<List<PersonalInformation>> GetApplications(int JobId)
        {
            IQueryable<PersonalInformation> queryFinal;
            //var job = new SqlParameter("jobId", JobId);
            //IQueryable<PersonalInformation> queryFinal = _context.PersonalInformations.
            //    FromSqlRaw("SELECT * from PersonalInformations Where UserId IN (SELECT UserId FROM Applications where JobId = @jobId)", job);

            queryFinal = from users in _context.AppUser
                    join applications in _context.Applications.Where(a => a.JobId == JobId)
                        on users.Id equals applications.AppUserId
                    join info in _context.PersonalInformations
                     on users.Id equals info.AppUserId
                    select info;

            return await queryFinal.AsNoTracking().ToListAsync();
        }

        public async Task<List<AppUser>> GetApplicantIds(int JobId)
        {
            IQueryable<AppUser> queryFinal;
          

            queryFinal = from users in _context.AppUser
                         join applications in _context.Applications.Where(a => a.JobId == JobId)
                             on users.Id equals applications.AppUserId
                         select users;

            return await queryFinal.AsNoTracking().ToListAsync();
        }

        public async Task<List<PersonalInformation>> SearchCandidates(int JobId, string SearchQuery, string ethnicityFiler, string GenderFilter, string disabilityFilter)
        {

            
            IQueryable<PersonalInformation> queryPI = _context.PersonalInformations;
            IQueryable<WorkHistory> queryWH = _context.WorkHistories;
            IQueryable<Education> queryEd = _context.Educations;

            //GetApplications for sepecific job
            IQueryable<AppUser> query = from users in _context.AppUser
                                     join applications in _context.Applications.Where(a => a.JobId == JobId)
                                         on users.Id equals applications.AppUserId
                                     select users;
            //Search name and surname
            if (!string.IsNullOrEmpty(SearchQuery))
            {
                query = from user in query
                               join personalinfo in _context.PersonalInformations.
                               Where(p => p.FirstName.Contains(SearchQuery) || p.LastName.Contains(SearchQuery))
                                   on user.Id equals personalinfo.AppUserId
                               select user;
            }
            //filter by ethnicity
            if (!string.IsNullOrEmpty(ethnicityFiler))
            {
                    query = from user in query
                                   join person in _context.PersonalInformations.Where(e => e.IdNumber.Contains(ethnicityFiler))
                                       on user.Id equals person.AppUserId
                                   select user;
                    //Searches for job titles, compensation and locations based on what user entered
            }
            //filter by gender
            if (!string.IsNullOrEmpty(GenderFilter))
            {
                query = from user in query
                               join person in _context.PersonalInformations.Where(e => e.FirstName.Equals(GenderFilter))
                                   on user.Id equals person.AppUserId
                               select user;
                //Searches for job titles, compensation and locations based on what user entered
            }
            //filter by disability
          
            queryPI = from users in query
                      join applications in queryPI
                          on users.Id equals applications.AppUserId
                      select applications;
            //Searches for job titles, compensation and locations based on what user entered
            return await queryPI.AsNoTracking().ToListAsync();
        }

    }
}
