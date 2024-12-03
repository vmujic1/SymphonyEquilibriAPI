namespace SymphonyEquilibriAPI.Models.Statistics
{
    public class GetStatisticsDto
    {
        public GetStatisticsDto(Statistics statistics)
        {
            Id = statistics.Id;
            Fitness = statistics.Fitness;
            Health = statistics.Health;
            BrainCapacity = statistics.BrainCapacity;
            Injured = statistics.Injured;
            Fatigued = statistics.Fatigued;
        }

        public int Id { get; set; }
        public int Fitness { get; set; }
        public int Health { get; set; }
        public int BrainCapacity { get; set; }
        public bool Injured { get; set; }
        public bool Fatigued { get; set; }
    }
}
