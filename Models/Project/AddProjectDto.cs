namespace SymphonyEquilibriAPI.Models.Project
{
    public class AddProjectDto
    {
        public string Name { get; set; } = string.Empty;

        public ProjectIndustry Industry { get; set; }
    }
}
