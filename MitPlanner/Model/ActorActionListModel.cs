namespace MitPlanner.Model
{
    public class ActorActionListModel
    {
        public int ActorId { get; set; }

        public string JobId { get; set; }

        public List<ActionModel> Actions { get; set; }
    }
}
