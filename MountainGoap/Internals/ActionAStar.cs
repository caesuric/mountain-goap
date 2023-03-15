// <copyright file="ActionAStar.cs" company="Chris Muller">
// Copyright (c) Chris Muller. All rights reserved.
// </copyright>

namespace MountainGoap {
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
                if (MeetsGoal(current, start)) {
                    FinalPoint = current;
                    break;
                }
                foreach (var next in graph.Neighbors(current)) {
                    float newCost = CostSoFar[current] + next.Cost();
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
            var wrongStates = 0;
            if (goal is Goal normalGoal) {
                foreach (var kvp in normalGoal.DesiredState) {
                    if (!actionNode.State.ContainsKey(kvp.Key)) wrongStates++;
                    else if (!actionNode.State[kvp.Key].Equals(normalGoal.DesiredState[kvp.Key])) wrongStates++;
                }
            }
            else if (goal is ExtremeGoal extremeGoal) {
                foreach (var kvp in extremeGoal.States) {
                    if (!actionNode.State.ContainsKey(kvp.Key)) wrongStates++;
                    else if (!current.State.ContainsKey(kvp.Key)) wrongStates++;
                    else if (kvp.Value && IsLowerThan(actionNode.State[kvp.Key], current.State[kvp.Key])) wrongStates++;
                    else if (!kvp.Value && IsHigherThan(actionNode.State[kvp.Key], current.State[kvp.Key])) wrongStates++;
                }
            }
            else if (goal is ComparativeGoal comparativeGoal) {
                foreach (var kvp in comparativeGoal.DesiredState) {
                    if (!actionNode.State.ContainsKey(kvp.Key)) wrongStates++;
                    else if (!current.State.ContainsKey(kvp.Key)) wrongStates++;
                    else if (kvp.Value.Operator == ComparisonOperator.Undefined) wrongStates++;
                    else if (kvp.Value.Operator == ComparisonOperator.Equals && !actionNode.State[kvp.Key].Equals(comparativeGoal.DesiredState[kvp.Key].Value)) wrongStates++;
                    else if (kvp.Value.Operator == ComparisonOperator.LessThan && !IsLowerThan(actionNode.State[kvp.Key], current.State[kvp.Key])) wrongStates++;
                    else if (kvp.Value.Operator == ComparisonOperator.GreaterThan && !IsHigherThan(actionNode.State[kvp.Key], current.State[kvp.Key])) wrongStates++;
                    else if (kvp.Value.Operator == ComparisonOperator.LessThanOrEquals && !IsLowerThanOrEquals(actionNode.State[kvp.Key], current.State[kvp.Key])) wrongStates++;
                    else if (kvp.Value.Operator == ComparisonOperator.GreaterThanOrEquals && !IsHigherThanOrEquals(actionNode.State[kvp.Key], current.State[kvp.Key])) wrongStates++;
                }
            }
            return wrongStates;
        }

        private static bool IsLowerThan(object a, object b) {
            if (a == null || b == null) return false;
            if (a is int intA && b is int intB) return intA < intB;
            if (a is long longA && b is long longB) return longA < longB;
            if (a is float floatA && b is float floatB) return floatA < floatB;
            if (a is double doubleA && b is double doubleB) return doubleA < doubleB;
            if (a is decimal decimalA && b is decimal decimalB) return decimalA < decimalB;
            if (a is DateTime dateTimeA && b is DateTime dateTimeB) return dateTimeA < dateTimeB;
            return false;
        }

        private static bool IsHigherThan(object a, object b) {
            if (a == null || b == null) return false;
            if (a is int intA && b is int intB) return intA > intB;
            if (a is long longA && b is long longB) return longA > longB;
            if (a is float floatA && b is float floatB) return floatA > floatB;
            if (a is double doubleA && b is double doubleB) return doubleA > doubleB;
            if (a is decimal decimalA && b is decimal decimalB) return decimalA > decimalB;
            if (a is DateTime dateTimeA && b is DateTime dateTimeB) return dateTimeA > dateTimeB;
            return false;
        }

        private static bool IsLowerThanOrEquals(object a, object b) {
            if (a == null || b == null) return false;
            if (a is int intA && b is int intB) return intA <= intB;
            if (a is long longA && b is long longB) return longA <= longB;
            if (a is float floatA && b is float floatB) return floatA <= floatB;
            if (a is double doubleA && b is double doubleB) return doubleA <= doubleB;
            if (a is decimal decimalA && b is decimal decimalB) return decimalA <= decimalB;
            if (a is DateTime dateTimeA && b is DateTime dateTimeB) return dateTimeA <= dateTimeB;
            return false;
        }

        private static bool IsHigherThanOrEquals(object a, object b) {
            if (a == null || b == null) return false;
            if (a is int intA && b is int intB) return intA >= intB;
            if (a is long longA && b is long longB) return longA >= longB;
            if (a is float floatA && b is float floatB) return floatA >= floatB;
            if (a is double doubleA && b is double doubleB) return doubleA >= doubleB;
            if (a is decimal decimalA && b is decimal decimalB) return decimalA >= decimalB;
            if (a is DateTime dateTimeA && b is DateTime dateTimeB) return dateTimeA >= dateTimeB;
            return false;
        }

        private bool MeetsGoal(ActionNode actionNode, ActionNode current) {
            if (goal is Goal normalGoal) {
                foreach (var kvp in normalGoal.DesiredState) {
                    if (!actionNode.State.ContainsKey(kvp.Key)) return false;
                    else if (!actionNode.State[kvp.Key].Equals(normalGoal.DesiredState[kvp.Key])) return false;
                }
                return true;
            }
            else if (goal is ExtremeGoal extremeGoal) {
                foreach (var kvp in extremeGoal.States) {
                    if (!actionNode.State.ContainsKey(kvp.Key)) return false;
                    else if (!current.State.ContainsKey(kvp.Key)) return false;
                    else if (kvp.Value && IsLowerThan(actionNode.State[kvp.Key], current.State[kvp.Key])) return false;
                    else if (!kvp.Value && IsHigherThan(actionNode.State[kvp.Key], current.State[kvp.Key])) return false;
                }
                return true;
            }
            else if (goal is ComparativeGoal comparativeGoal) {
                foreach (var kvp in comparativeGoal.DesiredState) {
                    if (!actionNode.State.ContainsKey(kvp.Key)) return false;
                    else if (!current.State.ContainsKey(kvp.Key)) return false;
                    else if (kvp.Value.Operator == ComparisonOperator.Undefined) return false;
                    else if (kvp.Value.Operator == ComparisonOperator.Equals && !actionNode.State[kvp.Key].Equals(comparativeGoal.DesiredState[kvp.Key].Value)) return false;
                    else if (kvp.Value.Operator == ComparisonOperator.LessThan && !IsLowerThan(actionNode.State[kvp.Key], current.State[kvp.Key])) return false;
                    else if (kvp.Value.Operator == ComparisonOperator.GreaterThan && !IsHigherThan(actionNode.State[kvp.Key], current.State[kvp.Key])) return false;
                    else if (kvp.Value.Operator == ComparisonOperator.LessThanOrEquals && !IsLowerThanOrEquals(actionNode.State[kvp.Key], current.State[kvp.Key])) return false;
                    else if (kvp.Value.Operator == ComparisonOperator.GreaterThanOrEquals && !IsHigherThanOrEquals(actionNode.State[kvp.Key], current.State[kvp.Key])) return false;
                }
            }
            return false;
        }
    }
}
