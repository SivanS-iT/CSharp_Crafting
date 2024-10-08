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
    public async Task Create_ShouldTrowPosgressException_WhenEmpTestRequestIsInvalid()
    {
        // Arrange
        var command = new CreateEmployeeCommand(employeeTestRequest);
        
        // Ack
        Task Action() => Sender.Send(_createEmployeeCommand);

        // Assert
        await Assert.ThrowsAsync<PostgresException>(Action);
    }
    
    [Fact]
    public async Task Create_ShouldAddEmployee_WhenCommandIsValid()
    {
        // Arrange
        var command = new CreateEmployeeCommand(employeeTestRequest);
        
        // Ack
        var response = await Sender.Send(_createEmployeeCommand);

        // Assert
        Assert.False(!response.Flag);
    }
}