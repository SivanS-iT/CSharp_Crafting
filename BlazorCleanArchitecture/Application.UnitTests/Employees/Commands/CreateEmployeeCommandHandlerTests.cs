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


        private static readonly Employee employeeTest = new()
        {
            Id = 1,
            Address = "Testing address",
            Name = "Ivan",
        };

        public CreateEmployeeCommandHandlerTests()
        {
            _employeeRpositoryMock = Substitute.For<IEmployeeRepository>();
        }
        private static readonly Employee? employeeAsNull = null;



        /// <summary>
        /// Returns false when employee does exist!
        /// </summary>
        [Fact]
        public async void Handle_Should_ReturnFailureResult_WhenCreateEmployeeNameExists()
        {
            // Arrange
            var handler = new CreateEmployeeHandler(_employeeRpositoryMock);
            _employeeRpositoryMock.CheckExists(employeeTest.Name, default).Returns(employeeTest);
            const string EmployeeExistsMessage = "User already exists";
            CreateEmployeeCommand createEmployeeCommand = new CreateEmployeeCommand(employeeTest);

            // Act
            var result = await handler.Handle(createEmployeeCommand, default);

            // Assert
            result.Flag.Should().BeFalse();
            result.Message.Should().BeEquivalentTo(EmployeeExistsMessage);
        }


        /// <summary>
        /// Returns true when employee does not exists!
        /// </summary>
        [Fact]
        public async void Handle_Should_ReturnTrueResult_WhenCreateEmployeeNameDoesNotExist()
        {
            // Arrange
            const string EmployeeAdded = "User added";
            CreateEmployeeCommand createEmployeeCommands = new CreateEmployeeCommand(employeeTest);

            var handler = new CreateEmployeeHandler(_employeeRpositoryMock);
            _employeeRpositoryMock.CheckExists(employeeTest.Name, default).Returns(employeeAsNull);
            
            // Act
            var result = await handler.Handle(createEmployeeCommands, default);

            // Assert
            result.Flag.Should().BeTrue();
            result.Message.Should().BeEquivalentTo(EmployeeAdded);
        }



        /// <summary>
        /// Test if CheckExists and CreateEmployee are executed properly
        /// </summary>
        [Fact]
        public async void Handle_Should_NotCreateEmployee_WhenEmployeeExists()
        {
            // Arrange
            CreateEmployeeCommand createEmployeeCommands = new CreateEmployeeCommand(employeeTest);

            var handler = new CreateEmployeeHandler(_employeeRpositoryMock);
            _employeeRpositoryMock.CheckExists(employeeTest.Name, default).Returns(employeeTest);

            // Act
            await handler.Handle(createEmployeeCommands, default);

            // Assert
            await _employeeRpositoryMock.Received(0).CreateEmployee(Arg.Is<Employee>(e => e == employeeTest), default);

        }


        /// <summary>
        /// Test if CheckExists and CreateEmployee are executed properly
        /// </summary>
        [Fact]
        public async void Handle_Should_CreateEmployee_WhenEmployeeDoesNotExist()
        {
            // Arrange
            CreateEmployeeCommand createEmployeeCommands = new CreateEmployeeCommand(employeeTest);

            var handler = new CreateEmployeeHandler(_employeeRpositoryMock);
            _employeeRpositoryMock.CheckExists(employeeTest.Name, default).Returns(employeeAsNull);

            // Act
            await handler.Handle(createEmployeeCommands, default);

            // Assert
            await _employeeRpositoryMock.Received(1).CreateEmployee(Arg.Is<Employee>(e => e == employeeTest), default);

        }

    }
}
