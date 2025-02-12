using Domain.Features.Employee;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class EmployeeRepository(AppDbContext appDbContext) : IEmployeeRepository
    {
        public async Task<Employee?> CheckExists(string name, CancellationToken cancellationToken)
        {
            return await appDbContext.Employees.FirstOrDefaultAsync(
                u => u.Name.ToLower().Equals(name.ToLower()), cancellationToken);
        }

        public async Task<Employee?> CheckExistsById(int employeeId, CancellationToken cancellationToken)
        {
            return await appDbContext.Employees.FirstOrDefaultAsync(u => u.Id == employeeId,
                cancellationToken: cancellationToken);
        }

        public async Task<List<Employee>> GetEmployees(CancellationToken cancellationToken)
        {
            return await appDbContext.Employees.ToListAsync(cancellationToken: cancellationToken);
        }

        public async Task<Employee?> GetEmployeeById(int employeeId, CancellationToken cancellationToken)
        {
            return await appDbContext.Employees.FindAsync(new object?[] { employeeId, cancellationToken },
                cancellationToken: cancellationToken);
        }

        public void CreateEmployee(CreateEmployeeRequest createEmployeeRequest,
            CancellationToken cancellationToken)
        {
            var employee = new Employee
            {
                Name = createEmployeeRequest.Name,
                Address = createEmployeeRequest.Address
            };
            appDbContext.Employees.AddAsync(employee, cancellationToken);
        }

        public void UpdateEmployee(Employee employee, CancellationToken cancellationToken)
        { 
            appDbContext.Update(employee);
        }

        public async void DeleteEmployee(Employee employee, CancellationToken cancellationToken)
        { 
            appDbContext.Employees.Remove(employee);
        }
    }
}