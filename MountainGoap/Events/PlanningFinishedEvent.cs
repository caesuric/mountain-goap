// <copyright file="PlanningFinishedEvent.cs" company="Chris Muller">
// Copyright (c) Chris Muller. All rights reserved.
// </copyright>

namespace MountainGoap {
    /// <summary>
    /// Delegate type for a listener to the event that fires when an agent finishes planning.
    /// </summary>
    /// <param name="agent">Agent doing the planning.</param>
    /// /// <param name="goal">Goal selected, or null if no valid plan was selected.</param>
    /// <param name="utility">Calculated utility of the plan.</param>
    public delegate void PlanningFinishedEvent(Agent agent, BaseGoal? goal, float utility);
}
