using Domain.DTOs;
using Domain.Features.Employee;
using MediatR;

namespace Application.Commands.EmployeeCommands
{
    /// <summary>
    /// Update emplyee command
    /// </summary>
    /// <param name="employee"></param>
    public record UpdateEmployeeCommand(Employee employee) : IRequest<ServiceResponse>;
}
