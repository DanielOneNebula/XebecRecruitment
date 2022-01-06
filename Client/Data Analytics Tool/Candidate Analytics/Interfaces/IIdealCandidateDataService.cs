using System.Collections.Generic;
using System.Threading.Tasks;
using XebecPortal.Shared;

namespace XebecPortal.Client.Data_Analytics_Tool.Candidate_Analytics.Interfaces
{
    public interface IIdealCandidateDataService
    {
        // GET: api/<IdealCandidateController>
        Task<IEnumerable<IdealCandidateDto>> GetAllIdealCandidates();

        // GET api/<IdealCandidateController>/{id}
        Task<IdealCandidateDto> GetIdealCandidateById(int IdealCandidateId);

        // POST api/<IdealCandidateController>
        //Task<IdealCandidate> CreateIdealCandidate(IdealCandidate IdealCandidate);

        // PUT api/<IdealCandidateController>/{id}
        Task UpdateIdealCandidate(int id, IdealCandidateDto idealCandidate);

        // DELETE api/<IdealCandidateController>/{id}
        Task DeleteIdealCandidate(int id);
    }
}