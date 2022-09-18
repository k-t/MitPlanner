using MitPlanner.Data;
using MitPlanner.Data.Model;
using MitPlanner.Model;

namespace MitPlanner.Services
{
    public class JobActionService
    {
        private readonly JobRepository jobs;
        private readonly JobActionRepository jobActions;

        public JobActionService(JobRepository jobs, JobActionRepository jobActions)
        {
            this.jobs = jobs;
            this.jobActions = jobActions;
        }

        public IReadOnlyCollection<ActionModel> GetAllActions(string? jobId)
        {
            var job = jobs.GetJob(jobId);
            if (job == null)
                return Array.Empty<ActionModel>();

            return jobActions
                .GetActions(jobId: jobId, role: job.Role)
                .Select(MapToModel)
                .ToArray();
        }

        public ActionModel? GetAction(string actionName)
        {
            var action = jobActions.GetAction(actionName);
            return action != null ? MapToModel(action) : null;
        }

        private ActionModel MapToModel(JobAction action)
        {
            return new ActionModel(action.Name, action.Title, action.Recast);
        }
    }
}
