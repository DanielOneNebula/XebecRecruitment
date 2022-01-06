using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using XebecPortal.Client.Data_Analytics_Tool.Candidate_Analytics.Interfaces;

namespace XebecPortal.Client.Data_Analytics_Tool.Candidate_Analytics.Services
{
    public class DepartmentDataService { 
        /*private readonly HttpClient _httpClient;

        public DepartmentDataService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<IEnumerable<Department>> GetAllDepartment()
        {
            return await JsonSerializer.DeserializeAsync<IEnumerable<Department>>
                (await _httpClient.GetStreamAsync($"api/Department"), new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });
        }

        public async Task<Department> GetDepartmentById(int DepartmentId)
        {
            return await JsonSerializer.DeserializeAsync<Department>
                (await _httpClient.GetStreamAsync($"api/Department/{DepartmentId}"), new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });
        }

        public async Task UpdateDepartment(int id, Department Department)
        {
            var DepartmentJson =
                new StringContent(JsonSerializer.Serialize(Department), Encoding.UTF8, "application/json");

            await _httpClient.PutAsync($"api/Department/{id}", DepartmentJson);
        }

        public async Task DeleteDepartment(int DepartmentId)
        {
            await _httpClient.DeleteAsync($"api/Department/{DepartmentId}");
        }*/
    }
}