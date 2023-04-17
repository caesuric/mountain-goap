// <copyright file="BeginExecuteActionEvent.cs" company="Chris Muller">
// Copyright (c) Chris Muller. All rights reserved.
// </copyright>

namespace MountainGoap {
    using System.Collections.Generic;

    /// <summary>
    /// Delegate type for a listener to the event that fires when an action begins executing.
    /// </summary>
    /// <param name="agent">Agent executing the action.</param>
    /// <param name="action">Action being executed.</param>
    /// <param name="parameters">Parameters to the action being executed.</param>
    public delegate void BeginExecuteActionEvent(Agent agent, Action action, Dictionary<string, object> parameters);
}
