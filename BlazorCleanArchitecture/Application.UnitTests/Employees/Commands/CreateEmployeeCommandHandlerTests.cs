using Application.Commands.EmployeeCommands;
using Application.Handlers.EmployeeHandler;
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

        public CreateEmployeeCommandHandlerTests()
        {
            _employeeRpositoryMock = Substitute.For<IEmployeeRepository>();
            _createEmployeeCommandHandler = new CreateEmployeeCommandHandler(_employeeRpositoryMock);
            _createEmployeeCommand = new CreateEmployeeCommand(employeeTestRequest);
        }

        private static readonly CreateEmployeeRequest employeeTestRequest = new()
        {
            Address = "Testing address",
            Name = "Ivan",
        };
        private static readonly Employee employeeTest = new()
        {
            Id = 1,
            Address = "Testing address",
            Name = "Ivan",
        };
        private static readonly Employee? employeeAsNull = null;
        private static readonly string employeeExistsMessage = "User already exists";
        private static readonly string employeeAdded = "User added";



        /// <summary>
        /// Returns false when employee does exist!
        /// </summary>
        [Fact]
        public async void Handle_Should_ReturnFailureResult_WhenCreateEmployeeNameExists()
        {
            // Arrange
            _employeeRpositoryMock.CheckExists(employeeTest.Name, default).Returns(employeeTest);

            // Act
            var result = await _createEmployeeCommandHandler.Handle(_createEmployeeCommand, default);

            // Assert
            result.Flag.Should().BeFalse();
            result.Message.Should().BeEquivalentTo(employeeExistsMessage);
        }


        /// <summary>
        /// Returns true when employee does not exists!
        /// </summary>
        [Fact]
        public async void Handle_Should_ReturnTrueResult_WhenCreateEmployeeNameDoesNotExist()
        {
            // Arrange
            _employeeRpositoryMock.CheckExists(employeeTest.Name, default).Returns(employeeAsNull);
            
            // Act
            var result = await _createEmployeeCommandHandler.Handle(_createEmployeeCommand, default);

            // Assert
            result.Flag.Should().BeTrue();
            result.Message.Should().BeEquivalentTo(employeeAdded);
        }



        /// <summary>
        /// Employee should not be created
        /// </summary>
        [Fact]
        public async void Handle_Should_NotCreateEmployee_WhenEmployeeExists()
        {
            // Arrange
            _employeeRpositoryMock.CheckExists(employeeTest.Name, default).Returns(employeeTest);

            // Act
            await _createEmployeeCommandHandler.Handle(_createEmployeeCommand, default);

            // Assert
            await _employeeRpositoryMock.Received(0).CreateEmployee(Arg.Is<CreateEmployeeRequest>(e => e == employeeTestRequest), default);
        }


        /// <summary>
        /// Employee should be created
        /// </summary>
        [Fact]
        public async void Handle_Should_CreateEmployee_WhenEmployeeDoesNotExist()
        {
            // Arrange
            _employeeRpositoryMock.CheckExists(employeeTest.Name, default).Returns(employeeAsNull);

            // Act
            await _createEmployeeCommandHandler.Handle(_createEmployeeCommand, default);

            // Assert
            await _employeeRpositoryMock.Received(1).CreateEmployee(Arg.Is<CreateEmployeeRequest>(e => e == employeeTestRequest), default);
        }

    }
}
