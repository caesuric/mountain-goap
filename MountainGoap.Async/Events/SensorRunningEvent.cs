﻿// <copyright file="SensorRunningEvent.cs" company="Chris Muller">
// Copyright (c) Chris Muller. All rights reserved.
// </copyright>

namespace MountainGoap.Async {
    /// <summary>
    /// Delegate type for a listener to the event that fires when an agent sensor is about to run.
    /// </summary>
    /// <param name="agent">Agent running the sensor.</param>
    /// <param name="sensor">Sensor that is about to run.</param>
    /// <returns>Async Task.</returns>
    public delegate Task SensorRunEvent(Agent agent, Sensor sensor);
}
