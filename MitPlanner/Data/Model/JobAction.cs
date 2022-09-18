namespace MitPlanner.Data.Model
{
    public class JobAction
    {
        public JobAction(
            string name,
            string title,
            TimeSpan recast,
            TimeSpan duration,
            string? role = null,
            string? job = null)
        {
            Name = name;
            Title = title;
            Recast = recast;
            Role = role;
            Job = job;
            Duration = duration;
        }

        public string Name { get; }

        public string Title { get; }

        public TimeSpan Recast { get; }

        public TimeSpan Duration { get; }

        public string? Role { get; }

        public string? Job { get; }
    }
}
