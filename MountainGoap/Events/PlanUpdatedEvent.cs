// <copyright file="PlanUpdatedEvent.cs" company="Chris Muller">
// Copyright (c) Chris Muller. All rights reserved.
// </copyright>

namespace MountainGoap {
    /// <summary>
    /// Delegate type for a listener to the event that fires when an agent has a new plan.
    /// </summary>
    /// <param name="agent">Agent executing the step of work.</param>
    /// <param name="plan">Plan determined to be optimal for the agent.</param>
    public delegate void PlanUpdatedEvent(Agent agent, List<Action> plan);
}
