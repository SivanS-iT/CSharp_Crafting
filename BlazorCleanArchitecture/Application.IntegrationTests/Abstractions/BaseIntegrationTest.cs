using Application.Employees.CreateEmployee;
using Docker.DotNet.Models;
using Domain.Features.Employee;
using Domain.Shared;
using Infrastructure.Data;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace Application.IntegrationTests.Abstractions;

public abstract class BaseIntegrationTest : IClassFixture<IntegrationTestWebAppFactory>
{
    private readonly IServiceScope _scope;
    protected readonly ISender Sender;
    protected readonly AppDbContext DbContext;

    protected BaseIntegrationTest(IntegrationTestWebAppFactory factory)
    {
        _scope = factory.Services.CreateScope();
        Sender = _scope.ServiceProvider.GetRequiredService<ISender>();
        DbContext = _scope.ServiceProvider.GetRequiredService<AppDbContext>();
    }
    
    
    // ---------------------------------------------
    // ---------------------------------------------
    // ---------------------------------------------
    protected const int NotExistingEmployee = 55;
    protected async Task<Result<int>> CreateEmployeeAsync(
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
        return await Sender.Send(command);
    }

    protected async Task CreateEmployeesAsync()
    {
        var employee1 = new CreateEmployeeRequest()
        {
            Name = "Ivan", 
            Address = "Address", 
            Email = "thisIsNewEmail@gmail.com"
        };
        var command = new CreateEmployeeCommand(employee1);
        await Sender.Send(command);
        var employee2 = new CreateEmployeeRequest()
        {
            Name = "Ivan2", 
            Address = "Address2", 
            Email = "thisIsNewEmail2@gmail.com"
        };
        var command2 = new CreateEmployeeCommand(employee2);
        await Sender.Send(command2);
    }
    
}