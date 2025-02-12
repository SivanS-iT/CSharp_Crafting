
namespace Domain.Features.Employee
{
    public interface IEmployeeRepository
    {
        public Task<Employee?> CheckExists(string name, CancellationToken cancellationToken);
        public Task<Employee?> CheckExistsById(int employeeId, CancellationToken cancellationToken);


        public Task<Employee?> GetEmployeeById(int employeeId, CancellationToken cancellationToken);
        public Task<List<Employee>> GetEmployees(CancellationToken cancellationToken);
        public void CreateEmployee(CreateEmployeeRequest employee, CancellationToken cancellationToken);
        public void UpdateEmployee(Employee employee, CancellationToken cancellationToken);
        public void DeleteEmployee(Employee employee, CancellationToken cancellationToken);

    }
}
