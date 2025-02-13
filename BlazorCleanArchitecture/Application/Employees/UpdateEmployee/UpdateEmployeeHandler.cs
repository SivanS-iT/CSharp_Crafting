using Application.Abstractions.Data;
using Application.Abstractions.Messaging;
using Domain.Features.Employee;
using Domain.Shared;

namespace Application.Employees.UpdateEmployee
{
    /// <summary>
    /// Handler for updating employee.
    /// </summary>
    /// <param name="employeeRepository"></param>
    public class UpdateEmployeeHandler(IEmployeeRepository employeeRepository, IUnitOfWork unitOfWork)
        : ICommandHandler<UpdateEmployeeCommand>
    {
        private readonly IEmployeeRepository _employeeRepository = employeeRepository;

        public async Task<Result> Handle(UpdateEmployeeCommand request, CancellationToken cancellationToken)
        {
            var employee = await _employeeRepository.CheckExistsById(request.employee.Id, cancellationToken);
            if (employee == null)
            {
                return  Result.Failure(EmployeeErrors.NotFound(request.employee.Id));
            }

            _employeeRepository.UpdateEmployee(request.employee, cancellationToken);

            await unitOfWork.SaveChangesAsync(cancellationToken);
            
            return Result.Success();
        }
    }
}
