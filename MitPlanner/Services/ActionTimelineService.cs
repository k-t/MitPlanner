using MitPlanner.Data;
using MitPlanner.Data.Model;
using MitPlanner.Model;

namespace MitPlanner.Services
{
    public class ActionTimelineService
    {
        private readonly ActionTimelineRepository timelineRepository;
        private readonly EncounterRepository encounterRepository;
        private readonly JobRepository jobRepository;
        private readonly JobActionService actionService;

        private readonly Dictionary<string, ActionTimelineModel> data;

        public ActionTimelineService(
            ActionTimelineRepository timelineRepository,
            EncounterRepository encounterRepository,
            JobRepository jobRepository,
            JobActionService actionService)
        {
            this.timelineRepository = timelineRepository;
            this.encounterRepository = encounterRepository;
            this.jobRepository = jobRepository;
            this.actionService = actionService;
            data = LoadData();
        }

        public ActionTimelineModel? GetEncounterTimeline(string encounterId)
        {
            return data.TryGetValue(encounterId, out var result)
                ? result
                : null;
        }

        public async Task<bool> AddAction(ActionTimelineModel timeline, int timelineItemId, int actorId, string actionName)
        {
            var timelineItem = timeline.Items.FirstOrDefault(t => t.Id == timelineItemId);
            if (timelineItem == null)
                return false;

            var actor = timelineItem.Actions.FirstOrDefault(a => a.ActorId == actorId);
            if (actor == null)
                return false;

            if (actor.Actions.Count >= 5)
                return false;

            if (actor.Actions.Any(a => a.Name == actionName))
                return false;

            var action = actionService.GetAction(actionName);
            if (action == null)
                return false;

            actor.Actions.Add(action);

            var entity = new TimelineAction
            {
                ActionName = actionName,
                ActionTimelineId = timeline.Id,
                TimelineNodeId = timelineItemId,
                TimelineActorId = actorId
            };

            await timelineRepository.AddTimelineActorActionAsync(entity);

            return true;
        }

        public async Task<bool> RemoveAction(ActionTimelineModel timeline, int timelineItemId, int actorId, string actionName)
        {
            var timelineItem = timeline.Items.FirstOrDefault(t => t.Id == timelineItemId);
            if (timelineItem == null)
                return false;

            var actor = timelineItem.Actions.FirstOrDefault(a => a.ActorId == actorId);
            if (actor == null)
                return false;

            if (actor.Actions.RemoveAll(a => a.Name == actionName) > 0)
            {
                var entity = new TimelineAction
                {
                    ActionName = actionName,
                    ActionTimelineId = timeline.Id,
                    TimelineNodeId = timelineItemId,
                    TimelineActorId = actorId
                };

                await timelineRepository.RemoveTimelineActorActionAsync(entity);

                return true;
            }

            return false;
        }

        private Dictionary<string, ActionTimelineModel> LoadData()
        {
            // TODO: sort out mess below

            var timelines = timelineRepository.GetTimelines();

            var data = new Dictionary<string, ActionTimelineModel>(timelines.Count);

            foreach (var tl in timelines)
            {
                var encounter = encounterRepository.GetEncounter(tl.EncounterId);
                if (encounter == null)
                    continue;

                var timelineModel = new ActionTimelineModel(tl.Id, tl.EncounterId);
                timelineModel.Actors.AddRange(tl.TimelineActors.Select(MapToModel));

                foreach (var item in encounter.Timeline)
                {
                    var timelineItemModel = new ActionTimelineItemModel(item.Id, item);

                    foreach (var actor in tl.TimelineActors)
                    {
                        var actions = actor.TimelineActions.Where(a => a.TimelineNodeId == item.Id);
                        var actorModel = new ActorActionListModel(actor.Id, actor.JobId);
                        actorModel.Actions.AddRange(actions.Select(MapToModel));
                        timelineItemModel.Actions.Add(actorModel);
                    }

                    timelineModel.Items.Add(timelineItemModel);
                }

                data[tl.EncounterId] = timelineModel;
            }

            return data;
        }

        private ActorModel MapToModel(TimelineActor actor)
        {
            var job = jobRepository.GetJob(actor.JobId);
            if (job == null)
                throw new ArgumentException($"Unknown job: {actor.JobId}");

            return new ActorModel(actor.Id, actor.JobId, job.Name);
        }

        private ActionModel MapToModel(TimelineAction timelineAction)
        {
            var action = actionService.GetAction(timelineAction.ActionName);
            if (action == null)
                throw new ArgumentException($"Unknown action: {timelineAction.ActionName}");

            return action;
        }
    }
}
