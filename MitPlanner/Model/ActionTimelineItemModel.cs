using MitPlanner.Data.Model;

namespace MitPlanner.Model
{
    public class ActionTimelineItemModel
    {
        public ActionTimelineItemModel(int id, TimelineNode timelineNode)
        {
            Id = id;
            Timeline = timelineNode;
            Actions = new List<ActorActionListModel>();
        }

        public int Id { get; }

        public TimelineNode Timeline { get; }

        public List<ActorActionListModel> Actions { get; }
    }
}
