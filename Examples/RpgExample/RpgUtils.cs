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
            var distance = Distance(pos1, pos2);
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

        /// <summary>
        /// Permutation selector to grab all enemies.
        /// </summary>
        /// <param name="state">State for the agent running the selector.</param>
        /// <returns>List of all enemies on the map.</returns>
        internal static List<object> EnemyPermutations(Dictionary<string, object?> state) {
            var enemies = new List<object>();
            if (state["agents"] is not List<Agent> agents || state["faction"] is not string faction) return enemies;
            return agents.Where((agent) => agent.State["faction"] is string faction2 && faction2 != faction).ToList<object>();
        }

        /// <summary>
        /// Permutation selector to grab all food positions.
        /// </summary>
        /// <param name="state">State for the agent running the selector.</param>
        /// <returns>List of all food positions on the map.</returns>
        internal static List<object> FoodPermutations(Dictionary<string, object?> state) {
            var foodPositions = new List<object>();
            if (state["foodPositions"] is not List<Vector2> sourcePositions) return foodPositions;
            foreach (var position in sourcePositions) foodPositions.Add(position);
            return foodPositions;
        }

        /// <summary>
        /// Gets a list of all possible starting positions for a move action.
        /// </summary>
        /// <param name="state">Current agent state.</param>
        /// <returns>List of all possible starting positions for a move action.</returns>
        internal static List<object> StartingPositionPermutations(Dictionary<string, object?> state) {
            var startingPositions = new List<object>();
            if (state["position"] is not Vector2 position) return startingPositions;
            startingPositions.Add(position);
            return startingPositions;
        }

        /// <summary>
        /// Gets the cost of moving to an enemy.
        /// </summary>
        /// <param name="action">Action for which cost is being calculated.</param>
        /// <param name="state">State as it will be when cost is relevant.</param>
        /// <returns>The cost of the action.</returns>
#pragma warning disable IDE0060 // Remove unused parameter
        internal static float GoToEnemyCost(Action action, Dictionary<string, object?> state) {
            if (action.GetParameter("startingPosition") is not Vector2 startingPosition || action.GetParameter("target") is not Agent target) return float.MaxValue;
            if (target.State["position"] is not Vector2 targetPosition) return float.MaxValue;
            return Distance(startingPosition, targetPosition);
        }
#pragma warning restore IDE0060 // Remove unused parameter

        /// <summary>
        /// Gets the cost of moving to food.
        /// </summary>
        /// <param name="action">Action for which the cost is being calculated.</param>
        /// /// <param name="state">State as it will be when cost is relevant.</param>
        /// <returns>The cost of the action.</returns>
#pragma warning disable IDE0060 // Remove unused parameter
        internal static float GoToFoodCost(Action action, Dictionary<string, object?> state) {
            if (action.GetParameter("startingPosition") is not Vector2 startingPosition || action.GetParameter("target") is not Vector2 targetPosition) return float.MaxValue;
            return Distance(startingPosition, targetPosition);
        }
#pragma warning restore IDE0060 // Remove unused parameter

        private static float Distance(Vector2 pos1, Vector2 pos2) {
            return (float)Math.Sqrt(Math.Pow(Math.Abs(pos2.X - pos1.X), 2) + Math.Pow(Math.Abs(pos2.Y - pos1.Y), 2));
        }
    }
}
