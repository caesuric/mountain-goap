// <copyright file="ActionAStar.cs" company="Chris Muller">
// Copyright (c) Chris Muller. All rights reserved.
// </copyright>

namespace MountainGoap {
    /// <summary>
    /// AStar calculator for an action graph.
    /// </summary>
    internal class ActionAStar {
        /// <summary>
        /// Final point at which the calculation arrived.
        /// </summary>
        internal readonly ActionNode? FinalPoint = null;

        /// <summary>
        /// Goal state that AStar is trying to achieve.
        /// </summary>
        private readonly Dictionary<string, object> goal;

        /// <summary>
        /// Dictionary giving the path from start to goal.
        /// </summary>
        private readonly Dictionary<ActionNode, ActionNode> cameFrom = new ();

        /// <summary>
        /// Cost so far to get to each node.
        /// </summary>
        private readonly Dictionary<ActionNode, float> costSoFar = new();

        /// <summary>
        /// Initializes a new instance of the <see cref="ActionAStar"/> class.
        /// </summary>
        /// <param name="graph">Graph to be traversed.</param>
        /// <param name="start">Action from which to start.</param>
        /// <param name="goal">Goal state to be achieved.</param>
        internal ActionAStar(ActionGraph graph, ActionNode start, Dictionary<string, object> goal) {
            this.goal = goal;
            PriorityQueue<ActionNode, float> frontier = new ();
            frontier.Enqueue(start, 0);
            cameFrom[start] = start;
            costSoFar[start] = 0;
            while (frontier.Count > 0) {
                var current = frontier.Dequeue();
                if (MeetsGoal(current)) {
                    FinalPoint = current;
                    break;
                }
                foreach (var next in graph.Neighbors(current)) {
                    float newCost = costSoFar[current] + next.Cost();
                    if (!costSoFar.ContainsKey(next) || newCost < costSoFar[next]) {
                        costSoFar[next] = newCost;
                        float priority = newCost + Heuristic(next, goal);
                        frontier.Enqueue(next, priority);
                        cameFrom[next] = current;
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
