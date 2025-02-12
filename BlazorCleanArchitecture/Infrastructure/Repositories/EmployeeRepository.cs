using Domain.Abstractions;
using Domain.DTOs;
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

        public async Task<Employee> CreateEmployee(CreateEmployeeRequest createEmployeeRequest,
            CancellationToken cancellationToken)
        {
            var employee = new Employee
            {
                Name = createEmployeeRequest.Name,
                Address = createEmployeeRequest.Address
            };
            await appDbContext.Employees.AddAsync(employee, cancellationToken);
            await appDbContext.SaveChangesAsync(cancellationToken);
            return employee;
        }

        public async Task<ServiceResponse> UpdateEmployee(Employee employee, CancellationToken cancellationToken)
        {
            appDbContext.Update(employee);
            await appDbContext.SaveChangesAsync(cancellationToken);
            return new ServiceResponse(true, "User updated");
        }

        public async Task<ServiceResponse> DeleteEmployee(Employee employee, CancellationToken cancellationToken)
        {
            appDbContext.Employees.Remove(employee);
            await appDbContext.SaveChangesAsync(cancellationToken);
            return new ServiceResponse(true, "User deleted");
        }
    }
}