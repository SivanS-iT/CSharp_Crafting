using Application.Employees.GetEmployees;
using Domain.Features.Employee;
using Domain.Shared;
using FluentAssertions;
using NSubstitute;

namespace Application.UnitTests.Employees.Commands
{
    public class GetEmployeesCommandHandlerTests
    {
        private readonly IEmployeeRepository _employeeRpositoryMock;
        private readonly GetEmployeeListHandler _getEmployeeListHandler;
        private readonly GetEmployeeListQuery _getEmployeeCommand;


        private static readonly List<Employee> employeeTestList =
        [
            new Employee { Id = 1, Name = "Ivan", Address ="Random address", Email = "email@gmail.com"},
            new Employee { Id = 2, Name = "Hrvoje", Address ="Hrvoje address", Email = "email1@gmail.com"},
            new Employee { Id = 3, Name = "Colin", Address ="Colin address", Email = "email2@gmail.com"},
        ];

        public GetEmployeesCommandHandlerTests()
        {
            _employeeRpositoryMock = Substitute.For<IEmployeeRepository>();
            _getEmployeeListHandler = new GetEmployeeListHandler(_employeeRpositoryMock);
            _getEmployeeCommand = new GetEmployeeListQuery();
        }



        /// <summary>
        /// Returns List of employee
        /// </summary>
        [Fact]
        public async void Handle_Should_ReturnEmployeeList()
        {
            // Arrange
            _employeeRpositoryMock.GetAll(default).Returns(employeeTestList);

            // Act
            var result = await _getEmployeeListHandler.Handle(_getEmployeeCommand, default);

            // Assert
            result.IsSuccess.Should().BeTrue();
            result.As<Result<List<Employee>>>();
            result.Value.Count.Should().Be(3);
        }

        /// <summary>
        /// Should call repository
        /// </summary>
        [Fact]
        public async void Handle_Should_CallRepository()
        {
            // Act
            await _getEmployeeListHandler.Handle(_getEmployeeCommand, default);

            // Assert
            await _employeeRpositoryMock.Received(1).GetAll(default);
        }

    }
}
