using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Primitives;

namespace Domain.Features.Employee
{
    public sealed class Employee : Entity
    {
        public string Name { get; set; }
        public string? Address { get; set; }
        public string Email { get; set; }
    }
}
