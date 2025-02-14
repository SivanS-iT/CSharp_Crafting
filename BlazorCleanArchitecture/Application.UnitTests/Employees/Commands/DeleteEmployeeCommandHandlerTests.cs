using Application.Abstractions.Data;
using Application.Employees.DeleteEmployee;
using Domain.Features.Employee;
using FluentAssertions;
using NSubstitute;

namespace Application.UnitTests.Employees.Commands;

public class DeleteEmployeeCommandHandlerTests
    {
        private readonly IEmployeeRepository _employeeRepositoryMock;
        private readonly DeleteEmployeeByIdHandler _deleteEmployeeHandler;
        private readonly DeleteEmployeeByIdCommand _deleteEmployeeCommand;
        private readonly IUnitOfWork _unitOfWorkMock;


        public DeleteEmployeeCommandHandlerTests()
        {
            _employeeRepositoryMock = Substitute.For<IEmployeeRepository>();
            _unitOfWorkMock = Substitute.For<IUnitOfWork>();
            _deleteEmployeeHandler = new DeleteEmployeeByIdHandler(_employeeRepositoryMock, _unitOfWorkMock);
            _deleteEmployeeCommand = new DeleteEmployeeByIdCommand(employeeId);
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



        [Fact]
        public async void Handle_Should_ReturnFailureResult_WhenEmployeeIsNotFound()
        {
            // Arrange
            _employeeRepositoryMock.CheckExistsById(employeeTest.Id, default).Returns(employeeAsNull);

            // Act
            var result = await _deleteEmployeeHandler.Handle(_deleteEmployeeCommand, default);

            // Assert
            result.IsFailure.Should().BeTrue();
        }


        [Fact]
        public async void Handle_Should_ReturnTrueResult_WhenEmployeeIsFound()
        {
            // Arrange
            _employeeRepositoryMock.CheckExistsById(employeeTest.Id, default).Returns(employeeTest);
            
            // Act
            var result = await _deleteEmployeeHandler.Handle(_deleteEmployeeCommand, default);

            // Assert
            result.IsSuccess.Should().BeTrue();
        }



        [Fact]
        public async void Handle_Should_CallUnitOfWorkAndSaveChangesAsync()
        {
            // Arrange
            _employeeRepositoryMock.CheckExistsById(employeeTest.Id, default).Returns(employeeTest);

            // Act
            await _deleteEmployeeHandler.Handle(_deleteEmployeeCommand, default);

            // Assert
            _employeeRepositoryMock.Received(1).DeleteEmployee(Arg.Is<Employee>(e => e == employeeTest), default);
            await _unitOfWorkMock.Received(1).SaveChangesAsync(Arg.Any<CancellationToken>());
        }
        
    }