namespace MitPlanner.Model
{
    public class ActionModel
    {
        public ActionModel(string name, string title, TimeSpan recast)
        {
            Name = name;
            Title = title;
            Recast = recast;
        }

        public string Name { get; }

        public string Title { get; }

        public TimeSpan Recast { get; }
    }
}
