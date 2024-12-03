using SymphonyEquilibriAPI.Data;
using SymphonyEquilibriAPI.Models;
using SymphonyEquilibriAPI.Models.Activity;
using SymphonyEquilibriAPI.Models.Employee;
using SymphonyEquilibriAPI.Models.Project;
using SymphonyEquilibriAPI.Models.Statistics;



namespace SymphonyEquilibriAPI.Services.EmployeeService
{
    public class EmployeeService : IEmployeeService
    {
        private readonly DataContext _context;

        public EmployeeService(DataContext context)
        {
            this._context = context;
        }
        public async Task<ServiceResponse<List<GetEmployeeDto>>> AddEmployee(AddEmployeeDto newEmployee)
        {
            var serviceResponse = new ServiceResponse<List<GetEmployeeDto>>();
            var employee = new Employee
            {
                Name = newEmployee.Name,
                DepartmentName = newEmployee.DepartmentName,
                Statistics = new Statistics // Add default statistics
                {
                    Fitness = 50,         // Example default value
                    Health = 50,          // Example default value
                    BrainCapacity = 100,   // Example default value
                    Injured = false,
                    Fatigued = false
                }
            };
            _context.Employees.Add(employee);
            await _context.SaveChangesAsync();
            var employees = await _context.Employees
                .Include(e => e.Statistics)
                .Include(e => e.Projects)
                .ToListAsync();

            serviceResponse.Data = employees.Select(e => new GetEmployeeDto(e)).ToList();

            return serviceResponse;
        }

        public async Task<ServiceResponse<GetEmployeeDto>> AddEmployeeProject(AddEmployeeProjectDto addEmployeeProject)
        {
            var response = new ServiceResponse<GetEmployeeDto>();

            try
            {
                var employee = await _context.Employees
                    .Include(e => e.Projects)
                    .Include(e => e.Statistics)
                    .FirstOrDefaultAsync(e => e.Id == addEmployeeProject.EmployeeId);
                if (employee == null)
                {
                    response.Success = false;
                    response.Message = "Employee not found.";
                    return response;
                }
                
                var project = await _context.Projects.FirstOrDefaultAsync(p => p.ProjectId == addEmployeeProject.ProjectId);
                if(project == null)
                {
                    response.Success = false;
                    response.Message = "Project not found.";
                    return response;
                }
                employee.Projects!.Add(project);
                employee.Statistics!.BrainCapacity = Math.Max(0, employee.Statistics.BrainCapacity - 30);
                await _context.SaveChangesAsync();
                response.Data = new GetEmployeeDto(employee);
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = ex.Message;
            }

            return response;
        }

        public async Task<ServiceResponse<GetEmployeeDto>> DeleteEmployeeProject(DeleteEmployeeProjectDto deleteEmployeeProject)
        {
            var serviceResponse = new ServiceResponse<GetEmployeeDto>();
            try
            {
                var employee = await _context.Employees
                                .Include(e => e.Projects)
                                .Include(e => e.Statistics)
                                .FirstOrDefaultAsync(e => e.Id == deleteEmployeeProject.EmployeeId);
                if (employee == null)
                {
                    serviceResponse.Success = false;
                    serviceResponse.Message = "Employee not found.";
                    return serviceResponse;
                }

                var project = await _context.Projects.FirstOrDefaultAsync(p => p.ProjectId == deleteEmployeeProject.ProjectId);
                if (project == null)
                {
                    serviceResponse.Success = false;
                    serviceResponse.Message = "Project not found.";
                    return serviceResponse;
                }
                employee.Projects!.Remove(project);
                employee.Statistics!.BrainCapacity += 30;
                employee.Statistics.BrainCapacity = Math.Min(employee.Statistics.BrainCapacity, 100);

                await _context.SaveChangesAsync();
                serviceResponse.Data = new GetEmployeeDto(employee);

            }
            catch (Exception ex)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = ex.Message;
            }

            return serviceResponse;
        }
        public async Task<ServiceResponse<List<GetEmployeeDto>>> DeleteEmployee(int id)
        {
            var serviceResponse = new ServiceResponse<List<GetEmployeeDto>>();
            try
            {
                var employee = await _context.Employees.Include(e => e.Projects).FirstOrDefaultAsync(e => e.Id == id);
                if(employee == null)
                {
                    serviceResponse.Success = false;
                    serviceResponse.Message = $"Employee with given ID {id} not found!";
                    return serviceResponse;
                }
                _context.Employees.Remove(employee);
                await _context.SaveChangesAsync();

                serviceResponse.Data = await _context.Employees
                    .Include(e => e.Projects)
                    .Select(e => new GetEmployeeDto(e))
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = ex.Message;
            }
            return serviceResponse;
        }


        public async Task<ServiceResponse<List<GetEmployeeDto>>> GetAllEmployees()
        {
            var serviceResponse = new ServiceResponse<List<GetEmployeeDto>>();

            try
            {
                // Fetch employees with their projects
                var dbEmployees = await _context.Employees
                    .Include(e => e.Projects)
                    .Include(e => e.Statistics)// Ensures projects are loaded
                    .ToListAsync();

                // Map each Employee to GetEmployeeDto using the constructor
                serviceResponse.Data = dbEmployees.Select(e => new GetEmployeeDto(e)).ToList();
            }
            catch (Exception ex)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = ex.Message;
            }

            return serviceResponse;
        }


        public async Task<ServiceResponse<GetEmployeeDto>> GetEmployeeById(int id)
        {
            var serviceResponse = new ServiceResponse<GetEmployeeDto>();
            try
            {
                var employee = await _context.Employees
                    .Include(e => e.Projects)
                    .Include(e => e.Statistics)
                    .FirstOrDefaultAsync(e => e.Id == id);

                if(employee == null)
                {
                    serviceResponse.Success = false;
                    serviceResponse.Message = $"Employee with given ID {id} not found!";
                    return serviceResponse;
                }
                serviceResponse.Data = new GetEmployeeDto(employee);                    
            }
            catch (Exception ex)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = ex.Message;

            }
            return serviceResponse;
        }

        public async Task<ServiceResponse<GetEmployeeDto>> UpdateEmployee(UpdateEmployeeDto updatedEmployee)
        {
            var serviceResponse = new ServiceResponse<GetEmployeeDto>();
            try
            {
                var employee = await _context.Employees .Include(e => e.Projects).FirstOrDefaultAsync(e => e.Id == updatedEmployee.Id);
                if (employee == null)
                {
                    serviceResponse.Success = false;
                    serviceResponse.Message = $"Employee with ID {updatedEmployee.Id} not found.";
                    return serviceResponse;
                }
                employee.Name = updatedEmployee.Name;
                employee.DepartmentName = updatedEmployee.DepartmentName;

                await _context.SaveChangesAsync();
                serviceResponse.Data = new GetEmployeeDto(employee);

            }
            catch (Exception ex)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = ex.Message;
            }
            return serviceResponse;
        }

        public async Task<ServiceResponse<GetEmployeeDto>> PerformActivity(PerformActivityDto performActivityDto)
        {
            var serviceResponse = new ServiceResponse<GetEmployeeDto>();
            try
            {
                var employee = await _context.Employees
                                    .Include(e => e.Projects)
                                    .Include(e => e.Statistics)
                                    .FirstOrDefaultAsync(e => e.Id == performActivityDto.EmployeeId);

                if (employee == null)
                {
                    serviceResponse.Success = false;
                    serviceResponse.Message = $"Employee with Id {performActivityDto.EmployeeId} not found.";
                    return serviceResponse;
                }

                if (performActivityDto.Activity != Activity.Running && performActivityDto.Activity != Activity.Swimming && performActivityDto.Activity != Activity.WeightLifting && performActivityDto.Activity != Activity.PlayingGames)
                {
                    serviceResponse.Success = false;
                    serviceResponse.Message = $"Activity with Name {performActivityDto.Activity} not found.";
                    return serviceResponse;
                }
                if(performActivityDto.Activity == Activity.Running)
                {
                    employee.Statistics!.Fitness += 5;
                    employee.Statistics!.Health += 3;
                }

                employee.Statistics!.Fitness = Math.Clamp(employee.Statistics.Fitness, 0, 100);
                employee.Statistics!.Health = Math.Clamp(employee.Statistics.Health, 0, 100);

                await _context.SaveChangesAsync();

                serviceResponse.Data = new GetEmployeeDto(employee);

            }
            catch (Exception ex)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = ex.Message;
            }
            return serviceResponse;
        }
    }
}
