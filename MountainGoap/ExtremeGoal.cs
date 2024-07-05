// <copyright file="ExtremeGoal.cs" company="Chris Muller">
// Copyright (c) Chris Muller. All rights reserved.
// </copyright>

namespace MountainGoap {
    using System.Collections.Generic;

    /// <summary>
    /// Represents a goal requiring an extreme value to be achieved for an agent.
    /// </summary>
    public class ExtremeGoal : BaseGoal {
        /// <summary>
        /// Dictionary of states to be maximized or minimized. A value of true indicates to maximize the goal, a value of false indicates to minimize it.
        /// </summary>
        internal readonly Dictionary<string, bool> DesiredState;

        /// <summary>
        /// Initializes a new instance of the <see cref="ExtremeGoal"/> class.
        /// </summary>
        /// <param name="name">Name of the goal.</param>
        /// <param name="weight">Weight to give the goal.</param>
        /// <param name="desiredState">States to be maximized or minimized.</param>
        public ExtremeGoal(string? name = null, float weight = 1f, Dictionary<string, bool>? desiredState = null)
            : base(name, weight) {
            DesiredState = desiredState ?? new();
        }

        public override bool MeetsGoal(ActionNode actionNode, ActionNode current)
        {
            if (actionNode.Action == null) return false;
            foreach (var kvp in DesiredState) {
                if (!actionNode.State.ContainsKey(kvp.Key)) return false;
                else if (!current.State.ContainsKey(kvp.Key)) return false;
                else if (kvp.Value && actionNode.State[kvp.Key] is object a && current.State[kvp.Key] is object b && Utils.IsLowerThanOrEquals(a, b)) return false;
                else if (!kvp.Value && actionNode.State[kvp.Key] is object a2 && current.State[kvp.Key] is object b2 && Utils.IsHigherThanOrEquals(a2, b2)) return false;
            }

            return true;
        }

        public override float Heuristic(ActionNode actionNode, BaseGoal goal, ActionNode current)
        {
            var cost = 1f;
            foreach (var kvp in DesiredState) {
                var valueDiff = 0f;
                var valueDiffMultiplier = (actionNode?.Action?.StateCostDeltaMultiplier ?? Action.DefaultStateCostDeltaMultiplier).Invoke(actionNode?.Action, kvp.Key);
                if (actionNode.State.ContainsKey(kvp.Key) && actionNode.State[kvp.Key] == null) {
                    cost += float.PositiveInfinity;
                    continue;
                }
                if (actionNode.State.ContainsKey(kvp.Key) && DesiredState.ContainsKey(kvp.Key)) valueDiff = Convert.ToSingle(actionNode.State[kvp.Key]) - Convert.ToSingle(current.State[kvp.Key]);
                if (!actionNode.State.ContainsKey(kvp.Key)) cost += float.PositiveInfinity;
                else if (!current.State.ContainsKey(kvp.Key)) cost += float.PositiveInfinity;
                else if (!kvp.Value && actionNode.State[kvp.Key] is object a && current.State[kvp.Key] is object b && Utils.IsLowerThanOrEquals(a, b)) cost += valueDiff * valueDiffMultiplier;
                else if (kvp.Value && actionNode.State[kvp.Key] is object a2 && current.State[kvp.Key] is object b2 && Utils.IsHigherThanOrEquals(a2, b2)) cost -= valueDiff * valueDiffMultiplier;
            }

            return cost;
        }
    }
}
