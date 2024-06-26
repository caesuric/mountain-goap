// <copyright file="Planner.cs" company="Chris Muller">
// Copyright (c) Chris Muller. All rights reserved.
// </copyright>

namespace MountainGoap {
    using System.Collections.Generic;

    /// <summary>
    /// Planner for an agent.
    /// </summary>
    internal static class Planner {
        /// <summary>
        /// Makes a plan to achieve the agent's goals.
        /// </summary>
        /// <param name="agent">Agent using the planner.</param>
        /// <param name="costMaximum">Maximum allowable cost for a plan.</param>
        /// <param name="stepMaximum">Maximum allowable steps for a plan.</param>
        /// <returns>Async Task.</returns>
        internal static async Task PlanAsync(Agent agent, float costMaximum, int stepMaximum) {
            await Agent.TriggerOnPlanningStarted(agent);
            float bestPlanUtility = 0;
            ActionAStar? astar;
            ActionNode? cursor;
            ActionAStar? bestAstar = null;
            BaseGoal? bestGoal = null;
            foreach (var goal in agent.Goals) {
                await Agent.TriggerOnPlanningStartedForSingleGoal(agent, goal);
                var graph = new ActionGraph();
                await graph.InitAsync(agent.Actions, agent.State);
                var start = new ActionNode(null, agent.State, new());
                astar = new ActionAStar();
                await astar.InitAsync(graph, start, goal, costMaximum, stepMaximum);
                cursor = astar.FinalPoint;
                if (cursor is not null && astar.CostSoFar[cursor] == 0) await Agent.TriggerOnPlanningFinishedForSingleGoal(agent, goal, 0);
                else if (cursor is not null) await Agent.TriggerOnPlanningFinishedForSingleGoal(agent, goal, goal.Weight / astar.CostSoFar[cursor]);
                if (cursor is not null && cursor.Action is not null && astar.CostSoFar.ContainsKey(cursor) && goal.Weight / astar.CostSoFar[cursor] > bestPlanUtility) {
                    bestPlanUtility = goal.Weight / astar.CostSoFar[cursor];
                    bestAstar = astar;
                    bestGoal = goal;
                }
            }
            if (bestPlanUtility > 0 && bestAstar is not null && bestGoal is not null && bestAstar.FinalPoint is not null) {
                await UpdateAgentActionListAsync(bestAstar.FinalPoint, bestAstar, agent);
                agent.IsBusy = true;
                await Agent.TriggerOnPlanningFinished(agent, bestGoal, bestPlanUtility);
            }
            else await Agent.TriggerOnPlanningFinished(agent, null, 0);
            agent.IsPlanning = false;
        }

        /// <summary>
        /// Updates the agent action list with the new plan. Only supports executing one sequence of events at a time for now.
        /// </summary>
        /// <param name="start">Starting node.</param>
        /// <param name="astar">AStar object used to calculate plan.</param>
        /// <param name="agent">Agent that will implement the plan.</param>
        private static async Task UpdateAgentActionListAsync(ActionNode start, ActionAStar astar, Agent agent) {
            ActionNode? cursor = start;
            List<Action> actionList = new();
            while (cursor != null && cursor.Action != null && astar.CameFrom.ContainsKey(cursor)) {
                actionList.Add(cursor.Action);
                cursor = astar.CameFrom[cursor];
            }
            actionList.Reverse();
            agent.CurrentActionSequences.Add(actionList);
            await Agent.TriggerOnPlanUpdated(agent, actionList);
        }
    }
}