// <copyright file="ActionAStar.cs" company="Chris Muller">
// Copyright (c) Chris Muller. All rights reserved.
// </copyright>

namespace MountainGoap {
    using System;
    using System.Collections.Generic;
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
        internal readonly Dictionary<ActionNode, float> CostSoFar = new();

        /// <summary>
        /// Dictionary giving the path from start to goal.
        /// </summary>
        internal readonly Dictionary<ActionNode, ActionNode> CameFrom = new();

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
        internal ActionAStar(ActionGraph graph, ActionNode start, BaseGoal goal) {
            this.goal = goal;
            FastPriorityQueue<ActionNode> frontier = new(100000);
            frontier.Enqueue(start, 0);
            CameFrom[start] = start;
            CostSoFar[start] = 0;
            while (frontier.Count > 0) {
                var current = frontier.Dequeue();
                if (current.Action != null) {
                    Console.WriteLine($"evaluating {current.Action.Name}");
                    if (current.Action.GetParameter("location") is not null) Console.WriteLine($"location: {current.Action.GetParameter("location")}");
                    var traceback = current;
                    while (CameFrom[traceback].Action != null) {
                        Console.WriteLine($"\t\ttraceback: {traceback.Action?.Name}");
                        traceback = CameFrom[traceback];
                    }
                }
                if (MeetsGoal(current, start)) {
                    FinalPoint = current;
                    break;
                }
                foreach (var next in graph.Neighbors(current)) {
                    Console.WriteLine($"\tneighbor: {next.Action?.Name}");
                    if (next.Action?.GetParameter("location") is not null) Console.WriteLine($"\tlocation: {next.Action.GetParameter("location")}");
                    float newCost = CostSoFar[current] + next.Cost(current.State);
                    Console.WriteLine($"\tcost so far is {newCost}");
                    if (!CostSoFar.ContainsKey(next) || newCost < CostSoFar[next]) {
                        CostSoFar[next] = newCost;
                        float priority = newCost + Heuristic(next, goal, current);
                        frontier.Enqueue(next, priority);
                        CameFrom[next] = current;
                    }
                }
            }
        }

        private static float Heuristic(ActionNode actionNode, BaseGoal goal, ActionNode current) {
            var cost = 0f;
            if (goal is Goal normalGoal) {
                normalGoal.DesiredState.Select(kvp => kvp.Key).ToList().ForEach(key => {
                    if (!actionNode.State.ContainsKey(key)) cost++;
                    else if (actionNode.State[key] == null && actionNode.State[key] != normalGoal.DesiredState[key] ) cost++;
                    else if (actionNode.State[key] is object obj && !obj.Equals(normalGoal.DesiredState[key])) cost++;
                });
            }
            else if (goal is ExtremeGoal extremeGoal) {
                foreach (var kvp in extremeGoal.DesiredState) {
                    if (actionNode.State.ContainsKey(kvp.Key) && actionNode.State[kvp.Key] == null) {
                        cost += float.PositiveInfinity;
                        continue;
                    }
                    if (!actionNode.State.ContainsKey(kvp.Key)) cost += float.PositiveInfinity;
                    else if (!current.State.ContainsKey(kvp.Key)) cost += float.PositiveInfinity;
                    else if (kvp.Value && actionNode.State[kvp.Key] is object a && current.State[kvp.Key] is object b && IsHigherThan(a, b)) cost++;
                    else if (!kvp.Value && actionNode.State[kvp.Key] is object a2 && current.State[kvp.Key] is object b2 && IsLowerThan(a2, b2)) cost++;
                }
            }
            else if (goal is ComparativeGoal comparativeGoal) {
                foreach (var kvp in comparativeGoal.DesiredState) {
                    if (!actionNode.State.ContainsKey(kvp.Key)) cost += float.PositiveInfinity;
                    else if (kvp.Value.Operator == ComparisonOperator.Undefined) cost += float.PositiveInfinity;
                    else if (kvp.Value.Operator == ComparisonOperator.Equals && actionNode.State[kvp.Key] is object obj && obj.Equals(comparativeGoal.DesiredState[kvp.Key].Value)) cost += actionNode.Cost(current.State);
                    else if (kvp.Value.Operator == ComparisonOperator.LessThan && actionNode.State[kvp.Key] is object a && comparativeGoal.DesiredState[kvp.Key] is object b && IsLowerThan(a, b)) cost += actionNode.Cost(current.State);
                    else if (kvp.Value.Operator == ComparisonOperator.GreaterThan && actionNode.State[kvp.Key] is object a2 && comparativeGoal.DesiredState[kvp.Key] is object b2 && IsHigherThan(a2, b2)) cost += actionNode.Cost(current.State);
                    else if (kvp.Value.Operator == ComparisonOperator.LessThanOrEquals && actionNode.State[kvp.Key] is object a3 && comparativeGoal.DesiredState[kvp.Key] is object b3 && IsLowerThanOrEquals(a3, b3)) cost += actionNode.Cost(current.State);
                    else if (kvp.Value.Operator == ComparisonOperator.GreaterThanOrEquals && actionNode.State[kvp.Key] is object a4 && comparativeGoal.DesiredState[kvp.Key] is object b4 && IsHigherThanOrEquals(a4, b4)) cost += actionNode.Cost(current.State);
                }
                cost /= comparativeGoal.DesiredState.Count;
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
            if (goal is Goal normalGoal) {
#pragma warning disable S3267 // Loops should be simplified with "LINQ" expressions
                foreach (var kvp in normalGoal.DesiredState) {
                    if (!actionNode.State.ContainsKey(kvp.Key)) return false;
                    else if (actionNode.State[kvp.Key] == null && actionNode.State[kvp.Key] != normalGoal.DesiredState[kvp.Key]) return false;
                    else if (actionNode.State[kvp.Key] is object obj && obj != null && !obj.Equals(normalGoal.DesiredState[kvp.Key])) return false;
                }
#pragma warning restore S3267 // Loops should be simplified with "LINQ" expressions
            }
            else if (goal is ExtremeGoal extremeGoal) {
                if (actionNode.Action == null) return false;
                foreach (var kvp in extremeGoal.DesiredState) {
                    if (!actionNode.State.ContainsKey(kvp.Key)) return false;
                    else if (!current.State.ContainsKey(kvp.Key)) return false;
                    else if (kvp.Value && actionNode.State[kvp.Key] is object a && current.State[kvp.Key] is object b && IsLowerThan(a, b)) return false;
                    else if (!kvp.Value && actionNode.State[kvp.Key] is object a2 && current.State[kvp.Key] is object b2 && IsHigherThan(a2, b2)) return false;
                }
            }
            else if (goal is ComparativeGoal comparativeGoal) {
                if (actionNode.Action == null) return false;
                foreach (var kvp in comparativeGoal.DesiredState) {
                    if (!actionNode.State.ContainsKey(kvp.Key)) return false;
                    else if (!current.State.ContainsKey(kvp.Key)) return false;
                    else if (kvp.Value.Operator == ComparisonOperator.Undefined) return false;
                    else if (kvp.Value.Operator == ComparisonOperator.Equals && actionNode.State[kvp.Key] is object obj && !obj.Equals(comparativeGoal.DesiredState[kvp.Key].Value)) return false;
                    else if (kvp.Value.Operator == ComparisonOperator.LessThan && actionNode.State[kvp.Key] is object a && current.State[kvp.Key] is object b && !IsLowerThan(a, b)) return false;
                    else if (kvp.Value.Operator == ComparisonOperator.GreaterThan && actionNode.State[kvp.Key] is object a2 && current.State[kvp.Key] is object b2 && !IsHigherThan(a2, b2)) return false;
                    else if (kvp.Value.Operator == ComparisonOperator.LessThanOrEquals && actionNode.State[kvp.Key] is object a3 && current.State[kvp.Key] is object b3 && !IsLowerThanOrEquals(a3, b3)) return false;
                    else if (kvp.Value.Operator == ComparisonOperator.GreaterThanOrEquals && actionNode.State[kvp.Key] is object a4 && current.State[kvp.Key] is object b4 && !IsHigherThanOrEquals(a4, b4)) return false;
                }
            }
            return true;
        }
    }
}
