namespace MitPlanner.Data.Model
{
    public class TimelineNode
    {
        public int Id { get; set; }

        public string? Name { get; set; }

        public TimeSpan? Start { get; set; }

        public TimeSpan? End { get; set; }

        public string? DamageType { get; set; }

        public string? Actor { get; set; }

        public TimelineNodeType Type { get; set; }
    }
}
