namespace MitPlanner.Model
{
    public class ActionTimelineModel
    {
        public ActionTimelineModel(string encounterId) : this(0, encounterId)
        {
        }

        public ActionTimelineModel(int id, string encounterId)
        {
            Id = id;
            EncounterId = encounterId;
            Actors = new List<ActorModel>();
            Items = new List<ActionTimelineItemModel>();
        }

        public int Id { get; }

        public string EncounterId { get; }

        public List<ActorModel> Actors { get; }

        public List<ActionTimelineItemModel> Items { get; }
    }
}
