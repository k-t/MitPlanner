namespace MitPlanner.Model
{
    public static class Roles
    {
        public const string Tank = "tank";

        public const string Healer = "heal";

        public const string Melee = "melee";

        public const string Caster = "caster";

        public const string Range = "range";

        public static bool IsDps(string role)
        {
            return
                string.Equals(role, Melee, StringComparison.InvariantCulture) ||
                string.Equals(role, Caster, StringComparison.InvariantCulture) ||
                string.Equals(role, Range, StringComparison.InvariantCulture);
        }
    }
}
