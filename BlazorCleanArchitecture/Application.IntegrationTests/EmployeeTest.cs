using Application.Commands.EmployeeCommands;
using Domain.DTOs;
using Domain.Features.Employee;
using Microsoft.EntityFrameworkCore;
using Npgsql;

namespace Application.IntegrationTests;

public class EmployeeTest : BaseIntegrationTest
{
    private readonly CreateEmployeeCommand _createEmployeeCommand;

    private static readonly CreateEmployeeRequest employeeTestRequest = new()
    {
        Address = "Testing",
        Name = "sdfsdf",
    };

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="factory"></param>
    public EmployeeTest(IntegrationTestWebAppFactory factory) : base(factory)
    {
        _createEmployeeCommand = new CreateEmployeeCommand(employeeTestRequest);
    }

    
    [Fact]
    public async Task Create_ShouldAddEmployee_WhenCommandIsValid()
    {
        // Arrange
        employeeTestRequest.Name = "NewTestName";

        // Ack
        var response = await Sender.Send(_createEmployeeCommand);

        // Assert
        Assert.False(!response.Flag);
    }
}