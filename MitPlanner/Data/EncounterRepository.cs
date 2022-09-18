using MitPlanner.Data.Converters;
using MitPlanner.Data.Model;

namespace MitPlanner.Data
{
    public class EncounterRepository
    {
        private readonly Dictionary<string, Encounter> data;

        public EncounterRepository()
        {
            data = LoadData();
        }

        public Encounter? GetEncounter(string name)
        {
            return data.TryGetValue(name, out var result)
                ? result
                : null;
        }

        private static Dictionary<string, Encounter> LoadData()
        {
            var data = new Dictionary<string, Encounter>();

            foreach (var fileName in Directory.EnumerateFiles("./Data/Files/Encounters", "*.json"))
            {
                var json = File.ReadAllText(fileName);
                var encounter = EncounterConverter.FromJson(json);
                if (encounter != null)
                    data[encounter.Name] = encounter;
            }

            return data;
        }
    }
}
