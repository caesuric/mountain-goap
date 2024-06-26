﻿// <copyright file="StateMutatorCallback.cs" company="Chris Muller">
// Copyright (c) Chris Muller. All rights reserved.
// </copyright>

namespace MountainGoap.Async {
    using System.Collections.Concurrent;

    /// <summary>
    /// Delegate type for a callback that mutates state following action execution or evaluation (the latter during planning).
    /// </summary>
    /// <param name="action">Action being executed or evaluated.</param>
    /// <param name="currentState">State as it will be when the action is executed or evaluated.</param>
    /// <returns>Async Task.</returns>
    public delegate Task StateMutatorCallback(Action action, ConcurrentDictionary<string, object?> currentState);
}