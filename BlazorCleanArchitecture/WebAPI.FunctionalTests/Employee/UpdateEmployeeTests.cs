using System.Net;
using System.Net.Http.Json;
using Application.Employees;
using Application.Employees.UpdateEmployee;
using Domain.Features.Employee;
using FluentAssertions;
using WebAPI.FunctionalTests.Abstraction;
using WebApi.FunctionalTests.Extensions;

namespace WebAPI.FunctionalTests.Employee;

public class UpdateEmployeeTests : BaseFunctionalTest
{
    public UpdateEmployeeTests(FunctionalTestWebAppFactory factory) : base(factory){}
    
    
    [Fact]
    public async Task Should_ReturnOk_WhenRequestIsValid()
    {
        // Arrange
        var empId = await CreateEmployeeFuncAsync( "Ivahasd", "Address234", "23423@gmail.com");
        var request = new Domain.Features.Employee.Employee
        {
            Id = empId,
            Name = "IvanUpdate",
            Address = "TestingAddressUpdate",
            Email = "thisIsEmailUpdate@gmail.com"
        };

        // Act
        var response = await HttpClient.PutAsJsonAsync(EmployeeEndpoint, request);

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.NoContent);
    }
    
    [Fact]
    public async Task Should_ReturnBadRequest_WhenNameIsMissing()
    {
        // Arrange
        var request = new Domain.Features.Employee.Employee
        {
            Id = NotExistingEmployee,
            Name = "IvanUpdate",
            Address = "TestingAddressUpdate",
            Email = ""
        };


        // Act
        var response = await HttpClient.PutAsJsonAsync(EmployeeEndpoint, request);

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        var problemDetails = await response.GetProblemDetails();
        problemDetails.Errors.Select(e => e.Code).Should()
            .Contain([EmployeeErrorCodes.NameMissing]);
    }

    [Fact]
    public async Task Should_ReturnBadRequest_WhenEmailIsMissing()
    {
        // Arrange
        var request = new Domain.Features.Employee.Employee
        {
            Id = NotExistingEmployee,
            Name = "",
            Address = "TestingAddressUpdate",
            Email = "thisIsEmailUpdate@gmail.com"
        };

        // Act
        var response = await HttpClient.PutAsJsonAsync(EmployeeEndpoint, request);

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        var problemDetails = await response.GetProblemDetails();
        problemDetails.Errors.Select(e => e.Code).Should()
            .Contain([EmployeeErrorCodes.EmailMissing]);
    }
}