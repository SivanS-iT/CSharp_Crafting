using Application.Commands;
using Domain.DTOs;
using Domain.Features.Employee;
using MediatR;

namespace Application.Handlers.EmployeeHandler
{
    public class CreateEmployeeHandler(IEmployeeRepository employeeRepository) : IRequestHandler<CreateEmployeeCommand, ServiceResponse>
    {
        
        private readonly IEmployeeRepository _employeeRepository = employeeRepository;

        public async Task<ServiceResponse> Handle(CreateEmployeeCommand request, CancellationToken cancellationToken)
        {
            var check = await _employeeRepository.CheckExists(request.Employee.Name, cancellationToken);
            if (check != null)
            {
                return new ServiceResponse(false, "User already exists");
            }

            var employee = _employeeRepository.CreateEmployee(request.Employee, cancellationToken);
            return new ServiceResponse(true, "User added");
        }
    }
}
