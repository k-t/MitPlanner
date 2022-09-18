using MitPlanner.Data.Model;
using System.ComponentModel;

namespace MitPlanner.Model
{
    public class TimetableItemModel
    {
        public TimetableItemModel(
            int id,
            TimelineNode timelineNode,
            IReadOnlyDictionary<int, List<ActionModel>> actions)
        {
            Id = id;
            Timeline = timelineNode;
            Actions = actions;
        }

        public event EventHandler? ActionListChanged;

        public int Id { get; }

        public TimelineNode Timeline { get; }

        public IReadOnlyDictionary<int, List<ActionModel>> Actions { get; }

        public bool AddAction(int actorId, ActionModel action)
        {
            if (!Actions.TryGetValue(actorId, out var actions))
                return false;

            if (actions.Count >= 5)
                return false;

            if (actions.Any(a => a.Name == action.Name))
                return false;

            actions.Add(action);

            OnActionListChanged();

            return true;
        }

        public bool RemoveAction(int actorId, ActionModel action)
        {
            if (!Actions.TryGetValue(actorId, out var actions))
                return false;

            if (actions.RemoveAll(a => a.Name == action.Name) > 0)
            {
                OnActionListChanged();
                return true;
            }

            return false;
        }

        public IReadOnlyList<ActionModel>? GetActions(int actorId)
        {
            return Actions.TryGetValue(actorId, out var actions) ? actions : null;
        }

        private void OnActionListChanged()
        {
            ActionListChanged?.Invoke(this, EventArgs.Empty);
        }
    }
}
