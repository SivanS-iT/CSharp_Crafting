using Application.Commands;
using Application.Contracts;
using Application.DTOs;
using Application.Queries.EmployeeQuery;
using Domain.Entities;
using Infrastructure.Data;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Handlers.EmployeeHandler
{
    public class UpdateEmployeeHandler : IRequestHandler<UpdateEmployeeCommand, ServiceResponse>
    {
        private readonly AppDbContext _appDbContext;
        public UpdateEmployeeHandler(AppDbContext appDbContext)
        {
            this._appDbContext = appDbContext;
        }

        public async Task<ServiceResponse> Handle(UpdateEmployeeCommand request, CancellationToken cancellationToken)
        {
            _appDbContext.Update(request.Employee);
            await _appDbContext.SaveChangesAsync();
            return new ServiceResponse(true, "User updated");
        }
    }
}
