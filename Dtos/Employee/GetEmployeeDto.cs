using SymphonyEquilibriAPI.Dtos.Statistics;

namespace SymphonyEquilibriAPI.Dtos.Employee
{
    using SymphonyEquilibriAPI.Models.Employee;
    public class GetEmployeeDto
    {
        public GetEmployeeDto(Employee employee)
        {
            Id = employee.Id;
            Name = employee.Name;
            DepartmentName = employee.DepartmentName;
            Projects = employee.Projects.Select(p => new GeEmployeeProjectDto(p)).ToList();
            Statistics = employee.Statistics == null ? null : new GetStatisticsDto(employee.Statistics);
        }

        public int Id { get; set; } = 0;

        public string Name { get; set; } = string.Empty;

        public string DepartmentName { get; set; } = string.Empty;

        public List<GeEmployeeProjectDto>? Projects { get; set; }

        public GetStatisticsDto? Statistics { get; set; }
    }
}
