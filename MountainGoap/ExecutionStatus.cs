// <copyright file="ExecutionStatus.cs" company="Chris Muller">
// Copyright (c) Chris Muller. All rights reserved.
// </copyright>

namespace MountainGoap {
    /// <summary>
    /// Possible execution status for an action.
    /// </summary>
    public enum ExecutionStatus {
        /// <summary>
        /// Indicates that the action is not currently executing.
        /// </summary>
        NotYetExecuted = 1,

        /// <summary>
        /// Indicates that the action is currently executing.
        /// </summary>
        Executing = 2,

        /// <summary>
        /// Indicates that the action has succeeded.
        /// </summary>
        Succeeded = 3,

        /// <summary>
        /// Indicates that the action has failed.
        /// </summary>
        Failed = 4,

        /// <summary>
        /// Indicates that the action is not possible due to preconditions.
        /// </summary>
        NotPossible = 5
    }
}
