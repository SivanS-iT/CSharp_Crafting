using Application.Abstractions.Data;
using Application.Abstractions.Messaging;
using Application.Commands.EmployeeCommands;
using Domain.Abstractions;
using Domain.Features.Employee;

namespace Application.Handlers.EmployeeHandler
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
            var employee = await _employeeRepository.CheckExists(request.employee.Name, cancellationToken);
            if (employee != null)
            {
                return  Result.Failure(EmployeeErrors.Exists(employee.Name));
            }

            await _employeeRepository.UpdateEmployee(request.employee, cancellationToken);

            await unitOfWork.SaveChangesAsync(cancellationToken);
            
            return Result.Success();
        }
    }
}
