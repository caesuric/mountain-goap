// <copyright file="Sensor.cs" company="Chris Muller">
// Copyright (c) Chris Muller. All rights reserved.
// </copyright>

namespace MountainGoap {
    using System;
    using System.Reflection;

    /// <summary>
    /// Sensor for getting information about world state.
    /// Supports async.
    /// </summary>
    public class SensorAsync {
        /// <summary>
        /// Name of the sensor.
        /// </summary>
        public readonly string Name;

        /// <summary>
        /// Callback to be executed when the sensor runs.
        /// </summary>
        private readonly SensorRunAsyncCallback runCallback;

        /// <summary>
        /// Initializes a new instance of the <see cref="Sensor"/> class.
        /// </summary>
        /// <param name="runCallback">Callback to be executed when the sensor runs.</param>
        /// <param name="name">Name of the sensor.</param>
        public SensorAsync(SensorRunAsyncCallback runCallback, string? name = null) {
            Name = name ?? $"Sensor {Guid.NewGuid()} ({runCallback.GetMethodInfo().Name})";
            this.runCallback = runCallback;
        }

        /// <summary>
        /// Event that triggers when a sensor runs.
        /// </summary>
        public static event SensorRunAsyncEvent OnSensorRun = (agent, sensor) => Task.CompletedTask;

        /// <summary>
        /// Runs the sensor during a game loop.
        /// </summary>
        /// <param name="agent">Agent for which the sensor is being run.</param>
        public async Task RunAsync(AgentAsync agent) {
            await OnSensorRun(agent, this);
            await runCallback(agent);
        }
    }
}