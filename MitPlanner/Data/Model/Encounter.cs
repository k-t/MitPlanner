namespace MitPlanner.Data.Model
{
    public class Encounter
    {
        public Encounter(string name, IReadOnlyList<TimelineNode> timeline)
        {
            Name = name;
            Timeline = timeline;
        }

        public string Name { get; }

        public IReadOnlyList<TimelineNode> Timeline { get; }
    }
}
