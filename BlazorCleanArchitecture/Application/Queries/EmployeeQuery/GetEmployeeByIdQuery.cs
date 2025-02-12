using Application.Abstractions.Messaging;
using Domain.Features.Employee;

namespace Application.Queries.EmployeeQuery
{
    /// <summary>
    /// Query for getting Employee with unique identifier
    /// </summary>
    /// <param name="Id"></param>
    public record GetEmployeeByIdQuery(int Id) : IQuery<Employee>;
}
