using Application.Abstractions.Messaging;
using Domain.Abstractions;
using Domain.DTOs;
using Domain.Features.Employee;
using MediatR;

namespace Application.Commands.EmployeeCommands
{
    /// <summary>
    /// Command for Creating employees
    /// </summary>
    /// <param name="Employee"></param>
    public sealed record CreateEmployeeCommand(CreateEmployeeRequest Employee) : ICommand;
}
