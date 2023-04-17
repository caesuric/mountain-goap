// <copyright file="FinishExecuteActionEvent.cs" company="Chris Muller">
// Copyright (c) Chris Muller. All rights reserved.
// </copyright>

namespace MountainGoap {
    using System.Collections.Generic;

    /// <summary>
    /// Delegate type for a listener to the event that fires when an action finishes executing.
    /// </summary>
    /// <param name="agent">Agent executing the action.</param>
    /// <param name="action">Action being executed.</param>
    /// <param name="status">Execution status of the action.</param>
    /// <param name="parameters">Parameters to the action being executed.</param>
    public delegate void FinishExecuteActionEvent(Agent agent, Action action, ExecutionStatus status, Dictionary<string, object> parameters);
}
