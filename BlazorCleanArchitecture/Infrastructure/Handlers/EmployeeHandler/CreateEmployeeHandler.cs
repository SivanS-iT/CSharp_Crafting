using Application.Commands;
using Application.DTOs;
using Domain.Entities;
using Infrastructure.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Handlers.EmployeeHandler
{
    public class CreateEmployeeHandler : IRequestHandler<CreateEmployeeCommand, ServiceResponse>
    {
        private readonly AppDbContext _appDbContext;

        public CreateEmployeeHandler(AppDbContext appDbContext)
        {
            this._appDbContext = appDbContext;
        }
        public async Task<ServiceResponse> Handle(CreateEmployeeCommand request, CancellationToken cancellationToken)
        {
            var check = await _appDbContext.Employees.FirstOrDefaultAsync(u => u.Name.ToLower() == request.Employee.Name.ToLower());
            if (check != null)
            {
                return new ServiceResponse(false, "User already exists");
            }

            _appDbContext.Employees.Add(request.Employee);
            await _appDbContext.SaveChangesAsync(cancellationToken);
            return new ServiceResponse(true, "User added");
        }
    }
}
