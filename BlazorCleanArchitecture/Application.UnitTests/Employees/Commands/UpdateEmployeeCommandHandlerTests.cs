using Application.Commands.EmployeeCommands;
using Application.Handlers.EmployeeHandler;
using Domain.Features.Employee;
using FluentAssertions;
using NSubstitute;
using NSubstitute.ReturnsExtensions;

namespace Application.UnitTests.Employees.Commands
{
    public class UpdateEmployeeCommandHandlerTests
    {
        private readonly IEmployeeRepository _employeeRpositoryMock;
        private readonly UpdateEmployeeHandler _updateEmployeeHandler;
        private readonly UpdateEmployeeCommand _updateEmployeeCommand;

        public UpdateEmployeeCommandHandlerTests()
        {
            _employeeRpositoryMock = Substitute.For<IEmployeeRepository>();
            //_updateEmployeeHandler = new UpdateEmployeeHandler(_employeeRpositoryMock);
            _updateEmployeeCommand = new UpdateEmployeeCommand(employeeTest);
        }

        private static readonly Employee employeeTest = new()
        {
            Id = 1,
            Address = "Testing Update",
            Name = "Ivan",
        };
        private static readonly Employee? employeeAsNull = null;
        private static readonly string employeeExistsMessage = "User already exists";
        private static readonly string employeeUpdated = "User updated";



        /// <summary>
        /// Returns false when employee does exist!
        /// </summary>
        [Fact]
        public async void Handle_Should_ReturnFailureResult_WhenUpdateEmployeeNameExists()
        {
            // Arrange
            _employeeRpositoryMock.CheckExists(employeeTest.Name, default).Returns(employeeTest);

            // Act
            var result = await _updateEmployeeHandler.Handle(_updateEmployeeCommand, default);

            // Assert
            result.Flag.Should().BeFalse();
            result.Message.Should().BeEquivalentTo(employeeExistsMessage);
        }


        /// <summary>
        /// Returns true when employee does not exists!
        /// </summary>
        [Fact]
        public async void Handle_Should_ReturnTrueResult_WhenUpdateEmployeeNameDoesNotExist()
        {
            // Arrange
            _employeeRpositoryMock.CheckExists(employeeTest.Name, default).Returns(employeeAsNull);

            // Act
            var result = await _updateEmployeeHandler.Handle(_updateEmployeeCommand, default);

            // Assert
            result.Should().ReturnsNull();
        }



        /// <summary>
        /// Test if user is not updated
        /// </summary>
        [Fact]
        public async void Handle_Should_NotUpdateEmployee_WhenEmployeeExists()
        {
            // Arrange
            _employeeRpositoryMock.CheckExists(employeeTest.Name, default).Returns(employeeTest);

            // Act
            await _updateEmployeeHandler.Handle(_updateEmployeeCommand, default);

            // Assert
            await _employeeRpositoryMock.Received(0).UpdateEmployee(Arg.Is<Employee>(e => e == employeeTest), default);

        }


        /// <summary>
        /// Test if user is updated
        /// </summary>
        [Fact]
        public async void Handle_Should_UpdateEmployee_WhenEmployeeDoesNotExist()
        {
            // Arrange
            _employeeRpositoryMock.CheckExists(employeeTest.Name, default).Returns(employeeAsNull);

            // Act
            await _updateEmployeeHandler.Handle(_updateEmployeeCommand, default);

            // Assert
            await _employeeRpositoryMock.Received(1).UpdateEmployee(Arg.Is<Employee>(e => e == employeeTest), default);

        }

    }
}
