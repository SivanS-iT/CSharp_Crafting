using Application.Commands.EmployeeCommands;
using Application.Queries.EmployeeQuery;
using Domain.DTOs;
using Domain.Features.Employee;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Npgsql;

namespace Application.IntegrationTests;

public class EmployeeTest : BaseIntegrationTest
{
    private static readonly CreateEmployeeRequest employeeTestRequest = new()
    {
        Address = "Testing",
        Name = "sdfsdf",
    };
    private static readonly Employee updateEmployee = new()
    {
        Id = 1,
        Address = "Testing",
        Name = "newName",
    };
    private int _getEmpById = 1;
    
    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="factory"></param>
    public EmployeeTest(IntegrationTestWebAppFactory factory) : base(factory)
    {
    }

    // GetList ------------------------------------------------------
    [Fact]
    public async Task GetList_ShouldReturnEmployee_WhenEmployeesExists()
    {
        // Arrange
        var empList = new GetEmployeeListQuery();
        employeeTestRequest.Name = "NewEmployee";
        var createEmpCommand = new CreateEmployeeCommand(employeeTestRequest);
        await Sender.Send(createEmpCommand);

        // Act
        var query = await Sender.Send(empList);

        // Assert
        Assert.NotEmpty(query);
        query.Count.Should().Be(1);
    }
    
    
    //Get EmployeeById --------------------------------------------------
    [Fact]
    public async Task GetById_ShouldReturnEmployee_WhenEmplyeeExists()
    {
        // Arrange
        var empById = new GetEmployeeByIdQuery(_getEmpById);
        
        // Ack
        var query = await Sender.Send(empById);
        
        // Assert
        Assert.NotNull(query);
    }
    
    [Fact]
    public async Task GetById_ShouldNotReturnEmployee_WhenEmplyeeDoesNotExists()
    {
        // Arrange
        var empByIdCommand = new GetEmployeeByIdQuery(10);
        
        // Ack
        var query = await Sender.Send(empByIdCommand);
        
        // Assert
        Assert.Null(query);
    }
    
    
    // Create Employee ------------------------------------
    [Fact]
    public async Task Create_ShouldAddEmployee_WhenCommandIsValid()
    {
        // Arrange
        employeeTestRequest.Name = "NewEmployee";
        var createEmpCommand = new CreateEmployeeCommand(employeeTestRequest);
        
        // Ack
        var response = await Sender.Send(createEmpCommand);
        
        // Assert
        Assert.True(response.Flag);
    }
    
    
    
    
    /*
    // Update Employee
    [Fact]
    public async Task Update_ShouldUpdateEmployee_WhenEmplyeeDoesExists()
    {
        // Arrange
        var updateEmployeeCommand = new UpdateEmployeeCommand(updateEmployee);
        
        // Ack
        var response = await Sender.Send(updateEmployeeCommand);
        
        // Assert
        Assert.True(response.Flag);
    }
    
    // Delete Emplyee
    [Fact]
    public async Task Delete_ShouldDeleteEmployee_WhenEmplyeeDoesExists()
    {
        // Arrange
        var deleteEmployeeCommand = new DeleteEmployeeByIdCommand(1);
        
        // Ack
        var response = await Sender.Send(deleteEmployeeCommand);
        
        // Assert
        Assert.True(response.Flag);
    }*/
}