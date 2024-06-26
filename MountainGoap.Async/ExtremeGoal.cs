// <copyright file="ExtremeGoal.cs" company="Chris Muller">
// Copyright (c) Chris Muller. All rights reserved.
// </copyright>

namespace MountainGoap.Async {
    using System.Collections.Generic;

    /// <summary>
    /// Represents a goal requiring an extreme value to be achieved for an agent.
    /// </summary>
    public class ExtremeGoal : BaseGoal {
        /// <summary>
        /// Dictionary of states to be maximized or minimized. A value of true indicates to maximize the goal, a value of false indicates to minimize it.
        /// </summary>
        internal readonly Dictionary<string, bool> DesiredState;

        /// <summary>
        /// Initializes a new instance of the <see cref="ExtremeGoal"/> class.
        /// </summary>
        /// <param name="name">Name of the goal.</param>
        /// <param name="weight">Weight to give the goal.</param>
        /// <param name="desiredState">States to be maximized or minimized.</param>
        public ExtremeGoal(string? name = null, float weight = 1f, Dictionary<string, bool>? desiredState = null)
            : base(name, weight) {
            DesiredState = desiredState ?? new();
        }
    }
}
