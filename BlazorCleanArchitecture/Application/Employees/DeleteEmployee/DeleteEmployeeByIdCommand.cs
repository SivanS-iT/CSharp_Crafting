
using Application.Abstractions.Messaging;

namespace Application.Employees.DeleteEmployee
{
    /// <summary>
    /// Delete Employee Command
    /// </summary>
    /// <param name="Id"></param>
    public sealed record DeleteEmployeeByIdCommand(int Id) : ICommand;
}
