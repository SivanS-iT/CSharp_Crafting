using Domain.DTOs;
using MediatR;

namespace Application.Commands
{
    public class DeleteEmployeeByIdCommand : IRequest<ServiceResponse>
    {
        public int Id { get; set; }
    }
}
