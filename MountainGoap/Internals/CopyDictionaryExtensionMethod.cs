// <copyright file="CopyDictionaryExtensionMethod.cs" company="Chris Muller">
// Copyright (c) Chris Muller. All rights reserved.
// </copyright>

namespace MountainGoap {
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// Extension method to copy a dictionary of strings and objects.
    /// </summary>
    internal static class CopyDictionaryExtensionMethod {
        /// <summary>
        /// Copies the dictionary to a shallow clone.
        /// </summary>
        /// <param name="dictionary">Dictionary to be copied.</param>
        /// <returns>A shallow copy of the dictionary.</returns>
        internal static Dictionary<string, object?> Copy(this Dictionary<string, object?> dictionary) {
            return dictionary.ToDictionary(entry => entry.Key, entry => entry.Value);
        }

        /// <summary>
        /// Copies the dictionary to a shallow clone.
        /// </summary>
        /// <param name="dictionary">Dictionary to be copied.</param>
        /// <returns>A shallow copy of the dictionary.</returns>
        internal static Dictionary<string, ComparisonValuePair> Copy(this Dictionary<string, ComparisonValuePair> dictionary) {
            return dictionary.ToDictionary(entry => entry.Key, entry => entry.Value);
        }

        /// <summary>
        /// Copies the dictionary to a shallow clone.
        /// </summary>
        /// <param name="dictionary">Dictionary to be copied.</param>
        /// <returns>A shallow copy of the dictionary.</returns>
        internal static Dictionary<string, string> Copy(this Dictionary<string, string> dictionary) {
            return dictionary.ToDictionary(entry => entry.Key, entry => entry.Value);
        }

        /// <summary>
        /// Copies the dictionary to a shallow clone.
        /// </summary>
        /// <param name="dictionary">Dictionary to be copied.</param>
        /// <returns>A shallow copy of the dictionary.</returns>
        internal static Dictionary<string, object> CopyNonNullable(this Dictionary<string, object> dictionary) {
            return dictionary.ToDictionary(entry => entry.Key, entry => entry.Value);
        }
    }
}