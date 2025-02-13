using Application.Abstractions.Data;
using Application.Abstractions.Messaging;
using Domain.Abstractions;
using Domain.Features.Employee;

namespace Application.Employees.CreateEmployee
{
    /// <summary>
    /// Handler for creating employee.
    /// </summary>
    /// <param name="employeeRepository"></param>
    public class CreateEmployeeCommandHandler(IEmployeeRepository employeeRepository, IUnitOfWork unitOfWork)
        : ICommandHandler<CreateEmployeeCommand>
    {
        public async Task<Result> Handle(CreateEmployeeCommand request, CancellationToken cancellationToken)
        {
            var employee = await employeeRepository.CheckExists(request.Employee.Email, cancellationToken);
            if (employee != null)
            {
                return  Result.Failure(EmployeeErrors.Exists(employee.Email));
            }

            employeeRepository.CreateEmployee(request.Employee, cancellationToken);

            await unitOfWork.SaveChangesAsync(cancellationToken);
            
            return Result.Success();
        }
    }
}