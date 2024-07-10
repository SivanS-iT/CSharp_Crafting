using Domain.DTOs;
using Domain.Features.Employee;
using MediatR;

namespace Application.Commands
{
    public class UpdateEmployeeCommand : IRequest<ServiceResponse>
    {
        public Employee? Employee { get; set; }
    }
}
