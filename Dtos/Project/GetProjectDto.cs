using SymphonyEquilibriAPI.Models.Employee;

namespace SymphonyEquilibriAPI.Dtos.Project
{
    using SymphonyEquilibriAPI.Models.Project;

    public class GetProjectDto
    {
        public GetProjectDto(Project project)
        {
            Id = project.ProjectId;
            Name = project.Name;
            Industry = project.Industry;
            Employees = project.Employees.Select(e => new GetProjectEmployeesDto(e)).ToList();
        }

        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public ProjectIndustry Industry { get; set; }
        public List<GetProjectEmployeesDto>? Employees { get; set; }
    }
}
