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
            var employeeExists = await employeeRepository.CheckExists(request.Employee.Email, cancellationToken);
            if (employeeExists != null)
            {
                return  Result.Failure<int>(EmployeeErrors.EmployeeExists);
            }
            

            var addEmployee = new Employee()
            {
                Name = request.Employee.Name,
                Address = request.Employee.Address,
                Email = request.Employee.Email

            };

            await employeeRepository.Add(addEmployee, cancellationToken);
            await unitOfWork.SaveChangesAsync(cancellationToken);
            
            return Result.Success<int>(addEmployee.Id);
        }
    }
}