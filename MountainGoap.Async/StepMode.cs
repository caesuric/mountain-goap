// <copyright file="StepMode.cs" company="Chris Muller">
// Copyright (c) Chris Muller. All rights reserved.
// </copyright>

namespace MountainGoap.Async {
    /// <summary>
    /// Different modes with which MountainGoap.Async can execute an agent step.
    /// </summary>
    public enum StepMode {
        /// <summary>
        /// Default step mode. Runs async, doesn't necessitate taking action.
        /// </summary>
        Default = 1,

        /// <summary>
        /// Turn-based step mode, plans synchronously, executes at least one action if possible.
        /// </summary>
        OneAction = 2,

        /// <summary>
        /// Turn-based step mode, plans synchronously, executes all actions in planned action sequence.
        /// </summary>
        AllActions = 3
    }
}
