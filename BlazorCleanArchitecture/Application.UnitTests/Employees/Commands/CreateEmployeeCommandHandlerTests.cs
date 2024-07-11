using Application.Commands;
using Application.Handlers.EmployeeHandler;
using Domain.Features.Employee;
using FluentAssertions;
using NSubstitute;

namespace Application.UnitTests.Employees.Commands
{
    public class CreateEmployeeCommandHandlerTests
    {
        private readonly IEmployeeRepository _employeeRpositoryMock;
        private static readonly Employee employee = new()
        {
            Id = 1,
            Address = "assdsadasddasd",
            Name = "Teshgjhjht",
        };
        private readonly CreateEmployeeCommand createEmployeeCommand = 
            new CreateEmployeeCommand(employee);



        public CreateEmployeeCommandHandlerTests()
        {
            _employeeRpositoryMock = Substitute.For<IEmployeeRepository>();
        }


        [Fact]
        public async void Handle_Should_ReturnHailureResult_WhenEmployeeNameExists()
        {
            // Arrange
            var handler = new CreateEmployeeHandler(_employeeRpositoryMock);
            _employeeRpositoryMock.CheckExists(employee.Name, default).Returns(employee);

            // Act
            var result = await handler.Handle(createEmployeeCommand, default);

            // Assert
            result.Flag.Should().BeFalse();
        }




    }
}
