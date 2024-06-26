// <copyright file="SensorRunCallback.cs" company="Chris Muller">
// Copyright (c) Chris Muller. All rights reserved.
// </copyright>

namespace MountainGoap.Async {
    /// <summary>
    /// Delegate type for a callback that runs a sensor during a game loop.
    /// </summary>
    /// <param name="agent">Agent using the sensor.</param>
    /// <returns>Async Task.</returns>
    public delegate Task SensorRunCallback(Agent agent);
}
