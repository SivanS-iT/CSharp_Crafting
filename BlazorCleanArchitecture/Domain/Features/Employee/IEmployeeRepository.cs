
using Domain.Abstractions;

namespace Domain.Features.Employee
{
    public interface IEmployeeRepository : IGenericRepository<Employee>
    {
        public Task<Employee?> CheckExists(string name, CancellationToken cancellationToken);
        public Task<Employee?> CheckExistsById(int employeeId, CancellationToken cancellationToken);
        
    }
}
