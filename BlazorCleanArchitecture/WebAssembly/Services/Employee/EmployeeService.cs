using System.Net.Http.Json;

namespace WebAssembly.Services.Employee
{
    public class EmployeeService : IEmployeeService
    {
        private readonly HttpClient _httpClient;
        public EmployeeService(HttpClient httpClient) 
        {
            this._httpClient = httpClient;
        }

        public async Task<Data.Employee?> GetById(int id, CancellationToken cancellationToken)
            => await _httpClient.GetFromJsonAsync<Data.Employee?>($"api/employee/{id}", cancellationToken);

        public async Task<List<Data.Employee>?> GetAll(CancellationToken cancellationToken)
            => await _httpClient.GetFromJsonAsync<List<Data.Employee>>("api/employee", cancellationToken);

        public async Task<int> Add(Data.Employee entity, CancellationToken cancellationToken)
        {
            var data = await _httpClient.PostAsJsonAsync("api/employee", entity, cancellationToken);
            var response = await data.Content.ReadFromJsonAsync<int>(cancellationToken);
            return response;
        }

        public async void Update(Data.Employee entity)
            => await _httpClient.PutAsJsonAsync("api/employee", entity);

        public async void Delete(int id) 
            => await _httpClient.DeleteAsync($"api/employee/{id}");
    }
}
