using Application.Abstractions.Messaging;
using Domain.Features.Employee;

namespace Application.Employees.CreateEmployee
{
    /// <summary>
    /// Command for Creating employees
    /// </summary>
    /// <param name="Employee"></param>
    public sealed record CreateEmployeeCommand(CreateEmployeeRequest Employee) : ICommand;
}
