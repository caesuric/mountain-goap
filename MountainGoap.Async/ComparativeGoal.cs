// <copyright file="ComparativeGoal.cs" company="Chris Muller">
// Copyright (c) Chris Muller. All rights reserved.
// </copyright>

namespace MountainGoap.Async {
    using System.Collections.Generic;

    /// <summary>
    /// Represents a goal to be achieved for an agent.
    /// </summary>
    public class ComparativeGoal : BaseGoal {
        /// <summary>
        /// Desired state for the comparative goal.
        /// </summary>
        internal readonly Dictionary<string, ComparisonValuePair> DesiredState;

        /// <summary>
        /// Initializes a new instance of the <see cref="ComparativeGoal"/> class.
        /// </summary>
        /// <param name="name">Name of the goal.</param>
        /// <param name="weight">Weight to give the goal.</param>
        /// <param name="desiredState">Desired state for the comparative goal.</param>
        public ComparativeGoal(string? name = null, float weight = 1f, Dictionary<string, ComparisonValuePair>? desiredState = null)
            : base(name, weight) {
            DesiredState = desiredState ?? new();
        }
    }
}
