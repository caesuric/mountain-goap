// <copyright file="CostCallback.cs" company="Chris Muller">
// Copyright (c) Chris Muller. All rights reserved.
// </copyright>

namespace MountainGoap.Async {
    using System.Collections.Concurrent;

    /// <summary>
    /// Delegate type for a callback that defines the cost of an action.
    /// </summary>
    /// <param name="action">Action being executed.</param>
    /// <param name="currentState">State as it will be when cost is relevant.</param>
    /// <returns>Cost of the action.</returns>
    public delegate Task<float> CostCallback(Action action, ConcurrentDictionary<string, object?> currentState);
}
