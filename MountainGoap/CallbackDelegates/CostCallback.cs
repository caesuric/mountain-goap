// <copyright file="CostCallback.cs" company="Chris Muller">
// Copyright (c) Chris Muller. All rights reserved.
// </copyright>

namespace MountainGoap {
    /// <summary>
    /// Delegate type for a callback that defines the cost of an action.
    /// </summary>
    /// <param name="action">Action being executed.</param>
    /// <param name="currentState">State as it will be when cost is relevant.</param>
    /// <returns>Cost of the action.</returns>
    public delegate float CostCallback(Action action, Dictionary<string, object?> currentState);
}
