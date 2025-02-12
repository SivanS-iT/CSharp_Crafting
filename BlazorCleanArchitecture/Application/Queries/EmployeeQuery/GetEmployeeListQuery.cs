using Domain.Features.Employee;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Abstractions.Messaging;

namespace Application.Queries.EmployeeQuery
{
    /// <summary>
    /// Query for getting Employee List
    /// </summary>
    public record GetEmployeeListQuery : IQuery<List<Employee>>;
}
