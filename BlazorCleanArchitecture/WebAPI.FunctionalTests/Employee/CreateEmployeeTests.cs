using System.Net;
using System.Net.Http.Json;
using Application.Employees;
using Domain.Features.Employee;
using FluentAssertions;
using WebAPI.FunctionalTests.Abstraction;
using WebApi.FunctionalTests.Extensions;

namespace WebAPI.FunctionalTests.Employee;

public class CreateEmployeeTests : BaseFunctionalTest
{
    public CreateEmployeeTests(FunctionalTestWebAppFactory factory) : base(factory){}
    
    
    [Fact]
    public async Task Should_ReturnOk_WhenRequestIsValid()
    {
        // Arrange
        var request = new CreateEmployeeRequest()
        {
            Name = "Ivan", 
            Address = "Address", 
            Email = "thisIsNewEmail345@gmail.com"
        };
        // Act
        var response = await HttpClient.PostAsJsonAsync(EmployeeEndpoint, request);

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.OK);
        var empId = await response.Content.ReadFromJsonAsync<int>();
        empId.Should().Be(1);
    }
    
    [Fact]
    public async Task Should_ReturnBadRequest_WhenNameIsMissing()
    {
        // Arrange
        var request = new CreateEmployeeRequest()
        {
            Name = "", 
            Address = "Address", 
            Email = "thisIsNewEmail345@gmail.com"
        };

        // Act
        var response = await HttpClient.PostAsJsonAsync(EmployeeEndpoint, request);

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
        var request = new CreateEmployeeRequest()
        {
            Name = "Ivan", 
            Address = "Address", 
            Email = ""
        };

        // Act
        var response = await HttpClient.PostAsJsonAsync(EmployeeEndpoint, request);

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        var problemDetails = await response.GetProblemDetails();
        problemDetails.Errors.Select(e => e.Code).Should()
            .Contain([EmployeeErrorCodes.EmailMissing]);
    }
    
}