using Application.Abstractions.Messaging;
using Domain.Features.Employee;

namespace Application.Employees.GetEmployee
{
    /// <summary>
    /// Query for getting Employee with unique identifier
    /// </summary>
    /// <param name="Id"></param>
    public sealed record GetEmployeeByIdQuery(int Id) : IQuery<Employee>;
}
