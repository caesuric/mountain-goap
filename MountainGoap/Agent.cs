// <copyright file="Agent.cs" company="Chris Muller">
// Copyright (c) Chris Muller. All rights reserved.
// </copyright>

namespace MountainGoap {
    public class Agent {
        /// <summary>
        /// The current world state from the agent perspective.
        /// </summary>
        internal Dictionary<string, object> State = new ();
    }
}