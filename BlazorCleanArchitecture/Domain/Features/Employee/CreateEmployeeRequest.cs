using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Features.Employee
{
    public class CreateEmployeeRequest
    {
        public string Name { get; set; }
        public string? Address { get; set; }
        
        public string Email { get; set; }
    }
}
