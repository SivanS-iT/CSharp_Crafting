using Domain.Features.Employee;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Queries.EmployeeQuery
{
    public record GetEmployeeListQuery : IRequest<List<Employee>>;
}
