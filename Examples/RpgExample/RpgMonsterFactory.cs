// <copyright file="RpgMonsterFactory.cs" company="Chris Muller">
// Copyright (c) Chris Muller. All rights reserved.
// </copyright>

namespace Examples {
    using System.Numerics;
    using MountainGoap;

    /// <summary>
    /// Class for generating an RPG monster.
    /// </summary>
    internal static class RpgMonsterFactory {
        private static readonly Random Rng = new();

        /// <summary>
        /// Returns an RPG monster agent.
        /// </summary>
        /// <param name="agents">List of agents included in the world state.</param>
        /// <param name="foodPositions">List of positions for food in the world state.</param>
        /// <returns>An RPG character agent.</returns>
        internal static Agent Create(List<Agent> agents, List<Vector2> foodPositions) {
            var agent = RpgCharacterFactory.Create(agents);
            Goal eatFood = new(
                weight: 0.1f,
                desiredState: new() {
                    { "eatingFood", true }
                }
            );
            Sensor seeFoodSensor = new(SeeFoodSensorHandler);
            Sensor foodProximitySensor = new(FoodProximitySensorHandler);
            Action lookForFood = new(
                executor: LookForFoodExecutor,
                preconditions: new() {
                    { "canSeeFood", false },
                    { "canSeeEnemies", false }
                },
                postconditions: new() {
                    { "canSeeFood", true }
                }
            );
            Action goToFood = new(
                executor: GoToFoodExecutor,
                preconditions: new() {
                    { "canSeeFood", true },
                    { "canSeeEnemies", false }
                },
                postconditions: new() {
                    { "nearFood", true }
                }
            );
            Action eat = new(
                executor: EatExecutor,
                preconditions: new() {
                    { "nearFood", true },
                    { "canSeeEnemies", false }
                },
                postconditions: new() {
                    { "eatingFood", true }
                }
            );
            agent.State["canSeeFood"] = false;
            agent.State["nearFood"] = false;
            agent.State["eatingFood"] = false;
            agent.State["foodPositions"] = foodPositions;
            agent.State["hp"] = 2;
            agent.Goals.Add(eatFood);
            agent.Sensors.Add(seeFoodSensor);
            agent.Sensors.Add(foodProximitySensor);
            agent.Actions.Add(goToFood);
            agent.Actions.Add(lookForFood);
            agent.Actions.Add(eat);
            return agent;
        }

        private static bool InDistance(Vector2 pos1, Vector2 pos2, float maxDistance) {
            var distance = Math.Sqrt(Math.Pow(Math.Abs(pos2.X - pos1.X), 2) + Math.Pow(Math.Abs(pos2.Y - pos1.Y), 2));
            if (distance <= maxDistance) return true;
            return false;
        }

        private static void SeeFoodSensorHandler(Agent agent) {
            if (agent.State["foodPositions"] is List<Vector2> foodPositions) {
                foreach (var position in foodPositions) {
                    if (agent.State["position"] is Vector2 agentPosition && InDistance(agentPosition, position, 5f)) {
                        agent.State["canSeeFood"] = true;
                        return;
                    }
                }
            }
            agent.State["canSeeFood"] = false;
            agent.State["eatingFood"] = false;
        }

        private static void FoodProximitySensorHandler(Agent agent) {
            if (agent.State["foodPositions"] is List<Vector2> foodPositions) {
                foreach (var position in foodPositions) {
                    if (agent.State["position"] is Vector2 agentPosition && InDistance(agentPosition, position, 1f)) {
                        agent.State["nearFood"] = true;
                        return;
                    }
                }
            }
            agent.State["nearFood"] = false;
            agent.State["eatingFood"] = false;
        }

        private static ExecutionStatus LookForFoodExecutor(Agent agent, Action action) {
            if (agent.State["position"] is Vector2 position) {
                position.X += Rng.Next(-1, 2);
                position.X = Math.Clamp(position.X, 0, RpgExample.MaxX - 1);
                position.Y += Rng.Next(-1, 2);
                position.Y = Math.Clamp(position.Y, 0, RpgExample.MaxY - 1);
                agent.State["position"] = position;
            }
            if (agent.State["canSeeFood"] is bool canSeeFood && canSeeFood) return ExecutionStatus.Succeeded;
            return ExecutionStatus.Failed;
        }

        private static ExecutionStatus GoToFoodExecutor(Agent agent, Action action) {
            if (agent.State["foodPositions"] is List<Vector2> foodPositions && agent.State["position"] is Vector2 position) {
                foreach (var foodPosition in foodPositions) {
                    if (InDistance(position, foodPosition, 5f)) {
                        var xSign = Math.Sign(foodPosition.X - position.X);
                        var ySign = Math.Sign(foodPosition.Y - position.Y);
                        if (xSign != 0) position.X += xSign;
                        else position.Y += ySign;
                        agent.State["position"] = position;
                        if (InDistance(position, foodPosition, 1f)) return ExecutionStatus.Succeeded;
                        return ExecutionStatus.Failed;
                    }
                }
            }
            return ExecutionStatus.Failed;
        }

        private static ExecutionStatus EatExecutor(Agent agent, Action action) {
            if (agent.State["foodPositions"] is List<Vector2> foodPositions && agent.State["position"] is Vector2 position) {
                foreach (var foodPosition in foodPositions) {
                    if (InDistance(position, foodPosition, 1f)) {
                        foodPositions.Remove(foodPosition);
                        return ExecutionStatus.Succeeded;
                    }
                }
            }
            return ExecutionStatus.Failed;
        }
    }
}
