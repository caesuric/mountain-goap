// <copyright file="AgentActionSequenceCompletedEvent.cs" company="Chris Muller">
// Copyright (c) Chris Muller. All rights reserved.
// </copyright>

namespace MountainGoap {
    /// <summary>
    /// Delegate type for a listener to the event that fires when an agent completes an action sequence.
    /// </summary>
    /// <param name="agent">Agent executing the action sequence.</param>
    public delegate void AgentActionSequenceCompletedEvent(Agent agent);
}
