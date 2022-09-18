using MitPlanner.Data.Model;
using System.Globalization;
using System.Text.Json.Nodes;

namespace MitPlanner.Data.Converters
{
    public class JobActionConverter
    {
        public static JobAction? FromJson(string json)
        {
            var node = JsonNode.Parse(json);
            return FromJson(node);
        }

        public static JobAction? FromJson(JsonNode? jsonNode)
        {
            if (jsonNode == null)
                return null;

            var name = jsonNode["name"]?.ToString();
            if (string.IsNullOrEmpty(name))
                throw new FormatException($"Missing action name");

            var title = jsonNode["title"]?.ToString();
            if (string.IsNullOrEmpty(title))
                throw new FormatException($"Missing action title for action '{name}'");

            var recast = ParseTimespan(jsonNode["recast"], name);
            var duration = ParseTimespan(jsonNode["duration"], name);
            var role = jsonNode["role"]?.ToString();
            var job = jsonNode["job"]?.ToString();

            return new JobAction(name, title, recast, duration, role: role, job: job);
        }

        private static TimeSpan ParseTimespan(JsonNode? jsonNode, string actionName)
        {
            var stringValue = jsonNode?.ToString();

            if (double.TryParse(stringValue, NumberStyles.Any, CultureInfo.InvariantCulture, out var value))
                return TimeSpan.FromSeconds(value);

            throw new FormatException($"Invalid time value '{stringValue ?? "null"}' for action '{actionName}'");
        }
    }
}
