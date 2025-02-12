using Domain.Abstractions;
using Domain.DTOs;

namespace Domain.Features.Employee
{
    public interface IEmployeeRepository
    {
        public Task<Employee?> CheckExists(string name, CancellationToken cancellationToken);
        public Task<Employee?> CheckExistsById(int employeeId, CancellationToken cancellationToken);


        public Task<Employee?> GetEmployeeById(int employeeId, CancellationToken cancellationToken);
        public Task<List<Employee>> GetEmployees(CancellationToken cancellationToken);
        public Task<Employee> CreateEmployee(CreateEmployeeRequest employee, CancellationToken cancellationToken);
        public Task<ServiceResponse> UpdateEmployee(Employee employee, CancellationToken cancellationToken);
        public Task<ServiceResponse> DeleteEmployee(Employee employee, CancellationToken cancellationToken);

    }
}
