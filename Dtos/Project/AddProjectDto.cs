using SymphonyEquilibriAPI.Models.Project;

namespace SymphonyEquilibriAPI.Dtos.Project
{
    public class AddProjectDto
    {
        public string Name { get; set; } = string.Empty;

        public ProjectIndustry Industry { get; set; }
    }
}
