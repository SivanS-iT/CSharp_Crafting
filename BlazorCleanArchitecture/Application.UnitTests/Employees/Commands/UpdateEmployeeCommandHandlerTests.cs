using Application.Commands;
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


        private static readonly Employee employeeTest = new()
        {
            Id = 1,
            Address = "Testing Update",
            Name = "Ivan",
        };

        public UpdateEmployeeCommandHandlerTests()
        {
            _employeeRpositoryMock = Substitute.For<IEmployeeRepository>();
        }
        private static readonly Employee? employeeAsNull = null;



        /// <summary>
        /// Returns false when employee does exist!
        /// </summary>
        [Fact]
        public async void Handle_Should_ReturnFailureResult_WhenUpdateEmployeeNameExists()
        {
            // Arrange
            const string EmployeeExistsMessage = "User already exists";

            var handler = new UpdateEmployeeHandler(_employeeRpositoryMock);
            _employeeRpositoryMock.CheckExists(employeeTest.Name, default).Returns(employeeTest);
            UpdateEmployeeCommand updateEmployeeCommand = new UpdateEmployeeCommand(employeeTest);

            // Act
            var result = await handler.Handle(updateEmployeeCommand, default);

            // Assert
            result.Flag.Should().BeFalse();
            result.Message.Should().BeEquivalentTo(EmployeeExistsMessage);
        }


        /// <summary>
        /// Returns true when employee does not exists!
        /// </summary>
        [Fact]
        public async void Handle_Should_ReturnTrueResult_WhenUpdateEmployeeNameDoesNotExist()
        {
            // Arrange
            const string EmployeeUpdated = "User updated";
            var updateEmployeeCommands = new UpdateEmployeeCommand(employeeTest);

            var handler = new UpdateEmployeeHandler(_employeeRpositoryMock);
            _employeeRpositoryMock.CheckExists(employeeTest.Name, default).Returns(employeeAsNull);

            // Act
            var result = await handler.Handle(updateEmployeeCommands, default);

            // Assert
            result.Should().ReturnsNull();
        }



        /// <summary>
        /// Test if CheckExists and CreateEmployee are executed properly
        /// </summary>
        [Fact]
        public async void Handle_Should_NotUpdateEmployee_WhenEmployeeExists()
        {
            // Arrange
            UpdateEmployeeCommand updateEmployeeCommand = new UpdateEmployeeCommand(employeeTest);

            var handler = new UpdateEmployeeHandler(_employeeRpositoryMock);
            _employeeRpositoryMock.CheckExists(employeeTest.Name, default).Returns(employeeTest);

            // Act
            await handler.Handle(updateEmployeeCommand, default);

            // Assert
            await _employeeRpositoryMock.Received(0).UpdateEmployee(Arg.Is<Employee>(e => e == employeeTest), default);

        }


        /// <summary>
        /// Test if CheckExists and Update employee are executed properly
        /// </summary>
        [Fact]
        public async void Handle_Should_UpdateEmployee_WhenEmployeeDoesNotExist()
        {
            // Arrange
            UpdateEmployeeCommand updateEmployeeCommands = new UpdateEmployeeCommand(employeeTest);

            var handler = new UpdateEmployeeHandler(_employeeRpositoryMock);
            _employeeRpositoryMock.CheckExists(employeeTest.Name, default).Returns(employeeAsNull);

            // Act
            await handler.Handle(updateEmployeeCommands, default);

            // Assert
            await _employeeRpositoryMock.Received(1).UpdateEmployee(Arg.Is<Employee>(e => e == employeeTest), default);

        }

    }
}
