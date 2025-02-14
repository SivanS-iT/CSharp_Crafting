using Domain.Features.Employee;
using Infrastructure.Data;
using Infrastructure.Repositories.Generic;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    internal class EmployeeRepository(AppDbContext appDbContext) : GenericRepository<Employee>(appDbContext), IEmployeeRepository
    {
        public async Task<Employee?> CheckExists(string email, CancellationToken cancellationToken)
        {
            return await appDbContext.Employees.FirstOrDefaultAsync(
                u => u.Email.ToLower().Equals(email.ToLower()), cancellationToken);
        }

        public async Task<Employee?> CheckExistsById(int employeeId, CancellationToken cancellationToken)
        {
            return await appDbContext.Employees.FirstOrDefaultAsync(u => u.Id == employeeId,
                cancellationToken: cancellationToken);
        }

        
    }
}