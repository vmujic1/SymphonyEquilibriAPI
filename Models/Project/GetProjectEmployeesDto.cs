
namespace SymphonyEquilibriAPI.Models.Project
{
    using SymphonyEquilibriAPI.Models.Employee;

    public class GetProjectEmployeesDto
    {
        public GetProjectEmployeesDto(Employee employee)
        {
            Id = employee.Id;
            Name = employee.Name;
            DepartmentName = employee.DepartmentName;
        }
        public int Id { get; set; } = 0;

        public string Name { get; set; } = string.Empty;

        public string DepartmentName { get; set; } = string.Empty;

    }

}
