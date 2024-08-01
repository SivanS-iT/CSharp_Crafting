using Domain.DTOs;
using Domain.Features.Employee;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class EmployeeRepository(AppDbContext appDbContext) : IEmployeeRepository
    {
        private readonly AppDbContext _appDbContext = appDbContext;


        public async Task<Employee?> CheckExists(string name, CancellationToken cancellationToken)
        {
            return await _appDbContext.Employees.FirstOrDefaultAsync(
                u => u.Name.ToLower().Equals(name.ToLower()), cancellationToken);
        }

        public async Task<Employee?> CheckExistsById(int employeeId, CancellationToken cancellationToken)
        {
            return await _appDbContext.Employees.FirstOrDefaultAsync(u => u.Id == employeeId);
        }



        public async Task<List<Employee>> GetEmployees(CancellationToken cancellationToken)
        {
            return await _appDbContext.Employees.ToListAsync(cancellationToken: cancellationToken);
        }

        public async Task<Employee?> GetEmployeeById(int employeeId, CancellationToken cancellationToken)
        {
            return await _appDbContext.Employees.FindAsync(employeeId, cancellationToken);
        }

        public async Task<Employee> CreateEmployee(CreateEmployeeRequest createEmployeeRequest, CancellationToken cancellationToken)
        {
            var employee = new Employee
            {
                Name = createEmployeeRequest.Name,
                Address = createEmployeeRequest.Address
            };
            await _appDbContext.Employees.AddAsync(employee);
            await _appDbContext.SaveChangesAsync(cancellationToken);
            return employee;
        }

        public async Task<ServiceResponse> UpdateEmployee(Employee employee, CancellationToken cancellationToken)
        {
            _appDbContext.Update(employee);
            await _appDbContext.SaveChangesAsync(cancellationToken);
            return new ServiceResponse(true, "User updated");
        }

        public async Task<ServiceResponse> DeleteEmployee(Employee employee, CancellationToken cancellationToken)
        {
            _appDbContext.Employees.Remove(employee);
            await _appDbContext.SaveChangesAsync(cancellationToken);
            return new ServiceResponse(true, "User deleted");
        }
    }
}
