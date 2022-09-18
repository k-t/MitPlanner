namespace MitPlanner.Data.Model
{
    public class ActionTimeline
    {
        public int Id { get; set; }

        public string EncounterId { get; set; }

        public List<TimelineAction> TimelineActions { get; set; }

        public List<TimelineActor> TimelineActors { get; set; }
    }
}
