// <copyright file="BaseGoal.cs" company="Chris Muller">
// Copyright (c) Chris Muller. All rights reserved.
// </copyright>

namespace MountainGoap {
    using System;

    /// <summary>
    /// Represents an abstract class for a goal to be achieved for an agent.
    /// </summary>
    public abstract class BaseGoal {
        /// <summary>
        /// Name of the goal.
        /// </summary>
        public readonly string Name;

        /// <summary>
        /// Weight to give the goal.
        /// </summary>
        internal readonly float Weight;

        /// <summary>
        /// Initializes a new instance of the <see cref="BaseGoal"/> class.
        /// </summary>
        /// <param name="name">Name of the goal.</param>
        /// <param name="weight">Weight to give the goal.</param>
        protected BaseGoal(string? name = null, float weight = 1f) {
            Name = name ?? $"Goal {Guid.NewGuid()}";
            Weight = weight;
        }
    }
}
