using Application.Abstractions.Messaging;
using Domain.Features.Employee;

namespace Application.Commands.EmployeeCommands
{
    /// <summary>
    /// Update emplyee command
    /// </summary>
    /// <param name="employee"></param>
    public record UpdateEmployeeCommand(Employee employee) : ICommand;
}
