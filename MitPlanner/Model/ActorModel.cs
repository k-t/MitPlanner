namespace MitPlanner.Model
{
    public class ActorModel
    {
        public ActorModel(int actorId, string jobId, string jobTitle)
        {
            ActorId = actorId;
            JobId = jobId;
            JobTitle = jobTitle;
        }

        public int ActorId { get; }

        public string JobId { get; }

        public string JobTitle { get; }
    }
}
