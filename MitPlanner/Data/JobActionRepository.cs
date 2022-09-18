using MitPlanner.Data.Converters;
using MitPlanner.Data.Model;
using System.Text.Json.Nodes;

namespace MitPlanner.Data
{
    public class JobActionRepository
    {
        private readonly Dictionary<string, JobAction> data;

        public JobActionRepository()
        {
            data = LoadData();
        }

        public JobAction? GetAction(string actionName)
        {
            return data.TryGetValue(actionName, out var result) ? result : null;
        }

        public IReadOnlyCollection<JobAction> GetActions(string? jobId = null, string? role = null)
        {
            var result = Enumerable.Empty<JobAction>();

            if (!string.IsNullOrEmpty(jobId))
            {
                result = result.Concat(data.Values.Where(a => string.Equals(a.Job, jobId, StringComparison.InvariantCulture)));
            }

            if (!string.IsNullOrEmpty(role))
            {
                result = result.Concat(data.Values.Where(a => string.Equals(a.Role, role, StringComparison.InvariantCulture)));
            }

            return result.OrderBy(a => a.Title).ToArray();
        }

        private static Dictionary<string, JobAction> LoadData()
        {
            var json = File.ReadAllText("./Data/Files/actions.json");
            var jsonArray = JsonNode.Parse(json)?.AsArray();

            if (jsonArray == null)
                throw new FormatException("Invalid actions.json format");

            var data = new Dictionary<string, JobAction>(jsonArray.Count);

            foreach (var node in jsonArray)
            {
                var action = JobActionConverter.FromJson(node);
                if (action != null)
                    data[action.Name] = action;
            }

            return data;
        }
    }
}
