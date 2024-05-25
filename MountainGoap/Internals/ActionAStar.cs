// <copyright file="ActionAStar.cs" company="Chris Muller">
// Copyright (c) Chris Muller. All rights reserved.
// </copyright>

namespace MountainGoap {
    using System;
    using System.Collections.Concurrent;
    using Priority_Queue;

    /// <summary>
    /// AStar calculator for an action graph.
    /// </summary>
    internal class ActionAStar {
        /// <summary>
        /// Final point at which the calculation arrived.
        /// </summary>
        internal readonly ActionNode? FinalPoint = null;

        /// <summary>
        /// Cost so far to get to each node.
        /// </summary>
        internal readonly ConcurrentDictionary<ActionNode, float> CostSoFar = new();

        /// <summary>
        /// Steps so far to get to each node.
        /// </summary>
        internal readonly ConcurrentDictionary<ActionNode, int> StepsSoFar = new();

        /// <summary>
        /// Dictionary giving the path from start to goal.
        /// </summary>
        internal readonly ConcurrentDictionary<ActionNode, ActionNode> CameFrom = new();

        /// <summary>
        /// Goal state that AStar is trying to achieve.
        /// </summary>
        private readonly BaseGoal goal;

        /// <summary>
        /// Initializes a new instance of the <see cref="ActionAStar"/> class.
        /// </summary>
        /// <param name="graph">Graph to be traversed.</param>
        /// <param name="start">Action from which to start.</param>
        /// <param name="goal">Goal state to be achieved.</param>
        /// <param name="costMaximum">Maximum allowable cost for a plan.</param>
        /// <param name="stepMaximum">Maximum allowable steps for a plan.</param>
        internal ActionAStar(ActionGraph graph, ActionNode start, BaseGoal goal, float costMaximum, int stepMaximum) {
            this.goal = goal;
            FastPriorityQueue<ActionNode> frontier = new(100000);
            frontier.Enqueue(start, 0);
            CameFrom[start] = start;
            CostSoFar[start] = 0;
            StepsSoFar[start] = 0;
            while (frontier.Count > 0) {
                var current = frontier.Dequeue();
                if (MeetsGoal(current, start)) {
                    FinalPoint = current;
                    break;
                }
                foreach (var next in graph.Neighbors(current)) {
                    float newCost = CostSoFar[current] + next.Cost(current.State);
                    int newStepCount = StepsSoFar[current] + 1;
                    if (newCost > costMaximum || newStepCount > stepMaximum) continue;
                    if (!CostSoFar.ContainsKey(next) || newCost < CostSoFar[next]) {
                        CostSoFar[next] = newCost;
                        float priority = newCost + Heuristic(next, goal, current);
                        frontier.Enqueue(next, priority);
                        CameFrom[next] = current;
                        Agent.TriggerOnEvaluatedActionNode(next, CameFrom);
                    }
                }
            }
        }

        private static float Heuristic(ActionNode actionNode, BaseGoal goal, ActionNode current) {
            var cost = 0f;
            if (goal is Goal normalGoal) {
                normalGoal.DesiredState.Select(kvp => kvp.Key).ToList().ForEach(key => {
                    if (!actionNode.State.ContainsKey(key)) cost++;
                    else if (actionNode.State[key] == null && actionNode.State[key] != normalGoal.DesiredState[key]) cost++;
                    else if (actionNode.State[key] is object obj && !obj.Equals(normalGoal.DesiredState[key])) cost++;
                });
            }
            else if (goal is ExtremeGoal extremeGoal) {
                foreach (var kvp in extremeGoal.DesiredState) {
                    var valueDiff = 0f;
                    var valueDiffMultiplier = (actionNode?.Action?.StateCostDeltaMultiplier ?? Action.DefaultStateCostDeltaMultiplier).Invoke(actionNode?.Action, kvp.Key);
                    if (actionNode.State.ContainsKey(kvp.Key) && actionNode.State[kvp.Key] == null) {
                        cost += float.PositiveInfinity;
                        continue;
                    }
                    if (actionNode.State.ContainsKey(kvp.Key) && extremeGoal.DesiredState.ContainsKey(kvp.Key)) valueDiff = Convert.ToSingle(actionNode.State[kvp.Key]) - Convert.ToSingle(current.State[kvp.Key]);
                    if (!actionNode.State.ContainsKey(kvp.Key)) cost += float.PositiveInfinity;
                    else if (!current.State.ContainsKey(kvp.Key)) cost += float.PositiveInfinity;
                    else if (!kvp.Value && actionNode.State[kvp.Key] is object a && current.State[kvp.Key] is object b && IsLowerThanOrEquals(a, b)) cost += valueDiff * valueDiffMultiplier;
                    else if (kvp.Value && actionNode.State[kvp.Key] is object a2 && current.State[kvp.Key] is object b2 && IsHigherThanOrEquals(a2, b2)) cost -= valueDiff * valueDiffMultiplier;
                }
            }
            else if (goal is ComparativeGoal comparativeGoal) {
                foreach (var kvp in comparativeGoal.DesiredState) {
                    var valueDiff2 = 0f;
                    var valueDiffMultiplier = (actionNode?.Action?.StateCostDeltaMultiplier ?? Action.DefaultStateCostDeltaMultiplier).Invoke(actionNode?.Action, kvp.Key);
                    if (actionNode.State.ContainsKey(kvp.Key) && comparativeGoal.DesiredState.ContainsKey(kvp.Key)) valueDiff2 = Math.Abs(Convert.ToSingle(actionNode.State[kvp.Key]) - Convert.ToSingle(current.State[kvp.Key]));
                    if (!actionNode.State.ContainsKey(kvp.Key)) cost += float.PositiveInfinity;
                    else if (!current.State.ContainsKey(kvp.Key)) cost += float.PositiveInfinity;
                    else if (kvp.Value.Operator == ComparisonOperator.Undefined) cost += float.PositiveInfinity;
                    else if (kvp.Value.Operator == ComparisonOperator.Equals && actionNode.State[kvp.Key] is object obj && !obj.Equals(comparativeGoal.DesiredState[kvp.Key].Value)) cost += valueDiff2 * valueDiffMultiplier;
                    else if (kvp.Value.Operator == ComparisonOperator.LessThan && actionNode.State[kvp.Key] is object a && comparativeGoal.DesiredState[kvp.Key].Value is object b && !IsLowerThan(a, b)) cost += valueDiff2 * valueDiffMultiplier;
                    else if (kvp.Value.Operator == ComparisonOperator.GreaterThan && actionNode.State[kvp.Key] is object a2 && comparativeGoal.DesiredState[kvp.Key].Value is object b2 && !IsHigherThan(a2, b2)) cost += valueDiff2 * valueDiffMultiplier;
                    else if (kvp.Value.Operator == ComparisonOperator.LessThanOrEquals && actionNode.State[kvp.Key] is object a3 && comparativeGoal.DesiredState[kvp.Key].Value is object b3 && !IsLowerThanOrEquals(a3, b3)) cost += valueDiff2 * valueDiffMultiplier;
                    else if (kvp.Value.Operator == ComparisonOperator.GreaterThanOrEquals && actionNode.State[kvp.Key] is object a4 && comparativeGoal.DesiredState[kvp.Key].Value is object b4 && !IsHigherThanOrEquals(a4, b4)) cost += valueDiff2 * valueDiffMultiplier;
                }
            }
            return cost;
        }

        private static bool IsLowerThan(object a, object b) {
            return Utils.IsLowerThan(a, b);
        }

        private static bool IsHigherThan(object a, object b) {
            return Utils.IsHigherThan(a, b);
        }

        private static bool IsLowerThanOrEquals(object a, object b) {
            return Utils.IsLowerThanOrEquals(a, b);
        }

        private static bool IsHigherThanOrEquals(object a, object b) {
            return Utils.IsHigherThanOrEquals(a, b);
        }

        private bool MeetsGoal(ActionNode actionNode, ActionNode current) {
            return Utils.MeetsGoal(goal, actionNode, current);
        }
    }
}
