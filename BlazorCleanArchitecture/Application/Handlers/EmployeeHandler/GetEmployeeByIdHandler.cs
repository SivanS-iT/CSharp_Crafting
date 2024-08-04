using Application.Queries.EmployeeQuery;
using Domain.Features.Employee;
using MediatR;

namespace Application.Handlers.EmployeeHandler
{
    /// <summary>
    /// Handler for Get employee by ID.
    /// </summary>
    /// <param name="employeeRepository"></param>
    public class GetEmployeeByIdHandler(IEmployeeRepository employeeRepository) : IRequestHandler<GetEmployeeByIdQuery, Employee>
    {
        private readonly IEmployeeRepository _employeeRepository = employeeRepository;
        
        public async Task<Employee?> Handle(GetEmployeeByIdQuery request, CancellationToken cancellationToken)
        {
            return await _employeeRepository.GetEmployeeById(request.Id, cancellationToken);
        }
    }
}
