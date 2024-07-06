﻿using Application.Commands;
using Application.Queries.EmployeeQuery;
using Domain.Entities;
using Infrastructure.Handlers.EmployeeHandler;
using MediatR;
using Microsoft.AspNetCore.Mvc;

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
        public async Task<IActionResult> Get() => Ok( await _mediator.Send(new GetEmployeeListQuery()));

        /// <summary>
        /// Get by ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id) => Ok( await _mediator.Send(new GetEmployeeByIdQuery { Id = id }));

        /// <summary>
        /// Add employee
        /// </summary>
        /// <param name="employeeDto"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Add([FromBody] Employee employeeDto)
        {
            var result = await _mediator.Send(new CreateEmployeeCommand { Employee = employeeDto });
           return Ok(result);
        }

        /// <summary>
        /// Update employee
        /// </summary>
        /// <param name="employeeDto"></param>
        /// <returns></returns>
        [HttpPut]
        public async Task<IActionResult> Update([FromBody] Employee employeeDto)
        {
            var result = await _mediator.Send(new UpdateEmployeeCommand { Employee = employeeDto });
            return Ok(result);
        }

        /// <summary>
        /// Delete emplyee
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _mediator.Send(new DeleteEmployeeByIdCommand { Id = id });
            return Ok(result);
        }
    }
}
