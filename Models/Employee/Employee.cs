namespace SymphonyEquilibriAPI.Models.Employee
{
    using SymphonyEquilibriAPI.Models.Project;
    using SymphonyEquilibriAPI.Models.Statistics;

    public class Employee
    {
        public int Id { get; set; } = 0;

        public string Name { get; set; } = string.Empty;

        public string DepartmentName { get; set; } = string.Empty;

        public List<Project>? Projects { get; set; }

        public Statistics? Statistics { get; set; }

    }
}
