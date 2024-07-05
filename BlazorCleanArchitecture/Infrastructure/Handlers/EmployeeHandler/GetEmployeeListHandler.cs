using Application.Queries.EmployeeQuery;
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
    public class GetEmployeeListHandler : IRequestHandler<GetEmployeeListQuery, List<Employee>>
    {
        private readonly AppDbContext _appDbContext;
        public GetEmployeeListHandler(AppDbContext appDbContext)
        {
            this._appDbContext = appDbContext; 
        }
        public async Task<List<Employee>> Handle(GetEmployeeListQuery request, CancellationToken cancellationToken)
        {
            return await _appDbContext.Employees.AsNoTracking().ToListAsync(cancellationToken: cancellationToken);
        }
    }
}
