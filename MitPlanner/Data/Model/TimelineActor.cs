namespace MitPlanner.Data.Model
{
    public class TimelineActor
    {
        public int Id { get; set; }

        public int ActionTimelineId { get; set; }

        public string JobId { get; set; }

        public List<TimelineAction> TimelineActions { get; set; } 
    }
}
