// <copyright file="ComparisonValuePair.cs" company="Chris Muller">
// Copyright (c) Chris Muller. All rights reserved.
// </copyright>

namespace MountainGoap {
    /// <summary>
    /// List of operators that can be used for comparison.
    /// </summary>
    public class ComparisonValuePair {
        /// <summary>
        /// Value to be compared against.
        /// </summary>
        public object? Value = null;

        /// <summary>
        /// Operator to be used for comparison.
        /// </summary>
        public ComparisonOperator Operator = ComparisonOperator.Undefined;
    }
}