
using Domain.Abstractions;

namespace Domain.Features.Employee
{
    
    /// <summary>
    /// Repository interface for doing database operations on Employee entity
    /// </summary>
    public interface IEmployeeRepository : IGenericRepository<Employee>
    {
        public Task<Employee?> CheckExists(string name, CancellationToken cancellationToken);
        public Task<Employee?> CheckExistsById(int employeeId, CancellationToken cancellationToken);
    }
}
