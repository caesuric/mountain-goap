// <copyright file="ExecutorCallback.cs" company="Chris Muller">
// Copyright (c) Chris Muller. All rights reserved.
// </copyright>

namespace MountainGoap {
    /// <summary>
    /// Delegate type for a callback that defines a list of all possible parameter states for the given state.
    /// </summary>
    /// <param name="agent">Agent executing the action.</param>
    /// <param name="action">Action being executed.</param>
    /// <returns>New execution status of the action.</returns>
    public delegate Task<ExecutionStatus> ExecutorCallback(Agent agent, Action action);
}
