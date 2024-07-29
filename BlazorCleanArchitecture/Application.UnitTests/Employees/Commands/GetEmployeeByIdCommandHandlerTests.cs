using Application.Handlers.EmployeeHandler;
using Application.Queries.EmployeeQuery;
using Domain.Features.Employee;
using FluentAssertions;
using NSubstitute;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.UnitTests.Employees.Commands
{
    public class GetEmployeeByIdCommandHandlerTests
    {
        private readonly IEmployeeRepository _employeeRpositoryMock;
        private readonly GetEmployeeByIdHandler _getEmployeeByIdHandler;
        private readonly GetEmployeeByIdQuery _getEmployeeCommand;


        private static readonly Employee employeeTest = new()
        {
            Id = 1,
            Address = "Testing address",
            Name = "Ivan",
        };
        private static readonly Employee? employeeAsNull = null;
        private static readonly int empId = 1;


        public GetEmployeeByIdCommandHandlerTests()
        {
            _employeeRpositoryMock = Substitute.For<IEmployeeRepository>();
            _getEmployeeByIdHandler = new GetEmployeeByIdHandler(_employeeRpositoryMock);
            _getEmployeeCommand = new GetEmployeeByIdQuery(empId);
        }



        /// <summary>
        /// Returns List of employee
        /// </summary>
        [Fact]
        public async void Handle_Should_ReturnEmployeeList()
        {
            // Arrange
            _employeeRpositoryMock.GetEmployeeById(empId, default).Returns(employeeTest);

            // Act
            var result = await _getEmployeeByIdHandler.Handle(_getEmployeeCommand, default);

            // Assert
            result.Should().Be(employeeTest);
        }

        /// <summary>
        /// Should call repository
        /// </summary>
        [Fact]
        public async void Handle_Should_CallRepository()
        {
            // Act
            await _getEmployeeByIdHandler.Handle(_getEmployeeCommand, default);

            // Assert
            await _employeeRpositoryMock.Received(1).GetEmployeeById(empId, default);
        }

    }
}
