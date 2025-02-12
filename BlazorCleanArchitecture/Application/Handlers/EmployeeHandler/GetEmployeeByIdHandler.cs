using Application.Abstractions.Data;
using Application.Abstractions.Messaging;
using Application.Queries.EmployeeQuery;
using Domain.Abstractions;
using Domain.Features.Employee;
using MediatR;

namespace Application.Handlers.EmployeeHandler
{
    /// <summary>
    /// Handler for Get employee by ID.
    /// </summary>
    /// <param name="employeeRepository"></param>
    public class GetEmployeeByIdHandler(IEmployeeRepository employeeRepository, IUnitOfWork unitOfWork) 
        : IQueryHandler<GetEmployeeByIdQuery, Employee>
    {
        private readonly IEmployeeRepository _employeeRepository = employeeRepository;
        
        public async Task<Result<Employee>> Handle(GetEmployeeByIdQuery request, CancellationToken cancellationToken)
        {
            var employee = await _employeeRepository.CheckExistsById(request.Id, cancellationToken);
            if (employee == null)
            {
                return Result.Failure<Employee>(EmployeeErrors.NotFound(request.Id));
            }

            var response = await _employeeRepository.GetEmployeeById(employee.Id, cancellationToken);
            
            return Result.Success<Employee>(response);   }
    }
}
