using Application.Abstractions.Messaging;
using Domain.Features.Employee;

namespace Application.Employees.UpdateEmployee
{
    /// <summary>
    /// Update emplyee command
    /// </summary>
    /// <param name="employee"></param>
    public sealed record UpdateEmployeeCommand(Employee employee) : ICommand;
}
