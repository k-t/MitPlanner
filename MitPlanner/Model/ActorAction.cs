namespace MitPlanner.Model
{
    public class ActorAction
    {
        public ActorAction(int actorId, string actionName)
        {
            ActorId = actorId;
            ActionName = actionName;
        }

        public int ActorId { get; }

        public string ActionName { get; }
    }
}
