using System.Net;
using System.Net.Http.Json;
using FluentAssertions;
using WebAPI.FunctionalTests.Abstraction;

namespace WebAPI.FunctionalTests.Employee;

public class GetEmployeesTests : BaseFunctionalTest
{
    public GetEmployeesTests(FunctionalTestWebAppFactory factory) : base(factory){}
    
    
    [Fact]
    public async Task Should_ReturnOk_WhenEmployeesExist()
    {
        // Arrange
        await CreateEmployeeFuncAsync(
            "Ivan1",
            "Address1",
            "ThisIsEmail12@gmail.com");
        await CreateEmployeeFuncAsync(
            "Ivan12",
            "Address12",
            "ThisIsEmail122@gmail.com");

        // Act
        var response = await HttpClient.GetAsync(EmployeeEndpoint);

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.OK);
        var result = await response.Content.ReadFromJsonAsync<List<Domain.Features.Employee.Employee>>();
        result.Should().NotBeNull();
    }
}