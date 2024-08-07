﻿using Application.Commands;
using Domain.DTOs;
using Domain.Features.Employee;
using MediatR;

namespace Application.Handlers.EmployeeHandler
{
    public class DeleteEmployeeByIdHandler(IEmployeeRepository employeeRepository) : IRequestHandler<DeleteEmployeeByIdCommand, ServiceResponse>
    {
        private readonly IEmployeeRepository _employeeRepository = employeeRepository;

        public async Task<ServiceResponse> Handle(DeleteEmployeeByIdCommand request, CancellationToken cancellationToken)
        {
            var check = _employeeRepository.CheckExistsById(request.Id, cancellationToken);
            if (check == null)
            {
                return new ServiceResponse(false, "User not found");
            }

            var successfulResponse = await _employeeRepository.DeleteEmployee(check.Result, cancellationToken);

            return successfulResponse;
        }
    }
}
