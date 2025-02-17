using System.Net;
using System.Net.Http.Json;
using Application.Employees;
using Domain.Features.Employee;
using FluentAssertions;
using WebAPI.FunctionalTests.Abstraction;
using WebApi.FunctionalTests.Extensions;

namespace WebAPI.FunctionalTests.Employee;

public class DeleteEmployeeTests : BaseFunctionalTest
{
    public DeleteEmployeeTests(FunctionalTestWebAppFactory factory) : base(factory){}
    
    
    [Fact]
    public async Task Should_ReturnNoContent_WhenEmployeeIdDeleted()
    {
        // Arrange
        var id = await CreateEmployeeFuncAsync(
            "Ivan1",
            "Address1",
            "ThisIsEmail12@gmail.com");

        // Act
        var response = await HttpClient.DeleteAsync($"{EmployeeEndpoint}/{id}");

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.NoContent);
    }
    
    [Fact]
    public async Task Should_ReturnBadRequest_WhenRequestIsInvalid()
    {
        // Act
        var response = await HttpClient.DeleteAsync($"{EmployeeEndpoint}/{NotExistingEmployee}");

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.NotFound);
    }
}