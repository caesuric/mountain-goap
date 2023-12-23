// <copyright file="Utils.cs" company="Chris Muller">
// Copyright (c) Chris Muller. All rights reserved.
// </copyright>

namespace MountainGoap {
    /// <summary>
    /// Utilities for the MountainGoap library.
    /// </summary>
    internal static class Utils {
        /// <summary>
        /// Indicates whether a is lower than b.
        /// </summary>
        /// <param name="a">First element to be compared.</param>
        /// <param name="b">Second element to be compared.</param>
        /// <returns>True if lower, false otherwise.</returns>
        internal static bool IsLowerThan(object a, object b) {
            if (a == null || b == null) return false;
            if (a is int intA && b is int intB) return intA < intB;
            if (a is long longA && b is long longB) return longA < longB;
            if (a is float floatA && b is float floatB) return floatA < floatB;
            if (a is double doubleA && b is double doubleB) return doubleA < doubleB;
            if (a is decimal decimalA && b is decimal decimalB) return decimalA < decimalB;
            if (a is DateTime dateTimeA && b is DateTime dateTimeB) return dateTimeA < dateTimeB;
            return false;
        }

        /// <summary>
        /// Indicates whether a is higher than b.
        /// </summary>
        /// <param name="a">First element to be compared.</param>
        /// <param name="b">Second element to be compared.</param>
        /// <returns>True if higher, false otherwise.</returns>
        internal static bool IsHigherThan(object a, object b) {
            if (a == null || b == null) return false;
            if (a is int intA && b is int intB) return intA > intB;
            if (a is long longA && b is long longB) return longA > longB;
            if (a is float floatA && b is float floatB) return floatA > floatB;
            if (a is double doubleA && b is double doubleB) return doubleA > doubleB;
            if (a is decimal decimalA && b is decimal decimalB) return decimalA > decimalB;
            if (a is DateTime dateTimeA && b is DateTime dateTimeB) return dateTimeA > dateTimeB;
            return false;
        }

        /// <summary>
        /// Indicates whether a is lower than or equal to b.
        /// </summary>
        /// <param name="a">First element to be compared.</param>
        /// <param name="b">Second element to be compared.</param>
        /// <returns>True if lower or equal, false otherwise.</returns>
        internal static bool IsLowerThanOrEquals(object a, object b) {
            if (a == null || b == null) return false;
            if (a is int intA && b is int intB) return intA <= intB;
            if (a is long longA && b is long longB) return longA <= longB;
            if (a is float floatA && b is float floatB) return floatA <= floatB;
            if (a is double doubleA && b is double doubleB) return doubleA <= doubleB;
            if (a is decimal decimalA && b is decimal decimalB) return decimalA <= decimalB;
            if (a is DateTime dateTimeA && b is DateTime dateTimeB) return dateTimeA <= dateTimeB;
            return false;
        }

        /// <summary>
        /// Indicates whether a is higher than or equal to b.
        /// </summary>
        /// <param name="a">First element to be compared.</param>
        /// <param name="b">Second element to be compared.</param>
        /// <returns>True if higher or equal, false otherwise.</returns>
        internal static bool IsHigherThanOrEquals(object a, object b) {
            if (a == null || b == null) return false;
            if (a is int intA && b is int intB) return intA >= intB;
            if (a is long longA && b is long longB) return longA >= longB;
            if (a is float floatA && b is float floatB) return floatA >= floatB;
            if (a is double doubleA && b is double doubleB) return doubleA >= doubleB;
            if (a is decimal decimalA && b is decimal decimalB) return decimalA >= decimalB;
            if (a is DateTime dateTimeA && b is DateTime dateTimeB) return dateTimeA >= dateTimeB;
            return false;
        }

        /// <summary>
        /// Indicates whether or not a goal is met by an action node.
        /// </summary>
        /// <param name="goal">Goal to be met.</param>
        /// <param name="actionNode">Action node being tested.</param>
        /// <param name="current">Prior node in the action chain.</param>
        /// <returns>True if the goal is met, otherwise false.</returns>
        internal static bool MeetsGoal(BaseGoal goal, ActionNode actionNode, ActionNode current) {
            if (goal is Goal normalGoal) {
#pragma warning disable S3267 // Loops should be simplified with "LINQ" expressions
                foreach (var kvp in normalGoal.DesiredState) {
                    if (!actionNode.State.ContainsKey(kvp.Key)) return false;
                    else if (actionNode.State[kvp.Key] == null && actionNode.State[kvp.Key] != normalGoal.DesiredState[kvp.Key]) return false;
                    else if (actionNode.State[kvp.Key] is object obj && obj != null && !obj.Equals(normalGoal.DesiredState[kvp.Key])) return false;
                }
#pragma warning restore S3267 // Loops should be simplified with "LINQ" expressions
            }
            else if (goal is ExtremeGoal extremeGoal) {
                if (actionNode.Action == null) return false;
                foreach (var kvp in extremeGoal.DesiredState) {
                    if (!actionNode.State.ContainsKey(kvp.Key)) return false;
                    else if (!current.State.ContainsKey(kvp.Key)) return false;
                    else if (kvp.Value && actionNode.State[kvp.Key] is object a && current.State[kvp.Key] is object b && IsLowerThanOrEquals(a, b)) return false;
                    else if (!kvp.Value && actionNode.State[kvp.Key] is object a2 && current.State[kvp.Key] is object b2 && IsHigherThanOrEquals(a2, b2)) return false;
                }
            }
            else if (goal is ComparativeGoal comparativeGoal) {
                if (actionNode.Action == null) return false;
                foreach (var kvp in comparativeGoal.DesiredState) {
                    if (!actionNode.State.ContainsKey(kvp.Key)) return false;
                    else if (!current.State.ContainsKey(kvp.Key)) return false;
                    else if (kvp.Value.Operator == ComparisonOperator.Undefined) return false;
                    else if (kvp.Value.Operator == ComparisonOperator.Equals && actionNode.State[kvp.Key] is object obj && !obj.Equals(comparativeGoal.DesiredState[kvp.Key].Value)) return false;
                    else if (kvp.Value.Operator == ComparisonOperator.LessThan && actionNode.State[kvp.Key] is object a && comparativeGoal.DesiredState[kvp.Key].Value is object b && !IsLowerThan(a, b)) return false;
                    else if (kvp.Value.Operator == ComparisonOperator.GreaterThan && actionNode.State[kvp.Key] is object a2 && comparativeGoal.DesiredState[kvp.Key].Value is object b2 && !IsHigherThan(a2, b2)) return false;
                    else if (kvp.Value.Operator == ComparisonOperator.LessThanOrEquals && actionNode.State[kvp.Key] is object a3 && comparativeGoal.DesiredState[kvp.Key].Value is object b3 && !IsLowerThanOrEquals(a3, b3)) return false;
                    else if (kvp.Value.Operator == ComparisonOperator.GreaterThanOrEquals && actionNode.State[kvp.Key] is object a4 && comparativeGoal.DesiredState[kvp.Key].Value is object b4 && !IsHigherThanOrEquals(a4, b4)) return false;
                }
            }
            return true;
        }
    }
}
