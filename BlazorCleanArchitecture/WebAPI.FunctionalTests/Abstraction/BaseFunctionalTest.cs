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
    protected const int NotExistingEmployee = 55;
    protected const string EmployeeEndpoint = "api/employee";

    protected async Task<Result<int>> CreateEmployeeFuncAsync(
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
        var command = new CreateEmployeeCommand(employeeRequest); 
        var response = await HttpClient.PostAsJsonAsync($"{EmployeeEndpoint}", command);
        return await response.Content.ReadFromJsonAsync<int>();
    }
}