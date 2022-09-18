namespace MitPlanner.Data.Model
{
    public class Job
    {
        public Job(string id, string name, string role)
        {
            Id = id;
            Name = name;
            Role = role;
        }

        public string Id { get; }

        public string Name { get; }

        public string Role { get; }
    }
}
