
using Application.Commands;
using Application.Commands.EmployeeCommands;
using Domain.DTOs;
using Infrastructure.Data;
using MediatR;

namespace Infrastructure.Handlers.EmployeeHandler
{
    public class DeleteEmployeeByIdHandler : IRequestHandler<DeleteEmployeeByIdCommand, ServiceResponse>
    {
        private readonly AppDbContext _appDbContext;

        public DeleteEmployeeByIdHandler(AppDbContext appDbContext)
        {
            this._appDbContext = appDbContext;
        }

        public async Task<ServiceResponse> Handle(DeleteEmployeeByIdCommand request, CancellationToken cancellationToken)
        {
            var employee = await _appDbContext.Employees.FindAsync(request.Id);
            if (employee == null)
            {
                return new ServiceResponse(false, "User not found");
            }
            _appDbContext.Employees.Remove(employee);
            await _appDbContext.SaveChangesAsync(cancellationToken);
            return new ServiceResponse(true, "User deleted");
        }
    }
}
