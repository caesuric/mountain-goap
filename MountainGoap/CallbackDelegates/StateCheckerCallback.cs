// <copyright file="StateCheckerCallback.cs" company="Chris Muller">
// Copyright (c) Chris Muller. All rights reserved.
// </copyright>

namespace MountainGoap {
    /// <summary>
    /// Delegate type for a callback that checks state before action execution or evaluation (the latter during planning).
    /// </summary>
    /// <param name="action">Action being executed or evaluated.</param>
    /// <param name="currentState">State as it will be when the action is executed or evaluated.</param>
    /// <returns>True if the state is okay for executing the action, otherwise false.</returns>
    public delegate bool StateCheckerCallback(Action action, Dictionary<string, object?> currentState);
}
