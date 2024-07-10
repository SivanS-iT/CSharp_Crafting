using Application.Queries.EmployeeQuery;
using Domain.Features.Employee;
using MediatR;

namespace Application.Handlers.EmployeeHandler
{
    public class GetEmployeeListHandler(IEmployeeRepository  employeeRepository) : IRequestHandler<GetEmployeeListQuery, List<Employee>>
    {
        private readonly IEmployeeRepository _employeeRepository = employeeRepository;

        public async Task<List<Employee>> Handle(GetEmployeeListQuery request, CancellationToken cancellationToken)
        {
            return await _employeeRepository.GetEmployees(cancellationToken);
        }
    }
}
