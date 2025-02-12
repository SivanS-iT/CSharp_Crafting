using Application.Abstractions.Data;
using Application.Commands.EmployeeCommands;
using Domain.DTOs;
using Domain.Features.Employee;
using MediatR;

namespace Application.Handlers.EmployeeHandler
{
    /// <summary>
    /// Handler for deleting employee.
    /// </summary>
    /// <param name="employeeRepository"></param>
    public class DeleteEmployeeByIdHandler(IEmployeeRepository employeeRepository, IUnitOfWork unitOfWork) 
        : IRequestHandler<DeleteEmployeeByIdCommand, ServiceResponse>
    {
        private readonly IEmployeeRepository _employeeRepository = employeeRepository;

        public async Task<ServiceResponse> Handle(DeleteEmployeeByIdCommand request, CancellationToken cancellationToken)
        {
            var check = _employeeRepository.CheckExistsById(request.Id, cancellationToken);
            if (check.Result == null)
            {
                return new ServiceResponse(false, "User not found");
            }

            var successfulResponse = await _employeeRepository.DeleteEmployee(check.Result, cancellationToken);

            await unitOfWork.SaveChangesAsync(cancellationToken);
            
            return successfulResponse;
        }
    }
}
