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
        private readonly Dictionary<string, object> goal;

        /// <summary>
        /// Initializes a new instance of the <see cref="ActionAStar"/> class.
        /// </summary>
        /// <param name="graph">Graph to be traversed.</param>
        /// <param name="start">Action from which to start.</param>
        /// <param name="goal">Goal state to be achieved.</param>
        internal ActionAStar(ActionGraph graph, ActionNode start, Dictionary<string, object> goal) {
            this.goal = goal;
            FastPriorityQueue<ActionNode> frontier = new(100000);
            frontier.Enqueue(start, 0);
            CameFrom[start] = start;
            CostSoFar[start] = 0;
            while (frontier.Count > 0) {
                var current = frontier.Dequeue();
                if (MeetsGoal(current)) {
                    FinalPoint = current;
                    break;
                }
                foreach (var next in graph.Neighbors(current)) {
                    float newCost = CostSoFar[current] + next.Cost();
                    if (!CostSoFar.ContainsKey(next) || newCost < CostSoFar[next]) {
                        CostSoFar[next] = newCost;
                        float priority = newCost + Heuristic(next, goal);
                        frontier.Enqueue(next, priority);
                        CameFrom[next] = current;
                    }
                }
            }
        }

        private static float Heuristic(ActionNode actionNode, Dictionary<string, object> goal) {
            var wrongStates = 0;
            foreach (var kvp in goal) {
                if (!actionNode.State.ContainsKey(kvp.Key)) wrongStates++;
                else if (!actionNode.State[kvp.Key].Equals(goal[kvp.Key])) wrongStates++;
            }
            return wrongStates;
        }

        private bool MeetsGoal(ActionNode actionNode) {
            foreach (var kvp in goal) {
                if (!actionNode.State.ContainsKey(kvp.Key)) return false;
                else if (!actionNode.State[kvp.Key].Equals(goal[kvp.Key])) return false;
            }
            return true;
        }
    }
}
