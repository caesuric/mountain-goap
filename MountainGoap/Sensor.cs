// <copyright file="Sensor.cs" company="Chris Muller">
// Copyright (c) Chris Muller. All rights reserved.
// </copyright>

namespace MountainGoap {
    /// <summary>
    /// Sensor for getting information about world state.
    /// </summary>
    public class Sensor {
        /// <summary>
        /// Name of the sensor.
        /// </summary>
        public readonly string Name;

        /// <summary>
        /// Callback to be executed when the sensor runs.
        /// </summary>
        private readonly SensorRunCallback runCallback;

        /// <summary>
        /// Initializes a new instance of the <see cref="Sensor"/> class.
        /// </summary>
        /// <param name="runCallback">Callback to be executed when the sensor runs.</param>
        /// <param name="name">Name of the sensor.</param>
        public Sensor(SensorRunCallback runCallback, string? name = null) {
            Name = name ?? $"Sensor {Guid.NewGuid()}";
            this.runCallback = runCallback;
        }

        /// <summary>
        /// Runs the sensor during a game loop.
        /// </summary>
        /// <param name="agent">Agent for which the sensor is being run.</param>
        public void Run(Agent agent) {
            runCallback(agent);
        }
    }
}