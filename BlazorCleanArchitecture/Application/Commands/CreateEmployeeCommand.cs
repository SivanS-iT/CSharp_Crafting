using Domain.DTOs;
using Domain.Features.Employee;
using MediatR;

namespace Application.Commands
{
    public record CreateEmployeeCommand(Employee Employee) : IRequest<ServiceResponse>;
}
