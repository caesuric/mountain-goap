// <copyright file="ExtremeGoal.cs" company="Chris Muller">
// Copyright (c) Chris Muller. All rights reserved.
// </copyright>

namespace MountainGoap {
    /// <summary>
    /// Represents a goal requiring an extreme value to be achieved for an agent.
    /// </summary>
    public class ExtremeGoal : BaseGoal {
        /// <summary>
        /// Dictionary of states to be maximized or minimized. A value of true indicates to maximize the goal, a value of false indicates to minimize it.
        /// </summary>
        internal readonly Dictionary<string, bool> States;

        /// <summary>
        /// Initializes a new instance of the <see cref="ExtremeGoal"/> class.
        /// </summary>
        /// <param name="name">Name of the goal.</param>
        /// <param name="weight">Weight to give the goal.</param>
        /// <param name="states">States to be maximized or minimized.</param>
        public ExtremeGoal(string? name = null, float weight = 1f, Dictionary<string, bool>? states = null)
            : base(name, weight) {
            States = states ?? new();
        }
    }
}
