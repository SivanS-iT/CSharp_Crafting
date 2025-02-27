using Application.Abstractions.Data;
using Application.Employees.UpdateEmployee;
using Domain.Features.Employee;
using FluentAssertions;
using NSubstitute;
using NSubstitute.ReturnsExtensions;

namespace Application.UnitTests.Employees.Commands
{
    public class UpdateEmployeeCommandHandlerTests
    {
        private readonly IEmployeeRepository _employeeRpositoryMock;
        private readonly IUnitOfWork _unitOfWork;
        private readonly UpdateEmployeeHandler _updateEmployeeHandler;
        private readonly UpdateEmployeeCommand _updateEmployeeCommand;
        
        public UpdateEmployeeCommandHandlerTests()
        {
            _employeeRpositoryMock = Substitute.For<IEmployeeRepository>();
            _unitOfWork = Substitute.For<IUnitOfWork>();
            _updateEmployeeHandler = new UpdateEmployeeHandler(_employeeRpositoryMock, _unitOfWork);
            _updateEmployeeCommand = new UpdateEmployeeCommand(employeeTest);
        }

        private static readonly Employee employeeTest = new()
        {
            Id = 1,
            Address = "Testing Update",
            Name = "Ivan",
        };
        private static readonly Employee? employeeAsNull = null;

        [Fact]
        public async void Handle_Should_ReturnFailureResult_WhenEmployeeIsNotFound()
        {
            // Arrange
            _employeeRpositoryMock.CheckExistsById(employeeTest.Id, default).Returns(employeeAsNull);

            // Act
            var result = await _updateEmployeeHandler.Handle(_updateEmployeeCommand, default);

            // Assert
            result.IsFailure.Should().BeTrue();
        }


        [Fact]
        public async void Handle_Should_ReturnTrueResult_WhenEmployeeIsFound()
        {
            // Arrange
            _employeeRpositoryMock.CheckExistsById(employeeTest.Id, default).Returns(employeeTest);
            
            // Act
            var result = await _updateEmployeeHandler.Handle(_updateEmployeeCommand, default);

            // Assert
            result.IsSuccess.Should().BeTrue();
        }


        [Fact]
        public async void Handle_Should_CallUnitOfWorkAndSaveChangesAsync()
        {
            // Arrange
            _employeeRpositoryMock.CheckExistsById(employeeTest.Id, default).Returns(employeeTest);

            // Act
            var result = await _updateEmployeeHandler.Handle(_updateEmployeeCommand, default);

            // Assert
            _employeeRpositoryMock.Received(1).Update(Arg.Is<Employee>(e => e == employeeTest));
            await _unitOfWork.Received(1).SaveChangesAsync(Arg.Any<CancellationToken>());
        }
    }
}
