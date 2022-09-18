namespace MitPlanner.Data.Model
{
    public class ActionTimeline
    {
        public ActionTimeline(int id, string encounterId)
        {
            Id = id;
            EncounterId = encounterId;
            TimelineActions = new List<TimelineAction>();
            TimelineActors = new List<TimelineActor>();
        }

        public int Id { get; set; }

        public string EncounterId { get; set; }

        public List<TimelineAction> TimelineActions { get; set; }

        public List<TimelineActor> TimelineActors { get; set; }
    }
}
