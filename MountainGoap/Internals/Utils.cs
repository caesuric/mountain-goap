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
    }
}