namespace MitPlanner.Data.Model
{
    public class TimelineActor
    {
        public TimelineActor(int id, int actionTimelineId, string jobId)
        {
            Id = id;
            ActionTimelineId = actionTimelineId;
            JobId = jobId;
            TimelineActions = new List<TimelineAction>();
        }

        public int Id { get; set; }

        public int ActionTimelineId { get; set; }

        public string JobId { get; set; }

        public List<TimelineAction> TimelineActions { get; set; } 
    }
}
