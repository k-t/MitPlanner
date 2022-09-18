using MitPlanner.Data.Converters;
using MitPlanner.Data.Model;
using System.Text.Json.Nodes;

namespace MitPlanner.Data
{
    public class JobRepository
    {
        private readonly Dictionary<string, Job> data;

        public JobRepository()
        {
            data = LoadData();
        }

        public Job? GetJob(string? id)
        {
            if (string.IsNullOrEmpty(id))
                return null;

            return data.TryGetValue(id, out var result) ? result : null;
        }

        private static Dictionary<string, Job> LoadData()
        {
            var json = File.ReadAllText("./Data/Files/jobs.json");
            var jsonArray = JsonNode.Parse(json)?.AsArray();

            if (jsonArray == null)
                throw new FormatException("Invalid jobs.json format");

            var data = new Dictionary<string, Job>(jsonArray.Count);

            foreach (var node in jsonArray)
            {
                var job = JobConverter.FromJson(node);
                if (job != null)
                    data[job.Id] = job;
            }

            return data;
        }
    }
}
