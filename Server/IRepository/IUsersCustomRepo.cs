using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using XebecPortal.Server.DTOs.ViewModels;
using XebecPortal.Shared;
using XebecPortal.Shared.Security;

namespace Server.IRepository
{
    public interface IUsersCustomRepo
    {
        Task<List<PersonalInformation>> SearchApplicants(int JobId, string SearchQuery, string ethnicityFiler, string GenderFilter, string disabilityFilter);
        Task<List<PersonalInformation>> GetApplicantsDetailsByJobId(int JobId);
        Task<List<AppUser>> GetApplicantIds(int JobId);
        Task<List<PersonalInformation>> GetPersonalByAdditional(string disability, string gender, string ehtnicity);
        Task<List<CandidateViewModel>> GetCandidateDetails(int JobId);
    }
}
