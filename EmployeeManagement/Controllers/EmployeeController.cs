using EmployeeManagement.Data;
using EmployeeManagement.Models;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace EmployeeManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeRepository _employeeRepository;

        public EmployeeController(IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }

        [HttpGet("{id}"), ActionName("GetByIdAsync")]
        public async Task<IActionResult> GetByIdAsync(int id)
        {
            try
            {
                if (id <= 0)
                {
                    return BadRequest(new { Message = $"Provide valid employee id" });
                }
                var employee = await _employeeRepository.GetByIdAsync(id);
                if (employee == null)
                    return NotFound(new { Message = $"Employee with id : {id} not found" });
                return Ok(employee);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            try
            {
                return Ok(await _employeeRepository.GetAllAsync());
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }


        [HttpPost]
        public async Task<IActionResult> AddAsync(Employee employee)
        {
            try
            {
                if ((employee == null) || (!ModelState.IsValid))
                {
                    return BadRequest();
                }
                await _employeeRepository.AddAsync(employee);
                return CreatedAtAction(nameof(GetByIdAsync), new { id = employee.EmployeeId }, employee);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }



        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            try
            {
                if (id <= 0)
                {
                    return BadRequest(new { Message = $"Provide valid employee id" });
                }
                var employee = await _employeeRepository.GetByIdAsync(id);
                if (employee == null)
                    return NotFound(new { Message = $"Employee with id : {id} not found" });
                await _employeeRepository.DeleteAsync(id);
                return Ok(new { Message = "Employee deleted successfully" });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }

        }
    }
}
