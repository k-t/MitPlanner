namespace MitPlanner.Model
{
    public class ActorAction
    {
        public ActorAction(int actorId, ActionModel action)
        {
            ActorId = actorId;
            Action = action;
        }

        public int ActorId { get; }

        public ActionModel Action { get; }
    }
}
