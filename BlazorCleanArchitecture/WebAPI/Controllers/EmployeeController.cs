using Application.Contracts;
using Domain.Entities;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployee _employee;

        public EmployeeController(IEmployee empoyee) 
        {
            this._employee = empoyee;
        }

        /// <summary>
        /// Get ALL
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var data = await _employee.GetAsync();
            return Ok(data);
        }

        /// <summary>
        /// Get by ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var data = await _employee.GetByIdAsync(id);
            return Ok(data);
        }

        /// <summary>
        /// Add employee
        /// </summary>
        /// <param name="employeeDto"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Add([FromBody] Employee employeeDto)
        {
            var result = await _employee.AddAsync(employeeDto);
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
            var result = await _employee.UpdateAsync(employeeDto);
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
            var result = await _employee.DeleteAsync(id);
            return Ok(result);
        }
    }
}
