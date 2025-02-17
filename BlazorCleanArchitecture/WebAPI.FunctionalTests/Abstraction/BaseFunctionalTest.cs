using System.Net.Http.Json;
using Application.Employees.CreateEmployee;
using Domain.Features.Employee;
using Domain.Shared;

namespace WebAPI.FunctionalTests.Abstraction;

public abstract class BaseFunctionalTest : IClassFixture<FunctionalTestWebAppFactory>
{

    public BaseFunctionalTest(FunctionalTestWebAppFactory factory)
    {
        HttpClient = factory.CreateClient();
    }
    
    protected HttpClient HttpClient { get; init;}
    
    // ---------------------------------------------
    // ---------------------------------------------
    // ---------------------------------------------
    protected const int NotExistingEmployee = 555;
    protected const string EmployeeEndpoint = "api/employee";
    protected async Task<int> CreateEmployeeFuncAsync(
        string name = "Ivan0",
        string address = "Testing address0",
        string email = "thisIsEmail0@gmail.com")
    {
        var employeeRequest = new CreateEmployeeRequest()
        {
            Name = name,
            Address = address,
            Email = email
        };
        var response = await HttpClient.PostAsJsonAsync(EmployeeEndpoint, employeeRequest);
        return await response.Content.ReadFromJsonAsync<int>();
    }
}