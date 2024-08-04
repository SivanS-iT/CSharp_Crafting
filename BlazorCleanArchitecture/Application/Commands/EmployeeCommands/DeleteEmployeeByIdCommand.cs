using Domain.DTOs;
using MediatR;

namespace Application.Commands.EmployeeCommands
{
    /// <summary>
    /// Delete Employee Command
    /// </summary>
    /// <param name="Id"></param>
    public record DeleteEmployeeByIdCommand(int Id) : IRequest<ServiceResponse>;
}
