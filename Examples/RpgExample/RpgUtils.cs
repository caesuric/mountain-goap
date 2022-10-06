// <copyright file="RpgUtils.cs" company="Chris Muller">
// Copyright (c) Chris Muller. All rights reserved.
// </copyright>

namespace Examples {
    using System.Numerics;
    using MountainGoap;

    /// <summary>
    /// Utility classes for the RPG example.
    /// </summary>
    internal static class RpgUtils {
        /// <summary>
        /// Checks if two positions are within a certain distance of one another.
        /// </summary>
        /// <param name="pos1">Position 1.</param>
        /// <param name="pos2">Position 2.</param>
        /// <param name="maxDistance">Maximum acceptable distance.</param>
        /// <returns>True if the positions are within the given distance, otherwise false.</returns>
        internal static bool InDistance(Vector2 pos1, Vector2 pos2, float maxDistance) {
            var distance = Math.Sqrt(Math.Pow(Math.Abs(pos2.X - pos1.X), 2) + Math.Pow(Math.Abs(pos2.Y - pos1.Y), 2));
            if (distance <= maxDistance) return true;
            return false;
        }

        /// <summary>
        /// Gets an enemy within a given range of a source agent.
        /// </summary>
        /// <param name="source">Agent around which to perform the search.</param>
        /// <param name="agents">List of all agents.</param>
        /// <param name="distance">Distance radius to be checked.</param>
        /// <returns>An agent in range, or <see cref="null"/> if none exist.</returns>
        internal static Agent? GetEnemyInRange(Agent source, List<Agent> agents, float distance) {
            foreach (var agent in agents) {
                if (agent == source) continue;
                if (source.State["position"] is Vector2 pos1 && agent.State["position"] is Vector2 pos2 && InDistance(pos1, pos2, distance) && source.State["faction"] != agent.State["faction"]) return agent;
            }
            return null;
        }

        /// <summary>
        /// Moves a position towards another position one space and returns the result.
        /// </summary>
        /// <param name="pos1">Source position.</param>
        /// <param name="pos2">Destination position.</param>
        /// <returns>Interpolated position from source to destination, moved by one space.</returns>
        internal static Vector2 MoveTowardsOtherPosition(Vector2 pos1, Vector2 pos2) {
            var xSign = Math.Sign(pos2.X - pos1.X);
            var ySign = Math.Sign(pos2.Y - pos1.Y);
            if (xSign != 0) pos1.X += xSign;
            else pos1.Y += ySign;
            return pos1;
        }
    }
}
