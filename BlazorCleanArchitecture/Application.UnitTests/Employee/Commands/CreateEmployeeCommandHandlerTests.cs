

using Application.Commands;
using Domain.Features.Employee;
using Moq;

namespace Application.UnitTests.Employee.Commands
{
    //private readonly Mock<IEmployeeRepository> _employeeRpositoryMock;

    public class CreateEmployeeCommandHandlerTests
    {
        [Fact]
        public void Handle_Should_ReturnHailureResult_WhenEmployeeNameIsNotUnique()
        {
            // Arrange

            // Act

            // Assert
        }
    }
}
