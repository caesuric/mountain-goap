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
        internal ActionAStar(ActionGraph graph, ActionNode start, BaseGoal goal, int maxStepDepth, float costMaximum, int stepMaximum) {
            this.goal = goal;
            FastPriorityQueue<ActionNode> frontier = new(1000);
            frontier.Enqueue(start, 0);
            CameFrom[start] = start;
            CostSoFar[start] = 0;
            StepsSoFar[start] = 0;
            var currentDepth = 0;
            while (frontier.Count > 0 && currentDepth < maxStepDepth && (frontier.Count + 1 < frontier.MaxSize)) {
                var current = frontier.Dequeue();
                if (goal.MeetsGoal(current, start)) {
                    FinalPoint = current;
                    break;
                }
                foreach (var next in graph.Neighbors(current))
                {
                    currentDepth++;
                    float newCost = CostSoFar[current] + next.Cost(current.State);
                    int newStepCount = StepsSoFar[current] + 1;
                    if (newCost > costMaximum || newStepCount > stepMaximum) continue;
                    if (!CostSoFar.ContainsKey(next) || newCost < CostSoFar[next]) {
                        CostSoFar[next] = newCost;
                        StepsSoFar[next] = newStepCount;
                        float priority = newCost + goal.Heuristic(next, goal, current);
                        frontier.Enqueue(next, priority);
                        CameFrom[next] = current;
                        Agent.TriggerOnEvaluatedActionNode(next, CameFrom);
                    }
                }
            }
        }
    }
}
