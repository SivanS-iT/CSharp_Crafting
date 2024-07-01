using Application.Contracts;
using Application.DTOs;
using Domain.Entities;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Implementation
{
    public class EmployeeRepo : IEmployee
    {
        private readonly AppDbContext _appDbContext;
        public EmployeeRepo(AppDbContext appDbContext)
        {
            this._appDbContext = appDbContext;
        }
        public async Task<ServiceResponse> AddAsync(Employee employee)
        {
            _appDbContext.Employees.Add(employee);
            await SaveChangesAsync();
            return new ServiceResponse(true, "User added");
        }

        public async Task<ServiceResponse> DeleteAsync(int id)
        {
            var employee = await _appDbContext.Employees.FindAsync(id);
            if (employee == null)
            {
                return new ServiceResponse(false, "User not found");
            }

            _appDbContext.Employees.Remove(employee);
            await SaveChangesAsync();
            return new ServiceResponse(true, "User deleted");
        }

        public async Task<List<Employee>> GetAsync() => await _appDbContext.Employees.AsNoTracking().ToListAsync();

        public async Task<Employee> GetByIdAsync(int id) => await _appDbContext.Employees.FindAsync(id);

        public async Task<ServiceResponse> UpdateAsync(Employee employee)
        {
            _appDbContext.Update(employee);
            await SaveChangesAsync();
            return new ServiceResponse(true, "User updated");
        }

        private async Task SaveChangesAsync() => await _appDbContext.SaveChangesAsync();
    }
}
