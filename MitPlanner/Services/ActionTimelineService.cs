using AntDesign;
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

        private readonly Dictionary<string, TimetableModel> data;

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

        public TimetableModel? GetEncounterTimeline(string encounterId)
        {
            return data.TryGetValue(encounterId, out var result)
                ? result
                : null;
        }

        public async Task<bool> AddAction(
            TimetableModel timetable,
            int timelineNodeId,
            int actorId,
            ActionModel action)
        {
            if (timetable.AddAction(timelineNodeId, actorId, action))
            {
                var entity = new TimelineAction
                {
                    ActionName = action.Name,
                    ActionTimelineId = timetable.Id,
                    TimelineNodeId = timelineNodeId,
                    TimelineActorId = actorId
                };

                await timelineRepository.AddTimelineActorActionAsync(entity);

                return true;
            }

            return false;
        }

        public async Task<bool> RemoveAction(
            TimetableModel timetable,
            int timelineNodeId,
            int actorId,
            ActionModel action)
        {
            if (timetable.RemoveAction(timelineNodeId, actorId, action))
            {
                var entity = new TimelineAction
                {
                    ActionName = action.Name,
                    ActionTimelineId = timetable.Id,
                    TimelineNodeId = timelineNodeId,
                    TimelineActorId = actorId
                };

                await timelineRepository.RemoveTimelineActorActionAsync(entity);

                return true;
            }

            return false;
        }

        private Dictionary<string, TimetableModel> LoadData()
        {
            // TODO: sort out mess below

            var timelines = timelineRepository.GetTimelines();

            var data = new Dictionary<string, TimetableModel>(timelines.Count);

            foreach (var tl in timelines)
            {
                var encounter = encounterRepository.GetEncounter(tl.EncounterId);
                if (encounter == null)
                    continue;

                var actors = tl.TimelineActors.Select(MapToModel).ToArray();
                var items = new List<TimetableItemModel>(encounter.Timeline.Count);

                foreach (var item in encounter.Timeline)
                {
                    var actions = new Dictionary<int, List<ActionModel>>(tl.TimelineActors.Count);

                    foreach (var actor in tl.TimelineActors)
                    {
                        actions[actor.Id] = actor.TimelineActions
                            .Where(a => a.TimelineNodeId == item.Id)
                            .Select(MapToModel)
                            .ToList();
                    }

                    items.Add(new TimetableItemModel(item.Id, item, actions));
                }

                data[tl.EncounterId] = new TimetableModel(tl.Id, tl.EncounterId, actors, items);
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
