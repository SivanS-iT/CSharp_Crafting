using Application.Commands;
using Domain.DTOs;
using Domain.Features.Employee;
using MediatR;

namespace Application.Handlers.EmployeeHandler
{
    public class UpdateEmployeeHandler(IEmployeeRepository employeeRepository) : IRequestHandler<UpdateEmployeeCommand, ServiceResponse>
    {
        private readonly IEmployeeRepository _employeeRepository = employeeRepository;

        public async Task<ServiceResponse> Handle(UpdateEmployeeCommand request, CancellationToken cancellationToken)
        {
            var serviceResponse = await _employeeRepository.UpdateEmployee(request.employee, cancellationToken);
            return serviceResponse;
        }
    }
}
