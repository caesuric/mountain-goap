// <copyright file="ExecutorCallback.cs" company="Chris Muller">
// Copyright (c) Chris Muller. All rights reserved.
// </copyright>

namespace MountainGoap {
    /// <summary>
    /// Delegate type for a callback that performs an action.
    /// </summary>
    /// <param name="agent">Agent executing the action.</param>
    /// <param name="action">Action being executed.</param>
    /// <returns>New execution status of the action.</returns>
    public delegate ExecutionStatus ExecutorCallback(Agent agent, Action action);

    /// <summary>
    /// Delegate type for a callback that performs an action as a Task.
    /// </summary>
    /// <param name="agent">Agent executing the action.</param>
    /// <param name="action">Action being executed.</param>
    /// <returns>New execution status of the action.</returns>
    public delegate Task<ExecutionStatus> ExecutorAsTaskCallback(Agent agent, Action action);
}
