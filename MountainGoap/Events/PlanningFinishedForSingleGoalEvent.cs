﻿// <copyright file="PlanningFinishedForSingleGoalEvent.cs" company="Chris Muller">
// Copyright (c) Chris Muller. All rights reserved.
// </copyright>

namespace MountainGoap {
    /// <summary>
    /// Delegate type for a listener to the event that fires when an agent finishes planning for a single goal.
    /// </summary>
    /// <param name="agent">Agent doing the planning.</param>
    /// <param name="goal">Goal for which planning was finished.</param>
    /// <param name="utility">Calculated utility of the plan.</param>
    public delegate void PlanningFinishedForSingleGoalEvent(Agent agent, BaseGoal goal, float utility);
}
