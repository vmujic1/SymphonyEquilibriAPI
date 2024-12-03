
namespace SymphonyEquilibriAPI.Models.Employee
{
    using SymphonyEquilibriAPI.Models.Project;

    public class GeEmployeeProjectDto
    {
        public GeEmployeeProjectDto(Project project)
        {
            ProjectId = project.ProjectId;
            Name = project.Name;
            Industry = project.Industry;

        }
        public int ProjectId { get; set; }

        public string Name { get; set; }

        public ProjectIndustry Industry { get; set; }
    }
}
