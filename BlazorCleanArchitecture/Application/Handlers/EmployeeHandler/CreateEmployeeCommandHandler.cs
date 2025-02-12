using Application.Abstractions.Data;
using Application.Abstractions.Messaging;
using Application.Commands.EmployeeCommands;
using Domain.Abstractions;
using Domain.DTOs;
using Domain.Features.Employee;
using MediatR;

namespace Application.Handlers.EmployeeHandler
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
            var check = await employeeRepository.CheckExists(request.Employee.Name, cancellationToken);
            if (check != null)
            {
                return  Result.Failure(EmployeeErrors.Exists(check.Name));
            }

            await employeeRepository.CreateEmployee(request.Employee, cancellationToken);

            await unitOfWork.SaveChangesAsync(cancellationToken);
            
            return Result.Success();
        }
    }
}