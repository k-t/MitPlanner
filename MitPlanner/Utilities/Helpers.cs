namespace MitPlanner.Utilities
{
    public static class Helpers
    {
        public static string GetActionImageUrl(string actionName)
        {
            return $"img/actions/{actionName.ToLower()}.png";
        }

        public static string GetJobImageUrl(string jobId)
        {
            return $"img/jobs/{jobId.ToLower()}.png";
        }
    }
}
