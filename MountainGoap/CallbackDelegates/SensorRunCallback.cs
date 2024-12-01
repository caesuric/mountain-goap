// <copyright file="SensorRunCallback.cs" company="Chris Muller">
// Copyright (c) Chris Muller. All rights reserved.
// </copyright>

namespace MountainGoap {
    /// <summary>
    /// Delegate type for a callback that runs a sensor during a game loop.
    /// </summary>
    /// <param name="agent">Agent using the sensor.</param>
    public delegate void SensorRunCallback(Agent agent);

    /// <summary>
    /// Delegate type for a callback that runs a sensor as a task during a game loop.
    /// </summary>
    /// <param name="agent">Agent using the sensor.</param>
    /// <returns>A Task for an asynchronous operation.</returns>
    public delegate Task SensorRunAsTaskCallback(Agent agent);
}
