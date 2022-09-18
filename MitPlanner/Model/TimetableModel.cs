using AntDesign;
using System.ComponentModel;

namespace MitPlanner.Model
{
    public class TimetableModel
    {
        private readonly object stateLock = new();

        public TimetableModel(
            int id,
            string encounterId,
            IReadOnlyList<ActorModel> actors,
            IReadOnlyList<TimetableItemModel> items)
        {
            Id = id;
            EncounterId = encounterId;
            Actors = actors;
            Items = items;
        }

        public int Id { get; }

        public string EncounterId { get; }

        public IReadOnlyList<ActorModel> Actors { get; }

        public IReadOnlyList<TimetableItemModel> Items { get; }

        public bool AddAction(int timelineNodeId, int actorId, ActionModel action)
        {
            lock (stateLock)
            {
                var item = GetItem(timelineNodeId);
                if (item == null)
                    return false;

                return item.AddAction(actorId, action);
            }
        }

        public bool RemoveAction(int timelineNodeId, int actorId, ActionModel action)
        {
            lock (stateLock)
            {
                var item = GetItem(timelineNodeId);
                if (item == null)
                    return false;

                return item.RemoveAction(actorId, action);
            }
        }

        private TimetableItemModel? GetItem(int timelineNodeId)
        {
            return Items.FirstOrDefault(t => t.Timeline.Id == timelineNodeId);
        }
    }
}
