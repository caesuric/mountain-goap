// <copyright file="AgentActionSequenceCompletedEvent.cs" company="Chris Muller">
// Copyright (c) Chris Muller. All rights reserved.
// </copyright>

namespace MountainGoap.Async {
    /// <summary>
    /// Delegate type for a listener to the event that fires when an agent completes an action sequence.
    /// </summary>
    /// <param name="agent">Agent executing the action sequence.</param>
    /// <returns>Async Task.</returns>
    public delegate Task AgentActionSequenceCompletedEvent(Agent agent);
}
