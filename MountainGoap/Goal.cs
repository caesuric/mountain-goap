// <copyright file="Goal.cs" company="Chris Muller">
// Copyright (c) Chris Muller. All rights reserved.
// </copyright>

namespace MountainGoap {
    using System.Collections.Generic;

    /// <summary>
    /// Represents a goal to be achieved for an agent.
    /// </summary>
    public class Goal : BaseGoal {
        /// <summary>
        /// Desired world state to be achieved.
        /// </summary>
        internal readonly Dictionary<string, object?> DesiredState;

        /// <summary>
        /// Initializes a new instance of the <see cref="Goal"/> class.
        /// </summary>
        /// <param name="name">Name of the goal.</param>
        /// <param name="weight">Weight to give the goal.</param>
        /// <param name="desiredState">Desired end state of the goal.</param>
        public Goal(string? name = null, float weight = 1f, Dictionary<string, object?>? desiredState = null)
            : base(name, weight) {
            DesiredState = desiredState ?? new();
        }

        public override bool MeetsGoal(ActionNode actionNode, ActionNode current)
        {
            foreach (var kvp in DesiredState) {
                if (!actionNode.State.ContainsKey(kvp.Key)) return false;
                else if (actionNode.State[kvp.Key] == null && actionNode.State[kvp.Key] != DesiredState[kvp.Key]) return false;
                else if (actionNode.State[kvp.Key] is object obj && obj != null && !obj.Equals(DesiredState[kvp.Key])) return false;
            }
            return true;
        }

        public override float Heuristic(ActionNode actionNode, BaseGoal goal, ActionNode current)
        {
            var cost = 0f;
            DesiredState.Select(kvp => kvp.Key).ToList().ForEach(key => {
                if (!actionNode.State.ContainsKey(key)) cost++;
                else if (actionNode.State[key] == null && actionNode.State[key] != DesiredState[key]) cost++;
                else if (actionNode.State[key] is object obj && !obj.Equals(DesiredState[key])) cost++;
            });
            return cost;
        }
    }
}
