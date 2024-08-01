using Domain.DTOs;
using Domain.Features.Employee;
using MediatR;

namespace Application.Commands
{
    public record UpdateEmployeeCommand(Employee employee) : IRequest<ServiceResponse>;
}
