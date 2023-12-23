// <copyright file="DictionaryExtensionMethods.cs" company="Chris Muller">
// Copyright (c) Chris Muller. All rights reserved.
// </copyright>

namespace MountainGoap {
    using System.Collections.Concurrent;
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// Extension method to copy a dictionary of strings and objects.
    /// </summary>
    public static class DictionaryExtensionMethods {
        /// <summary>
        /// Add functionality for ConcurrentDictionary.
        /// </summary>
        /// <param name="dictionary">Dictionary to which to add an item.</param>
        /// <param name="key">Key to add.</param>
        /// <param name="value">Value to add.</param>
        public static void Add(this ConcurrentDictionary<string, object?> dictionary, string key, object? value) {
            dictionary.TryAdd(key, value);
        }

        /// <summary>
        /// Copies the dictionary to a shallow clone.
        /// </summary>
        /// <param name="dictionary">Dictionary to be copied.</param>
        /// <returns>A shallow copy of the dictionary.</returns>
        internal static Dictionary<string, object?> Copy(this Dictionary<string, object?> dictionary) {
            return dictionary.ToDictionary(entry => entry.Key, entry => entry.Value);
        }

        /// <summary>
        /// Copies the concurrent dictionary to a shallow clone.
        /// </summary>
        /// <param name="dictionary">Dictionary to be copied.</param>
        /// <returns>A shallow copy of the dictionary.</returns>
        internal static ConcurrentDictionary<string, object?> Copy(this ConcurrentDictionary<string, object?> dictionary) {
            return new ConcurrentDictionary<string, object?>(dictionary);
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