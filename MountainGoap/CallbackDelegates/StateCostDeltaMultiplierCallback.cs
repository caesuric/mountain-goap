// <copyright file="StateMutatorCallback.cs" company="Chris Muller">
// Copyright (c) Chris Muller. All rights reserved.
// </copyright>

namespace MountainGoap {
    /// <summary>
    /// Delegate type for a callback that provides multiplier for delta value of the respective key to obtain delta cost to use with ExtremeGoal and ComparativeGoal.
    /// </summary>
    /// <param name="action">Action being executed or evaluated.</param>
    /// <param name="stateKey">Key to provide multiplier for</param>
    /// <returns>Multiplier for the delta value to get delta cost</returns>
    public delegate float StateCostDeltaMultiplierCallback(Action? action, string stateKey);
}
