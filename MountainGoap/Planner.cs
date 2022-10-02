// <copyright file="Planner.cs" company="Chris Muller">
// Copyright (c) Chris Muller. All rights reserved.
// </copyright>

namespace MountainGoap {
    /// <summary>
    /// Planner for an agent.
    /// </summary>
    public class Planner {
        /// <summary>
        /// Makes a plan to achieve the agent's goals.
        /// </summary>
        /// <param name="agent">Agent using the planner.</param>
        public static void Plan(Agent agent) {
            float bestPlanUtility = 0;
            ActionAStar? astar;
            ActionAStar? bestAstar = null;
            ActionNode? cursor = null;
            foreach (var goal in agent.Goals) {
                ActionGraph graph = new(agent.Actions, agent.State);
                ActionNode start = new(null, agent.State);
                astar = new(graph, start, goal.DesiredState);
                cursor = astar.FinalPoint;
                if (cursor != null && cursor.Action != null && astar.CostSoFar.ContainsKey(cursor) && goal.Weight / astar.CostSoFar[cursor] > bestPlanUtility) {
                    bestPlanUtility = astar.CostSoFar[cursor];
                    bestAstar = astar;
                }
            }
            if (bestPlanUtility > 0 && cursor is not null && bestAstar is not null) {
                UpdateAgentActionList(cursor, bestAstar, agent);
                agent.IsBusy = true;
            }
            agent.IsPlanning = false;
        }

        /// <summary>
        /// Updates the agent action list with the new plan. Only supports executing one sequence of events at a time for now.
        /// </summary>
        /// <param name="start">Starting node.</param>
        /// <param name="astar">AStar object used to calculate plan.</param>
        /// <param name="agent">Agent that will implement the plan.</param>
        private static void UpdateAgentActionList(ActionNode start, ActionAStar astar, Agent agent) {
            ActionNode? cursor = start;
            List<Action> actionList = new();
            while (cursor != null && cursor.Action != null && astar.CameFrom.ContainsKey(cursor)) {
                actionList.Add(cursor.Action);
                cursor = astar.CameFrom[cursor];
            }
            actionList.Reverse();
            agent.CurrentActionSequences.Add(actionList);
        }
    }
}