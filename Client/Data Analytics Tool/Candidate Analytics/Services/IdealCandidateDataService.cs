using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using XebecPortal.Client.Data_Analytics_Tool.Candidate_Analytics.Interfaces;

namespace XebecPortal.Client.Data_Analytics_Tool.Candidate_Analytics.Services
{
    public class IdealCandidateDataService : IIdealCandidateDataService
    {
        private readonly HttpClient _httpClient;

        public IdealCandidateDataService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<IEnumerable<IdealCandidateDto>> GetAllIdealCandidates()
        {
            return await JsonSerializer.DeserializeAsync<IEnumerable<IdealCandidateDto>>
                (await _httpClient.GetStreamAsync($"api/IdealCandidate"), new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });
        }

        public async Task<IdealCandidateDto> GetIdealCandidateById(int IdealCandidateId)
        {
            return await JsonSerializer.DeserializeAsync<IdealCandidateDto>
                (await _httpClient.GetStreamAsync($"api/IdealCandidate/{IdealCandidateId}"), new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });
        }

        public async Task UpdateIdealCandidate(int id, IdealCandidateDto IdealCandidate)
        {
            var IdealCandidateJson =
                new StringContent(JsonSerializer.Serialize(IdealCandidate), Encoding.UTF8, "application/json");

            await _httpClient.PutAsync($"api/IdealCandidate/{id}", IdealCandidateJson);
        }

        public async Task DeleteIdealCandidate(int IdealCandidateId)
        {
            await _httpClient.DeleteAsync($"api/IdealCandidate/{IdealCandidateId}");
        }
    }
}