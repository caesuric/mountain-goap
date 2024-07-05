// <copyright file="ComparativeGoal.cs" company="Chris Muller">
// Copyright (c) Chris Muller. All rights reserved.
// </copyright>

namespace MountainGoap {
    using System.Collections.Generic;

    /// <summary>
    /// Represents a goal to be achieved for an agent.
    /// </summary>
    public class ComparativeGoal : BaseGoal {
        /// <summary>
        /// Desired state for the comparative goal.
        /// </summary>
        internal readonly Dictionary<string, ComparisonValuePair> DesiredState;

        /// <summary>
        /// Initializes a new instance of the <see cref="ComparativeGoal"/> class.
        /// </summary>
        /// <param name="name">Name of the goal.</param>
        /// <param name="weight">Weight to give the goal.</param>
        /// <param name="desiredState">Desired state for the comparative goal.</param>
        public ComparativeGoal(string? name = null, float weight = 1f, Dictionary<string, ComparisonValuePair>? desiredState = null)
            : base(name, weight) {
            DesiredState = desiredState ?? new();
        }

        public override bool MeetsGoal(ActionNode actionNode, ActionNode current)
        {
            if (actionNode.Action == null) return false;
            foreach (var kvp in DesiredState) {
                if (!actionNode.State.ContainsKey(kvp.Key)) return false;
                else if (!current.State.ContainsKey(kvp.Key)) return false;
                else if (kvp.Value.Operator == ComparisonOperator.Undefined) return false;
                else if (kvp.Value.Operator == ComparisonOperator.Equals && actionNode.State[kvp.Key] is object obj && !obj.Equals(DesiredState[kvp.Key].Value)) return false;
                else if (kvp.Value.Operator == ComparisonOperator.LessThan && actionNode.State[kvp.Key] is object a && DesiredState[kvp.Key].Value is object b && !Utils.IsLowerThan(a, b)) return false;
                else if (kvp.Value.Operator == ComparisonOperator.GreaterThan && actionNode.State[kvp.Key] is object a2 && DesiredState[kvp.Key].Value is object b2 && !Utils.IsHigherThan(a2, b2)) return false;
                else if (kvp.Value.Operator == ComparisonOperator.LessThanOrEquals && actionNode.State[kvp.Key] is object a3 && DesiredState[kvp.Key].Value is object b3 && !Utils.IsLowerThanOrEquals(a3, b3)) return false;
                else if (kvp.Value.Operator == ComparisonOperator.GreaterThanOrEquals && actionNode.State[kvp.Key] is object a4 && DesiredState[kvp.Key].Value is object b4 && !Utils.IsHigherThanOrEquals(a4, b4)) return false;
            }

            return true;
        }

        public override float Heuristic(ActionNode actionNode, BaseGoal goal, ActionNode current)
        {
            var cost = 1f;
            foreach (var kvp in DesiredState) {
                var valueDiff2 = 0f;
                var valueDiffMultiplier = (actionNode?.Action?.StateCostDeltaMultiplier ?? Action.DefaultStateCostDeltaMultiplier).Invoke(actionNode?.Action, kvp.Key);
                if (actionNode.State.ContainsKey(kvp.Key) && DesiredState.ContainsKey(kvp.Key)) valueDiff2 = Math.Abs(Convert.ToSingle(actionNode.State[kvp.Key]) - Convert.ToSingle(current.State[kvp.Key]));
                if (!actionNode.State.ContainsKey(kvp.Key)) cost += float.PositiveInfinity;
                else if (!current.State.ContainsKey(kvp.Key)) cost += float.PositiveInfinity;
                else if (kvp.Value.Operator == ComparisonOperator.Undefined) cost += float.PositiveInfinity;
                else if (kvp.Value.Operator == ComparisonOperator.Equals && actionNode.State[kvp.Key] is object obj && !obj.Equals(DesiredState[kvp.Key].Value)) cost += valueDiff2 * valueDiffMultiplier;
                else if (kvp.Value.Operator == ComparisonOperator.LessThan && actionNode.State[kvp.Key] is object a && DesiredState[kvp.Key].Value is object b && !Utils.IsLowerThan(a, b)) cost += valueDiff2 * valueDiffMultiplier;
                else if (kvp.Value.Operator == ComparisonOperator.GreaterThan && actionNode.State[kvp.Key] is object a2 && DesiredState[kvp.Key].Value is object b2 && !Utils.IsHigherThan(a2, b2)) cost += valueDiff2 * valueDiffMultiplier;
                else if (kvp.Value.Operator == ComparisonOperator.LessThanOrEquals && actionNode.State[kvp.Key] is object a3 && DesiredState[kvp.Key].Value is object b3 && !Utils.IsLowerThanOrEquals(a3, b3)) cost += valueDiff2 * valueDiffMultiplier;
                else if (kvp.Value.Operator == ComparisonOperator.GreaterThanOrEquals && actionNode.State[kvp.Key] is object a4 && DesiredState[kvp.Key].Value is object b4 && !Utils.IsHigherThanOrEquals(a4, b4)) cost += valueDiff2 * valueDiffMultiplier;
            }

            return cost;
        }
    }
}