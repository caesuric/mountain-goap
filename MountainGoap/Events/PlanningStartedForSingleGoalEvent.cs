// <copyright file="PlanningStartedForSingleGoalEvent.cs" company="Chris Muller">
// Copyright (c) Chris Muller. All rights reserved.
// </copyright>

namespace MountainGoap {
    /// <summary>
    /// Delegate type for a listener to the event that fires when an agent starts planning for a single goal.
    /// </summary>
    /// <param name="agent">Agent doing the planning.</param>
    /// <param name="goal">Goal for which planning was started.</param>
    /// <returns>Async Task.</returns>
    public delegate Task PlanningStartedForSingleGoalEvent(Agent agent, BaseGoal goal);
}
