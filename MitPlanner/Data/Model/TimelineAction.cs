namespace MitPlanner.Data.Model
{
    public class TimelineAction
    {
        public int Id { get; set; }

        public int ActionTimelineId { get; set; }

        public int TimelineNodeId { get; set; }

        public int TimelineActorId { get; set; }

        public string ActionName { get; set; }
    }
}
