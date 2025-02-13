using Application.Abstractions.Data;
using Application.Abstractions.Messaging;
using Domain.Features.Employee;
using Domain.Shared;

namespace Application.Employees.DeleteEmployee
{
    /// <summary>
    /// Handler for deleting employee.
    /// </summary>
    /// <param name="employeeRepository"></param>
    public class DeleteEmployeeByIdHandler(IEmployeeRepository employeeRepository, IUnitOfWork unitOfWork) 
        : ICommandHandler<DeleteEmployeeByIdCommand>
    {
        private readonly IEmployeeRepository _employeeRepository = employeeRepository;

        public async Task<Result> Handle(DeleteEmployeeByIdCommand request, CancellationToken cancellationToken)
        {
            var employee = await _employeeRepository.CheckExistsById(request.Id, cancellationToken);
            if (employee == null)
            {
                return Result.Failure(EmployeeErrors.NotFound(request.Id));
            }

            _employeeRepository.DeleteEmployee(employee, cancellationToken);

            await unitOfWork.SaveChangesAsync(cancellationToken);
            
            return Result.Success();
        }
    }
}
