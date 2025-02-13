using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Abstractions;

namespace Domain.Features.Employee
{
    public class Employee : Entity
    {
        public string Name { get; set; }
        public string? Address { get; set; }

        public string Email { get; set; }
    }
}
