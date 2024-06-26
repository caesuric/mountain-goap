// <copyright file="PermutationSelectorCallback.cs" company="Chris Muller">
// Copyright (c) Chris Muller. All rights reserved.
// </copyright>

namespace MountainGoap {
    using System.Collections.Concurrent;
    using System.Collections.Generic;

    /// <summary>
    /// Delegate type for a callback that defines a list of all possible parameter states for the given state.
    /// </summary>
    /// <param name="state">Current world state.</param>
    /// <returns>A list with each parameter set to be tried for the action.</returns>
    public delegate List<object> PermutationSelectorCallback(ConcurrentDictionary<string, object?> state);
}
