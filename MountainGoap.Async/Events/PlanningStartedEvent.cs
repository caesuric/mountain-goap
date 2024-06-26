// <copyright file="PlanningStartedEvent.cs" company="Chris Muller">
// Copyright (c) Chris Muller. All rights reserved.
// </copyright>

namespace MountainGoap.Async {
    /// <summary>
    /// Delegate type for a listener to the event that fires when an agent begins planning.
    /// </summary>
    /// <param name="agent">Agent doing the planning.</param>
    /// <returns>Async Task.</returns>
    public delegate Task PlanningStartedEvent(Agent agent);
}
