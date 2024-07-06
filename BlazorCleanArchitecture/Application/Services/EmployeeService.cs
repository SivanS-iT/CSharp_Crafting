using Application.DTOs;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly HttpClient _httpClient;
        public EmployeeService(HttpClient httpClient) 
        {
            this._httpClient = httpClient;
        }

        public async Task<ServiceResponse> AddAsync(Employee employee)
        {
            var data = await _httpClient.PostAsJsonAsync("api/employee", employee);
            var response = await data.Content.ReadFromJsonAsync<ServiceResponse>();
            return response;
        }

        public async Task<ServiceResponse> DeleteAsync(int id)
        {
            var data = await _httpClient.DeleteAsync($"api/employee/{id}");
            var response = await data.Content.ReadFromJsonAsync<ServiceResponse>();
            return response;
        } 

        public async Task<List<Employee>> GetAsync() =>
            await _httpClient.GetFromJsonAsync<List<Employee>>("api/employee");

        public async Task<Employee> GetByIdAsync(int id) =>
            await _httpClient.GetFromJsonAsync<Employee>($"api/employee/{id}");

        public async Task<ServiceResponse> UpdateAsync(Employee employee)
        {
            var data = await _httpClient.PutAsJsonAsync("api/employee", employee);
            var response = await data.Content.ReadFromJsonAsync<ServiceResponse>();
            return response;
        }
    }
}
