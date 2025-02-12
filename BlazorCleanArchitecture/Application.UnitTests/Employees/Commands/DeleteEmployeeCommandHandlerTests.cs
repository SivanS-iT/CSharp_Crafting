using Application.Commands.EmployeeCommands;
using Application.Handlers.EmployeeHandler;
using Domain.DTOs;
using Domain.Features.Employee;
using FluentAssertions;
using NSubstitute;
using NSubstitute.ReturnsExtensions;

namespace Application.UnitTests.Employees.Commands;

public class DeleteEmployeeCommandHandlerTests
    {
        private readonly IEmployeeRepository _employeeRepositoryMock;
        private readonly DeleteEmployeeByIdHandler _deleteEmployeeHandler;
        private readonly DeleteEmployeeByIdCommand _deleteEmployeeCommand;
        private readonly ServiceResponse _serviceResponse;

        public DeleteEmployeeCommandHandlerTests()
        {
            _employeeRepositoryMock = Substitute.For<IEmployeeRepository>();
            //_deleteEmployeeHandler = new DeleteEmployeeByIdHandler(_employeeRepositoryMock);
            _deleteEmployeeCommand = new DeleteEmployeeByIdCommand(employeeId);
            _serviceResponse = new ServiceResponse(flagTrue, messageDeleted);
        }

        private static readonly Employee employeeTest = new()
        {
            Id = 1,
            Address = "Testing Delete",
            Name = "Ivan",
        };

        private static readonly Employee? employeeAsNull = null;
        private static readonly string employeeDeleted = "User deleted";
        private static readonly string employeeNotFound = "User not found";
        private static readonly int employeeId = 1;
        private static bool flagTrue = true;
        private static string messageDeleted = employeeDeleted;



        /// <summary>
        /// Returns true when employee id exist!
        /// </summary>
        [Fact]
        public async void Handle_Should_ReturnTrueResult_WhenEmployeeIdExists()
        {
            // Arrange
            _employeeRepositoryMock.CheckExistsById(employeeId, default).Returns(employeeTest);
            _employeeRepositoryMock.DeleteEmployee(employeeTest, default).Returns(_serviceResponse);

            // Act
            var result = await _deleteEmployeeHandler.Handle(_deleteEmployeeCommand, default);

            // Assert
            result.Flag.Should().BeTrue();
            result.Message.Should().BeEquivalentTo(employeeDeleted);
        }


        /// <summary>
        /// Returns false when employee id does not exist!
        /// </summary>
        [Fact]
        public async void Handle_Should_ReturnTrueResult_WhenIdDoesNotExist()
        {
            // Arrange
            _employeeRepositoryMock.CheckExistsById(employeeId, default).Returns(employeeAsNull);

            // Act
            var result = await _deleteEmployeeHandler.Handle(_deleteEmployeeCommand, default);

            // Assert
            result.Flag.Should().BeFalse();
            result.Message.Should().BeEquivalentTo(employeeNotFound);
        }



        /// <summary>
        /// Test if user is not Deleted
        /// </summary>
        [Fact]
        public async void Handle_Should_NotDeleteEmployee_WhenEmployeeDoesNotExist()
        {
            // Arrange
            _employeeRepositoryMock.CheckExistsById(employeeId, default).Returns(employeeAsNull);

            // Act
            await _deleteEmployeeHandler.Handle(_deleteEmployeeCommand, default);

            // Assert
            await _employeeRepositoryMock.Received(0).DeleteEmployee(Arg.Is<Employee>(e => e == employeeAsNull), default);

        }


        /// <summary>
        /// Test if user is Deleted
        /// </summary>
        [Fact]
        public async void Handle_Should_DeleteEmployee_WhenEmployeeExists()
        {
            // Arrange
            _employeeRepositoryMock.CheckExistsById(employeeId, default).Returns(employeeTest);

            // Act
            await _deleteEmployeeHandler.Handle(_deleteEmployeeCommand, default);

            // Assert
            await _employeeRepositoryMock.Received(1).DeleteEmployee(Arg.Is<Employee>(e => e == employeeTest), default);

        }

    }