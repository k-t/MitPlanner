using MitPlanner.Data.Model;

namespace MitPlanner.Model
{
    public class ActionTimelineItemModel
    {
        public int Id { get; set; }

        public TimelineNode Timeline { get; set; }

        public List<ActorActionListModel> Actions { get; set; }
    }
}
