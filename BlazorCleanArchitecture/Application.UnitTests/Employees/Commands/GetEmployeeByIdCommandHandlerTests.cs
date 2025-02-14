using Application.Abstractions.Data;
using Domain.Features.Employee;
using FluentAssertions;
using NSubstitute;
using Application.Employees.GetEmployee;
using Domain.Shared;

namespace Application.UnitTests.Employees.Commands
{
    public class GetEmployeeByIdCommandHandlerTests
    {
        private readonly IEmployeeRepository _employeeRpositoryMock;
        private readonly GetEmployeeByIdHandler _getEmployeeByIdHandler;
        private readonly GetEmployeeByIdQuery _getEmployeeCommand;
        private readonly IUnitOfWork _unitOfWorkMock;


        private static readonly Employee employeeTest = new()
        {
            Id = 1,
            Address = "Testing address",
            Name = "Ivan",
            Email = "email@gmail.com"
        };
        private static readonly Employee? employeeAsNull = null;
        private static readonly int empId = 1;


        public GetEmployeeByIdCommandHandlerTests()
        {
            _employeeRpositoryMock = Substitute.For<IEmployeeRepository>();
            _unitOfWorkMock = Substitute.For<IUnitOfWork>();
            _getEmployeeByIdHandler = new GetEmployeeByIdHandler(_employeeRpositoryMock, _unitOfWorkMock);
            _getEmployeeCommand = new GetEmployeeByIdQuery(empId);
        }

        
        [Fact]
        public async void Handle_Should_ReturnFailureResult_WhenEmployeeIsNotFound()
        {
            // Arrange
            _employeeRpositoryMock.CheckExistsById(employeeTest.Id, default).Returns(employeeAsNull);

            // Act
            var result = await _getEmployeeByIdHandler.Handle(_getEmployeeCommand, default);

            // Assert
            result.IsFailure.Should().BeTrue();
            result.Error.Should().Be(EmployeeErrors.NotFound(employeeTest.Id));
        }


        [Fact]
        public async void Handle_Should_ReturnTrueResult_WhenEmployeeIsFound()
        {
            // Arrange
            _employeeRpositoryMock.CheckExistsById(employeeTest.Id, default).Returns(employeeTest);
            _employeeRpositoryMock.GetEmployeeById(employeeTest.Id, default).Returns(employeeTest);

            // Act
            var result = await _getEmployeeByIdHandler.Handle(_getEmployeeCommand, default);

            // Assert
            result.IsSuccess.Should().BeTrue();
            result.As<Result<Employee>>();
            result.Value.Address.Should().Be(employeeTest.Address);
            result.Value.Name.Should().Be(employeeTest.Name);
            result.Value.Email.Should().Be(employeeTest.Email);
            result.Value.Id.Should().Be(employeeTest.Id);
        }



        [Fact]
        public async void Handle_Should_CallRepository()
        {
            // Arrange
            _employeeRpositoryMock.CheckExistsById(employeeTest.Id, default).Returns(employeeTest);

            // Act
            var result = await _getEmployeeByIdHandler.Handle(_getEmployeeCommand, default);

            // Assert
            await _employeeRpositoryMock.Received(1).GetEmployeeById(Arg.Is<int>(e => e == employeeTest.Id), default);
        }
    }
}
