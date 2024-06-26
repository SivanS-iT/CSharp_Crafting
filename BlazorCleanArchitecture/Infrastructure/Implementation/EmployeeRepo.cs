using Application.Contracts;
using Application.DTOs;
using Domain.Entities;
using Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Implementation
{
    public class EmployeeRepo : IEmpoyee
    {
        private readonly AppDbContext _appDbContext;
        public EmployeeRepo(AppDbContext appDbContext)
        {
            this._appDbContext = appDbContext;
        }
        public Task<ServiceResponse> AddAsync(Employee employee)
        {
            throw new NotImplementedException();
        }

        public Task<ServiceResponse> DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<List<Employee>> GetAsync()
        {
            throw new NotImplementedException();
        }

        public Task<Employee> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<ServiceResponse> UpdateAsync(Employee employee)
        {
            throw new NotImplementedException();
        }
    }
}
