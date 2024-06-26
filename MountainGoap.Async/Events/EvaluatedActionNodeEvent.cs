// <copyright file="EvaluatedActionNodeEvent.cs" company="Chris Muller">
// Copyright (c) Chris Muller. All rights reserved.
// </copyright>

namespace MountainGoap.Async {
    using System.Collections.Concurrent;

    /// <summary>
    /// Delegate type for a listener to the event that fires when an agent is evaluating a path for a potential action plan.
    /// </summary>
    /// <param name="node">Node being evaluated.</param>
    /// <param name="nodes">All nodes in the plan being evaluated.</param>
    /// <returns>Async Task.</returns>
    public delegate Task EvaluatedActionNodeEvent(ActionNode node, ConcurrentDictionary<ActionNode, ActionNode> nodes);
}
