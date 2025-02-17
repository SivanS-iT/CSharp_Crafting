using Application.Abstractions.Data;
using Application.Employees.CreateEmployee;
using Domain.Features.Employee;
using FluentAssertions;
using NSubstitute;

namespace Application.UnitTests.Employees.Commands
{
    public class CreateEmployeeCommandHandlerTests
    {
        private readonly IEmployeeRepository _employeeRpositoryMock;
        private readonly CreateEmployeeCommandHandler _createEmployeeCommandHandler;
        private readonly CreateEmployeeCommand _createEmployeeCommand;
        private readonly IUnitOfWork _unitOfWorkMock;

        public CreateEmployeeCommandHandlerTests()
        {
            _employeeRpositoryMock = Substitute.For<IEmployeeRepository>();
            _unitOfWorkMock = Substitute.For<IUnitOfWork>();
            _createEmployeeCommandHandler = new CreateEmployeeCommandHandler(_employeeRpositoryMock, _unitOfWorkMock);
            _createEmployeeCommand = new CreateEmployeeCommand(employeeTestRequest);
        }

        private static readonly CreateEmployeeRequest employeeTestRequest = new()
        {
            Address = "Testing address",
            Name = "Ivan",
            Email = "TestingEmail"
        };
        private static readonly Employee employeeTest = new()
        {
            Id = 1,
            Address = "Testing address",
            Name = "Ivan",
            Email = "TestingEmail"
        };
        private static readonly Employee? employeeAsNull = null;



        [Fact]
        public async void Handle_Should_ReturnFailureResult_WhenCreateEmployeeEmailExists()
        {
            // Arrange
            _employeeRpositoryMock.CheckExists(employeeTest.Email, default).Returns(employeeTest);

            // Act
            var result = await _createEmployeeCommandHandler.Handle(_createEmployeeCommand, default);

            // Assert
            result.IsFailure.Should().BeTrue();
            result.Error.Should().Be(EmployeeErrors.EmployeeExists);
        }


        [Fact]
        public async void Handle_Should_ReturnTrueResult_WhenCreateEmployeeEmailDoesNotExist()
        {
            // Arrange
            _employeeRpositoryMock.CheckExists(employeeTest.Email, default).Returns(employeeAsNull);
            
            // Act
            var result = await _createEmployeeCommandHandler.Handle(_createEmployeeCommand, default);

            // Assert
            result.IsSuccess.Should().BeTrue();
        }



        [Fact]
        public async void Handle_Should_CallUnitOfWork()
        {
            // Arrange
            _employeeRpositoryMock.CheckExists(employeeTest.Email, default).Returns(employeeAsNull);

            // Act
            await _createEmployeeCommandHandler.Handle(_createEmployeeCommand, default);

            // Assert
            await _unitOfWorkMock.Received(1).SaveChangesAsync(Arg.Any<CancellationToken>());
        }


        [Fact]
        public async void Handle_Should_CallRepository()
        {
            // Arrange
            _employeeRpositoryMock.CheckExists(employeeTest.Email, default).Returns(employeeAsNull);

            // Act
            await _createEmployeeCommandHandler.Handle(_createEmployeeCommand, default);

            // Assert
            await  _employeeRpositoryMock.Received(1).Add(Arg.Any<Employee>(), default);
        }

    }
}
