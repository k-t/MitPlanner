using MitPlanner.Data.Model;
using System.Globalization;
using System.Text.Json.Nodes;

namespace MitPlanner.Data.Converters
{
    public class EncounterConverter
    {
        public static Encounter? FromJson(string json)
        {
            var node = JsonNode.Parse(json);
            return FromJson(node);
        }

        public static Encounter? FromJson(JsonNode? jsonNode)
        {
            if (jsonNode == null)
                return null;

            var encounterName = jsonNode["encounterName"]?.ToString();

            if (string.IsNullOrEmpty(encounterName))
                throw new FormatException("Missing encounter name");

            var timelineArray = jsonNode["timeline"]?.AsArray();
            if (timelineArray == null)
                throw new FormatException("Missing timeline data");

            var timeline = new List<TimelineNode>(timelineArray.Count);
            var timelineItemIndex = 1;

            foreach (var timelineNode in timelineArray)
            {
                var timelineItem = ParseTimelineNode(timelineNode);
                if (timelineItem != null)
                {
                    timelineItem.Id = timelineItemIndex++;
                    timeline.Add(timelineItem);
                }
            }

            return new Encounter(encounterName, timeline);
        }

        private static TimelineNode? ParseTimelineNode(JsonNode? jsonNode)
        {
            if (jsonNode == null)
                return null;

            var type = jsonNode["type"]?.ToString() switch
            {
                "raidwide" => TimelineNodeType.Raidwide,
                "tb" => TimelineNodeType.Tankbuster,
                "enrage" => TimelineNodeType.Enrage,
                _ => TimelineNodeType.Default
            };

            return new TimelineNode
            {
                Start = ParseTimespan(jsonNode["start"]),
                End = ParseTimespan(jsonNode["end"]),
                Name = jsonNode["name"]?.ToString(),
                Actor = jsonNode["actor"]?.ToString(),
                DamageType = jsonNode["damageType"]?.ToString(),
                Type = type
            };
        }

        private static TimeSpan? ParseTimespan(JsonNode? jsonNode)
        {
            if (jsonNode == null)
                return null;

            var stringValue = jsonNode.ToString();

            return double.TryParse(stringValue, NumberStyles.Any, CultureInfo.InvariantCulture, out var value)
                ? TimeSpan.FromSeconds(value)
                : null;
        }
    }
}
