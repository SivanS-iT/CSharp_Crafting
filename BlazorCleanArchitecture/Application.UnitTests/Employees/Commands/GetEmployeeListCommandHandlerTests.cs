using Application.Commands;
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
    public class GetEmployeeListCommandHandlerTests
    {
        private readonly IEmployeeRepository _employeeRpositoryMock;
        private readonly GetEmployeeListHandler _getEmployeeListHandler;
        private readonly GetEmployeeListQuery _createEmployeeCommand;


        private static readonly List<Employee> employeeTestList =
        [
            new Employee { Id = 1, Name = "Ivan", Address ="Random address"},
            new Employee { Id = 2, Name = "Hrvoje", Address ="Hrvoje address"},
            new Employee { Id = 3, Name = "Colin", Address ="Colin address"},
        ];

        public GetEmployeeListCommandHandlerTests()
        {
            _employeeRpositoryMock = Substitute.For<IEmployeeRepository>();
            _getEmployeeListHandler = new GetEmployeeListHandler(_employeeRpositoryMock);
            _createEmployeeCommand = new GetEmployeeListQuery();
        }
        private static readonly Employee? employeeAsNull = null;



        /// <summary>
        /// Returns List of employee
        /// </summary>
        [Fact]
        public async void Handle_Should_ReturnEmployeeList()
        {
            // Arrange
            _employeeRpositoryMock.GetEmployees(default).Returns(employeeTestList);

            // Act
            var result = await _getEmployeeListHandler.Handle(_createEmployeeCommand, default);

            // Assert
            result.As<List<Employee>>();
            result.Count.Should().Be(3);
            result.First().Id.Should().Be(employeeTestList[0].Id);
        }

        /// <summary>
        /// Should call repository
        /// </summary>
        [Fact]
        public async void Handle_Should_CallRepository()
        {
            // Act
            await _getEmployeeListHandler.Handle(_createEmployeeCommand, default);

            // Assert
            await _employeeRpositoryMock.Received(1).GetEmployees(default);
        }

    }
}
