using Application.Employees.DeleteEmployee;
using Application.Employees.GetEmployee;
using Application.Employees.GetEmployees;
using Application.Employees.UpdateEmployee;
using Application.IntegrationTests.Abstractions;
using Domain.Features.Employee;
using FluentAssertions;

namespace Application.IntegrationTests;

public class EmployeeTest : BaseIntegrationTest
{
    
    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="factory"></param>
    public EmployeeTest(IntegrationTestWebAppFactory factory) : base(factory)
    {
    }
    
    
    #region Create Employee
   
    [Fact]
    public async Task Create_ShouldAddEmployee_WhenCommandIsValid()
    {
        // Arrange
        var response = await CreateEmployeeAsync();
        
        // Assert
        Assert.True(response.IsSuccess);
    }
    
    [Fact]
    public async Task Create_ShouldAddNotEmployee_WhenEmailAllreadyExits()
    {
        // Arrange
        await CreateEmployeeAsync(
            "Employee",
            "EmployeeAddress",
            "EmployeeEmail");
        var response = await CreateEmployeeAsync(
            "Employee2",
            "EmployeeAddress2",
            "EmployeeEmail");
        
        // Assert
        Assert.True(response.IsFailure);
    }

    #endregion
    
    #region Get Employee
    
    [Fact]
    public async Task GetById_ShouldReturnEmployee_WhenEmplyeeExists()
    {
        // Arrange
        var employeeId = await CreateEmployeeAsync("Ivan3", "NewAddress", "Mail");
        var getEmployeeById = new GetEmployeeByIdQuery(employeeId.Value);
        
        // Ack
        var response = await Sender.Send(getEmployeeById);
        
        // Assert
        Assert.NotNull(response);
    }
    
    [Fact]
    public async Task GetById_ShouldNotReturnEmployee_WhenEmployeeDoesNotExists()
    {
        // Arrange
        var empById = new GetEmployeeByIdQuery(10);

        // Ack
        var response = await Sender.Send(empById);

        // Assert
        response.IsFailure.Should().BeTrue();
    }
    #endregion

    #region Get Employees

    [Fact]
    public async Task GetList_ShouldReturnEmployees_WhenEmployeesExist()
    {
        // Arrange
        await CreateEmployeesAsync();
        var getEmployeeList = new GetEmployeeListQuery();

        // Act
        var response = await Sender.Send(getEmployeeList);

        // Assert
        Assert.NotEmpty(response.Value);
    }
    #endregion
    
    #region Update Employee

    [Fact]
    public async Task Update_ShouldUpdateEmployee_WhenEmplyeeDoesExists()
    {
        // Arrange
        var employeeId = await CreateEmployeeAsync("Ivan4", "NewAddress4", "Mail4");
        var existingEmployee = await DbContext.Employees.FindAsync(employeeId.Value);
        if (existingEmployee != null)
        {
            existingEmployee.Address = "AddressUpdate";
            existingEmployee.Name = "NameUpdate";
            existingEmployee.Email = "mailUpdate@gmail.com";
    
        }
        var updateEmployeeCommand = new UpdateEmployeeCommand(existingEmployee);
        
        // Ack
        var response = await Sender.Send(updateEmployeeCommand);
        
        // Assert
        Assert.True(response.IsSuccess);
    }
    
    [Fact]
    public async Task Update_ShouldNotUpdateEmployee_WhenEmplyeeDoesNotExists()
    {
        // Arrange
        var employee = new Employee()
        {
            Id = NotExistingEmployee,
            Name = "NewName",
            Address = "NewAddress",
            Email = "NewEmail"
        };
        
        var updateEmployeeCommand = new UpdateEmployeeCommand(employee);
        
        // Ack
        var response = await Sender.Send(updateEmployeeCommand);
        
        // Assert
        Assert.True(response.IsFailure);
        response.Error.Should().Be(EmployeeErrors.NotFound(employee.Id));
    }

    #endregion

    #region Delete Employee

    // Delete Emplyee
    [Fact]
    public async Task Delete_ShouldDeleteEmployee_WhenEmplyeeDoesExists()
    {
        // Arrange
        var employeeId = await CreateEmployeeAsync("Ivan5", "NewAddress5", "Mail5");
        var deleteEmployeeCommand = new DeleteEmployeeByIdCommand(employeeId.Value);
        
        // Ack
        var response = await Sender.Send(deleteEmployeeCommand);
        
        // Assert
        Assert.True(response.IsSuccess);
    }

    [Fact]
    public async Task Delete_ShouldNotDeleteEmployee_WhenEmployeeDoesNotExists()
    {
        // Arrange
        var deleteEmployeeCommand = new DeleteEmployeeByIdCommand(NotExistingEmployee);
        
        // Ack
        var response = await Sender.Send(deleteEmployeeCommand);
        
        // Assert
        Assert.True(response.IsFailure);
        response.Error.Should().Be(EmployeeErrors.NotFound(NotExistingEmployee));
    }
    #endregion
 
}