using Domain.DTOs;
using MediatR;

namespace Application.Commands
{
    public record DeleteEmployeeByIdCommand(int Id) : IRequest<ServiceResponse>;
}
