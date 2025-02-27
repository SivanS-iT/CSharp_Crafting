using Application.Abstractions.Messaging;
using Domain.Features.Employee;

namespace Application.Employees.GetEmployees
{
    /// <summary>
    /// Query for getting Employee List
    /// </summary>
    public sealed record GetEmployeeListQuery : IQuery<List<Employee>>;
}
