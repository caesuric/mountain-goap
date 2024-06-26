// <copyright file="ComparisonOperator.cs" company="Chris Muller">
// Copyright (c) Chris Muller. All rights reserved.
// </copyright>

namespace MountainGoap.Async {
    /// <summary>
    /// List of operators that can be used for comparison.
    /// </summary>
    public enum ComparisonOperator {
        /// <summary>
        /// Undefined comparison operator (will not do anything).
        /// </summary>
        Undefined = 0,

        /// <summary>
        /// Equality (==) operator.
        /// </summary>
        Equals = 1,

        /// <summary>
        /// Less than (&lt;) operator.
        /// </summary>
        LessThan = 2,

        /// <summary>
        /// Less than or equals (&lt;=) operator.
        /// </summary>
        LessThanOrEquals = 3,

        /// <summary>
        /// Greater than (>) operator).
        /// </summary>
        GreaterThan = 4,

        /// <summary>
        /// Greater than or equals (>=) operator.
        /// </summary>
        GreaterThanOrEquals = 5
    }
}