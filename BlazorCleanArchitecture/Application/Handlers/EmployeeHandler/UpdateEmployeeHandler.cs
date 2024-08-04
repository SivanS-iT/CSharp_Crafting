using Application.Commands;
using Application.Commands.EmployeeCommands;
using Domain.DTOs;
using Domain.Features.Employee;
using MediatR;

namespace Application.Handlers.EmployeeHandler
{
    /// <summary>
    /// Handler for updating employee.
    /// </summary>
    /// <param name="employeeRepository"></param>
    public class UpdateEmployeeHandler(IEmployeeRepository employeeRepository) : IRequestHandler<UpdateEmployeeCommand, ServiceResponse>
    {
        private readonly IEmployeeRepository _employeeRepository = employeeRepository;

        public async Task<ServiceResponse> Handle(UpdateEmployeeCommand request, CancellationToken cancellationToken)
        {
            var check = await _employeeRepository.CheckExists(request.employee.Name, cancellationToken);
            if (check != null)
            {
                return new ServiceResponse(false, "User already exists");
            }

            var serviceResponse = await _employeeRepository.UpdateEmployee(request.employee, cancellationToken);
            
            
            return serviceResponse;
        }
    }
}
