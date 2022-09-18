using MitPlanner.Data.Model;
using System.Text.Json.Nodes;

namespace MitPlanner.Data.Converters
{
    public class JobConverter
    {
        public static Job? FromJson(string json)
        {
            var node = JsonNode.Parse(json);
            return FromJson(node);
        }

        public static Job? FromJson(JsonNode? jsonNode)
        {
            if (jsonNode == null)
                return null;

            var id = jsonNode["id"]?.ToString();
            var name = jsonNode["name"]?.ToString();
            var role = jsonNode["role"]?.ToString();

            if (string.IsNullOrEmpty(id))
                throw new FormatException("Missing job id");

            if (string.IsNullOrEmpty(name))
                throw new FormatException($"Missing job name for {id}");

            if (string.IsNullOrEmpty(role))
                throw new FormatException($"Missing job role for {id}");

            return new Job(id, name, role);
        }
    }
}
