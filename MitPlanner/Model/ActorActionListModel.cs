namespace MitPlanner.Model
{
    public class ActorActionListModel
    {
        public ActorActionListModel(int actorId, string jobId)
        {
            ActorId = actorId;
            JobId = jobId;
            Actions = new List<ActionModel>();
        }

        public int ActorId { get; }

        public string JobId { get; }

        public List<ActionModel> Actions { get; }
    }
}
