using System.Net;
using System.Net.Http.Json;
using FluentAssertions;
using WebAPI.FunctionalTests.Abstraction;

namespace WebAPI.FunctionalTests.Employee;

public class GetEmployeeByIdTests : BaseFunctionalTest
{
    public GetEmployeeByIdTests(FunctionalTestWebAppFactory factory) : base(factory){}
    
    
    [Fact]
    public async Task Should_ReturnOk_WhenEmployeeIdIsValid()
    {
        // Arrange
        var id = await CreateEmployeeFuncAsync(
            "Ivan1",
            "Address1",
            "ThisIsEmail12@gmail.com");

        // Act
        var response = await HttpClient.GetAsync($"{EmployeeEndpoint}/{id}");

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.OK);
        var result = await response.Content.ReadFromJsonAsync<Domain.Features.Employee.Employee>();
        result.Should().NotBeNull();
    }
    
    [Fact]
    public async Task Should_ReturnBadRequest_WhenRequestIsInvalid()
    {
        // Act
        var response = await HttpClient.GetAsync($"{EmployeeEndpoint}/{NotExistingEmployee}");

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.NotFound);
    }
}