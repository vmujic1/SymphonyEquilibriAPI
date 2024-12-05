using SymphonyEquilibriAPI.Dtos.Activity;
using SymphonyEquilibriAPI.Dtos.Employee;
using SymphonyEquilibriAPI.Models;

namespace SymphonyEquilibriAPI.Services.EmployeeService
{
    public interface IEmployeeService
    {
        Task<ServiceResponse<List<GetEmployeeDto>>> GetAllEmployees();

        Task<ServiceResponse<GetEmployeeDto>> GetEmployeeById(int id);

        Task<ServiceResponse<List<GetEmployeeDto>>> AddEmployee(AddEmployeeDto newEmployee);

        Task<ServiceResponse<GetEmployeeDto>> UpdateEmployee(UpdateEmployeeDto updatedEmployee);

        Task<ServiceResponse<List<GetEmployeeDto>>> DeleteEmployee(int id);

        Task<ServiceResponse<GetEmployeeDto>> AddEmployeeProject(AddEmployeeProjectDto addEmployeeProject);

        Task<ServiceResponse<GetEmployeeDto>> DeleteEmployeeProject(DeleteEmployeeProjectDto deleteEmployeeProject);

        Task<ServiceResponse<GetEmployeeDto>> PerformActivity(PerformActivityDto performActivityDto);

    }
}
