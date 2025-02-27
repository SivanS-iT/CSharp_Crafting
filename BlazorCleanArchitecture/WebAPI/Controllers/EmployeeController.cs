using Application.Employees.CreateEmployee;
using Application.Employees.DeleteEmployee;
using Application.Employees.GetEmployee;
using Application.Employees.GetEmployees;
using Application.Employees.UpdateEmployee;
using Domain.Features.Employee;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using WebApi.Extensions;
using WebApi.Infrastructure;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly IMediator _mediator;

        public EmployeeController(IMediator mediator) 
        {
            this._mediator = mediator;
        }

        /// <summary>
        /// Get ALL
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IResult> Get()
        {
            var result = await _mediator.Send(new GetEmployeeListQuery());
            return result.Match(Results.Ok, CustomResults.Problem);
        } 

        /// <summary>
        /// Get by ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<IResult> GetById(int id)
        {
            var result = await _mediator.Send(new GetEmployeeByIdQuery(id));
            return result.Match(Results.Ok, CustomResults.Problem);
        } 

        /// <summary>
        /// Add employee
        /// </summary>
        /// <param name="employeeDto"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IResult> Add([FromBody] CreateEmployeeRequest employeeDto)
        {
           var result = await _mediator.Send(new CreateEmployeeCommand (employeeDto));
           return result.Match(Results.Ok, CustomResults.Problem);
        }

        /// <summary>
        /// Update employee
        /// </summary>
        /// <param name="employeeDto"></param>
        /// <returns></returns>
        [HttpPut]
        public async Task<IResult> Update([FromBody] Employee employeeDto)
        {
            var result = await _mediator.Send(new UpdateEmployeeCommand(employeeDto));
            return result.Match(Results.NoContent, CustomResults.Problem);
        }

        /// <summary>
        /// Delete emplyee
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public async Task<IResult> Delete(int id)
        {
            var result = await _mediator.Send(new DeleteEmployeeByIdCommand (id));
            return result.Match(Results.NoContent, CustomResults.Problem);
        }
    }
}
