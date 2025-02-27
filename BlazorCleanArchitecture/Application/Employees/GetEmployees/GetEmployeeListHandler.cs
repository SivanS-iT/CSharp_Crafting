using Application.Abstractions.Messaging;
using Domain.Features.Employee;
using Domain.Shared;

namespace Application.Employees.GetEmployees
{
    /// <summary>
    /// Handler for getting employees.
    /// </summary>
    /// <param name="employeeRepository"></param>
    public class GetEmployeeListHandler(IEmployeeRepository  employeeRepository) : IQueryHandler<GetEmployeeListQuery, List<Employee>>
    {
        private readonly IEmployeeRepository _employeeRepository = employeeRepository;

        public async Task<Result<List<Employee>>> Handle(GetEmployeeListQuery request, CancellationToken cancellationToken)
        {
            return await _employeeRepository.GetAll(cancellationToken);
        }
    }
}
