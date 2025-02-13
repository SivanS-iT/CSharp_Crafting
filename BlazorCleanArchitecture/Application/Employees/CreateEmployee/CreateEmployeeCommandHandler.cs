using Application.Abstractions.Data;
using Application.Abstractions.Messaging;
using Domain.Features.Employee;
using Domain.Shared;

namespace Application.Employees.CreateEmployee
{
    /// <summary>
    /// Handler for creating employee.
    /// </summary>
    /// <param name="employeeRepository"></param>
    public class CreateEmployeeCommandHandler(IEmployeeRepository employeeRepository, IUnitOfWork unitOfWork)
        : ICommandHandler<CreateEmployeeCommand, int>
    {
        public async Task<Result<int>> Handle(CreateEmployeeCommand request, CancellationToken cancellationToken)
        {
            var employee = await employeeRepository.CheckExists(request.Employee.Email, cancellationToken);
            if (employee != null)
            {
                return  Result.Failure<int>(EmployeeErrors.Exists(employee.Email));
            }

            employeeRepository.CreateEmployee(request.Employee, cancellationToken);
            await unitOfWork.SaveChangesAsync(cancellationToken);
            
            var createdEmployee = await employeeRepository.CheckExists(request.Employee.Email, cancellationToken);

            return Result.Success<int>(createdEmployee.Id);
        }
    }
}