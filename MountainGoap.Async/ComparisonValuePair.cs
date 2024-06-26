// <copyright file="ComparisonValuePair.cs" company="Chris Muller">
// Copyright (c) Chris Muller. All rights reserved.
// </copyright>

namespace MountainGoap.Async {
    /// <summary>
    /// List of operators that can be used for comparison.
    /// </summary>
    public class ComparisonValuePair {
        /// <summary>
        /// Gets or sets the value to be compared against.
        /// </summary>
        public object? Value { get; set; } = null;

        /// <summary>
        /// Gets or sets the operator to be used for comparison.
        /// </summary>
        public ComparisonOperator Operator { get; set; } = ComparisonOperator.Undefined;
    }
}