using Application.Commands.EmployeeCommands;
using Domain.DTOs;
using Domain.Features.Employee;
using Npgsql;

namespace Application.IntegrationTests;

public class EmployeeTest : BaseIntegrationTest
{
    private readonly CreateEmployeeCommand _createEmployeeCommand;
    
    private static readonly CreateEmployeeRequest employeeTestRequest = new()
    {
        Address = "Testing address",
        Name = "Ivan",
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
    public async Task Create_ShouldAdd_NewEmployeeToDatabase()
    {
        // Arrange
        var command = new CreateEmployeeCommand(employeeTestRequest);
        
        // Ack
        Task Action() => Sender.Send(command);

        // Assert
        await Assert.ThrowsAsync<PostgresException>(Action);
    }
}