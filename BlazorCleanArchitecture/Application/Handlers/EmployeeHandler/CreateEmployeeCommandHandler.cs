using Application.Commands.EmployeeCommands;
using Domain.DTOs;
using Domain.Features.Employee;
using MediatR;

namespace Application.Handlers.EmployeeHandler
{
    /// <summary>
    /// Handler for creating employee.
    /// </summary>
    /// <param name="employeeRepository"></param>
    public class CreateEmployeeCommandHandler(IEmployeeRepository employeeRepository)
        : IRequestHandler<CreateEmployeeCommand, ServiceResponse>
    {
        public async Task<ServiceResponse> Handle(CreateEmployeeCommand request, CancellationToken cancellationToken)
        {
            var check = await employeeRepository.CheckExists(request.Employee.Name, cancellationToken);
            if (check != null)
            {
                return new ServiceResponse(false, "User already exists");
            }

            await employeeRepository.CreateEmployee(request.Employee, cancellationToken);
            return new ServiceResponse(true, "User added");
        }
    }
}