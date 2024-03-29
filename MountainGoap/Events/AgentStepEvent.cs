﻿// <copyright file="AgentStepEvent.cs" company="Chris Muller">
// Copyright (c) Chris Muller. All rights reserved.
// </copyright>

namespace MountainGoap {
    /// <summary>
    /// Delegate type for a listener to the event that fires when an agent executes a step of work.
    /// </summary>
    /// <param name="agent">Agent executing the step of work.</param>
    public delegate void AgentStepEvent(Agent agent);
}
