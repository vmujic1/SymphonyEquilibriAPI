using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SymphonyEquilibriAPI.Dtos.Activity;
using SymphonyEquilibriAPI.Dtos.Employee;
using SymphonyEquilibriAPI.Models;
using SymphonyEquilibriAPI.Models.Employee;
using SymphonyEquilibriAPI.Services.EmployeeService;

namespace SymphonyEquilibriAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EmployeeController : Controller
    {
        private readonly IEmployeeService _employeeService;

        public EmployeeController(IEmployeeService employeeService)
        {
            this._employeeService = employeeService;
        }

        [Authorize(Roles = "Admin")]
        [HttpGet("GetAll")]
        public async Task<ActionResult<ServiceResponse<List<GetEmployeeDto>>>> GetAllEmployees()
        {
            return Ok(await _employeeService.GetAllEmployees());
        }

        [Authorize(Roles = "Admin")]
        [HttpGet("{id}")]
        public async Task<ActionResult<ServiceResponse<GetEmployeeDto>>> GetEmployeeById(int id)
        {
            var response = await _employeeService.GetEmployeeById(id);
            if (response.Data == null)
            {
                return NotFound(response);
            }
            return Ok(await _employeeService.GetEmployeeById(id));
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<ActionResult<ServiceResponse<List<Employee>>>> AddEmployee(AddEmployeeDto newEmployee)
        {
            return Ok(await _employeeService.AddEmployee(newEmployee));
        }

        [Authorize(Roles = "Admin,ProjectManager")]
        [HttpPost("Project")]
        public async Task<ActionResult<ServiceResponse<GetEmployeeDto>>> AddEmployeeProject(AddEmployeeProjectDto addEmployeeProject)
        {
            var response = await _employeeService.AddEmployeeProject(addEmployeeProject);
            if(response.Data == null)
            {
                return NotFound(response);
            }
            return Ok(response);
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete]
        public async Task<ActionResult<ServiceResponse<List<GetEmployeeDto>>>> DeleteEmployee(int id)
        {
            var response = await _employeeService.DeleteEmployee(id);
            if (response.Data == null)
            {
                return NotFound(response);
            }
            return Ok(response);

        }

        [Authorize(Roles = "Admin")]
        [HttpPut]
        public async Task<ActionResult<ServiceResponse<GetEmployeeDto>>> UpdateEmployee (UpdateEmployeeDto updateEmployee)
        {
            var response = await _employeeService.UpdateEmployee(updateEmployee);
            if (response.Data == null)
            {
                return NotFound(response);
            }

            return Ok(response);
        }

        [Authorize(Roles = "Admin,ProjectManager")]
        [HttpDelete("Project")]
        public async Task<ActionResult<ServiceResponse<GetEmployeeDto>>> DeleteteEmployeeProject(DeleteEmployeeProjectDto deleteEmployeeProject)
        {
            var response = await _employeeService.DeleteEmployeeProject(deleteEmployeeProject);
            if (response.Data == null)
            {
                return NotFound(response);
            }
            return Ok(response);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost("PerformActivity")]
        public async Task<ActionResult<ServiceResponse<GetEmployeeDto>>> PerformActivity(PerformActivityDto performActivityDto)
        {
            var response = await _employeeService.PerformActivity(performActivityDto);
            if(response.Data == null)
            {
                return NotFound(response);
            }
            return Ok(response);
        }
    }
}
