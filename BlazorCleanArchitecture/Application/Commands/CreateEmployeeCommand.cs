using Application.DTOs;
using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Commands
{
    public class CreateEmployeeCommand : IRequest<ServiceResponse>
    {
        public Employee? Employee { get; set; }
    }
}
