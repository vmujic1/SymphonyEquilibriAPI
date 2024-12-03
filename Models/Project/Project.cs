namespace SymphonyEquilibriAPI.Models.Project
{
    using SymphonyEquilibriAPI.Models.Employee;
    public class Project
    {
        public int ProjectId { get; set; }

        public string Name { get; set; } = string.Empty;

        public ProjectIndustry Industry { get; set; }

        public List<Employee>? Employees { get; set; }
    }
}
