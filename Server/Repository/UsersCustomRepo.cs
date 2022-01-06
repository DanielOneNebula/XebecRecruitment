using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Server.Data;
using Server.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using XebecPortal.Server.DTOs.ViewModels;
using XebecPortal.Shared;
using XebecPortal.Shared.Security;

namespace Server.Repository
{
    public class UsersCustomRepo : GenericRepository<AppUser>, IUsersCustomRepo
    {
        public UsersCustomRepo(ApplicationDbContext context) : base(context)
        {
        }

        #region Experiemental Queries
        public async Task<List<CandidateViewModel>> GetCandidateDetails(int JobId)
        {
            IQueryable<CandidateViewModel> queryPI = null;
            IQueryable<AppUser> queryUsers = from users in _context.AppUser
                                             join applications in _context.Applications.Where(a => a.JobId == JobId)
                                             on users.Id equals applications.AppUserId
                                             select users;



            IQueryable<WorkHistory> works = from users in queryUsers
                                            join work in _context.WorkHistories
                                            on users.Id equals work.AppUserId
                                            select work;

            //GetApplicants for sepecific job
            queryPI = from users in queryUsers
                      join personal in _context.PersonalInformations
                      on users.Id equals personal.AppUserId
                      select new CandidateViewModel() { WorkHistories = works.ToList(), PersonalInfo = personal };

            return await queryPI.AsNoTracking().ToListAsync();
        }
        #endregion
        #region Tshego's queries
        public async Task<List<PersonalInformation>> GetApplicantsDetailsByJobId(int JobId)
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

        public async Task<List<PersonalInformation>> SearchApplicants(int JobId, string SearchQuery, string ethnicityFiler, string GenderFilter, string disabilityFilter)
        {

            IQueryable<PersonalInformation> queryPI = _context.PersonalInformations;
            IQueryable<WorkHistory> queryWH = _context.WorkHistories;
            IQueryable<Education> queryEd = _context.Educations;

            //GetApplicants for sepecific job
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
                                   join person in _context.AdditionalInformations.Where(e => e.Ethnicity.Contains(ethnicityFiler))
                                       on user.Id equals person.AppUserId
                                   select user;
                    //Searches for job titles, compensation and locations based on what user entered
            }
            //filter by gender
            if (!string.IsNullOrEmpty(GenderFilter))
            {
                query = from user in query
                               join person in _context.AdditionalInformations.Where(e => e.Gender.Equals(GenderFilter))
                                   on user.Id equals person.AppUserId
                               select user;
                //Searches for job titles, compensation and locations based on what user entered
            }
            //filter by disability
            if (!string.IsNullOrEmpty(disabilityFilter))
            {
                query = from user in query
                               join person in _context.AdditionalInformations.Where(e => e.Disability == disabilityFilter)
                                   on user.Id equals person.AppUserId
                               select user;
                //Searches for job titles, compensation and locations based on what user entered
            }
            queryPI = from users in query
                      join applications in queryPI
                          on users.Id equals applications.AppUserId
                      select applications;
            //return personalInfo of people after going through all the queries
            return await queryPI.AsNoTracking().ToListAsync();
        }
#endregion
        #region Iviwe's queries
        public async Task<List<PersonalInformation>> GetPersonalInfoByRole(string role)
        {
            IQueryable<PersonalInformation> query;

            query = from users in _context.AppUser.Where(r => r.Role == role)
                    join info in _context.PersonalInformations
                     on users.Id equals info.AppUserId
                    select info;

            return await query.AsNoTracking().ToListAsync();
        }
        public async Task<List<PersonalInformation>> GetPersonalInfoByAppUsers(List<AppUser> lstUsers)
        {
            IQueryable<PersonalInformation> query;
            IQueryable<AppUser> queryUsers = lstUsers.AsQueryable();
            query = from users in queryUsers
                    join info in _context.PersonalInformations
                     on users.Id equals info.AppUserId
                    select info;

            return await query.AsNoTracking().ToListAsync();
        }

        public async Task<List<PersonalInformation>> GetPersonalByAdditional(string disability, string gender, string ethnicity)
        {
            IQueryable<PersonalInformation> queryPI = _context.PersonalInformations;

            if (!string.IsNullOrEmpty(disability))
            {
                queryPI = from user in queryPI
                        join personalinfo in _context.AdditionalInformations.
                        Where(p => p.Ethnicity.Contains(ethnicity))
                            on user.AppUserId equals personalinfo.AppUserId
                        select user;
                //filters by disability in additionalInfo table
            }

            if (!string.IsNullOrEmpty(ethnicity))
            {
                queryPI = from user in queryPI
                        join addition in _context.AdditionalInformations.Where(e => e.Disability == disability)
                            on user.AppUserId equals addition.AppUserId
                        select user;
                //filters by ethnicity in additionalInfo table
            }
            //filter by gender
            if (!string.IsNullOrEmpty(gender))
            {
                queryPI = from user in queryPI
                        join person in _context.AdditionalInformations.Where(e => e.Gender.Equals(gender))
                            on user.AppUserId equals person.AppUserId
                        select user;
                //filters by gender in additionalInfo table
            }

            return await queryPI.AsNoTracking().ToListAsync();
        }
        #endregion
    }
}
