using System.Net;
using System.Net.Http.Json;
using Domain.Features.Employee;
using FluentAssertions;
using WebAPI.FunctionalTests.Abstraction;

namespace WebAPI.FunctionalTests.Employee;

public class CreateEmployeeTests : BaseFunctionalTest
{
    public CreateEmployeeTests(FunctionalTestWebAppFactory factory) : base(factory){}
    
    
    [Fact]
    public async Task Should_ReturnOk_WhenRequestIsValid()
    {
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
}