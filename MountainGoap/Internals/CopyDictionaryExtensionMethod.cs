// <copyright file="CopyDictionaryExtensionMethod.cs" company="Chris Muller">
// Copyright (c) Chris Muller. All rights reserved.
// </copyright>

using System.Collections.Generic;
using System.Linq;

namespace MountainGoap {
    /// <summary>
    /// Extension method to copy a dictionary of strings and objects.
    /// </summary>
    internal static class CopyDictionaryExtensionMethod {
        /// <summary>
        /// Copies the dictionary to a shallow clone.
        /// </summary>
        /// <param name="dictionary">Dictionary to be copied.</param>
        /// <returns>A shallow copy of the dictionary.</returns>
        internal static Dictionary<string, object> Copy(this Dictionary<string, object> dictionary) {
            return dictionary.ToDictionary(entry => entry.Key, entry => entry.Value);
        }
    }
}