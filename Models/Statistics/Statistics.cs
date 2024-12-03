namespace SymphonyEquilibriAPI.Models.Statistics
{
    using SymphonyEquilibriAPI.Models.Employee;
    public class Statistics
    {
        public int Id { get; set; }

        public int Fitness { get; set; }

        public int Health { get; set; }

        public int BrainCapacity { get; set; } = 100;

        public bool Injured { get; set; } = false;

        public bool Fatigued { get; set; } = false;

        public Employee? Employee { get; set; }

        public int EmployeeId { get; set; }
    }
}
