// <copyright file="RpgCharacterFactory.cs" company="Chris Muller">
// Copyright (c) Chris Muller. All rights reserved.
// </copyright>

namespace Examples {
    using System.Numerics;
    using MountainGoap;

    /// <summary>
    /// Class for generating an RPG character.
    /// </summary>
    internal static class RpgCharacterFactory {
        /// <summary>
        /// Returns an RPG character agent.
        /// </summary>
        /// <param name="agents">List of agents included in the world state.</param>
        /// <returns>An RPG character agent.</returns>
        internal static Agent Create(List<Agent> agents) {
            Goal removeEnemies = new(
                weight: 1f,
                desiredState: new() {
                    { "canSeeEnemies", false }
                }
            );
            Sensor seeEnemiesSensor = new(SeeEnemiesSensorHandler);
            Sensor enemyProximitySensor = new(EnemyProximitySensorHandler);
            Action goToNearestEnemy = new(
                executor: GoToNearestEnemyExecutor,
                preconditions: new() {
                    { "canSeeEnemies", true },
                    { "nearEnemy", false }
                },
                postconditions: new() {
                    { "nearEnemy", true }
                }
            );
            Action killNearbyEnemy = new(
                executor: KillNearbyEnemyExecutor,
                preconditions: new() {
                    { "nearEnemy", true }
                },
                postconditions: new() {
                   { "canSeeEnemies", false },
                   { "nearEnemy", false }
                }
            );

            Agent agent = new(
                state: new() {
                    { "canSeeEnemies", false },
                    { "nearEnemy", false },
                    { "hp", 10 },
                    { "position", new Vector2(10, 10) },
                    { "faction", "enemy" },
                    { "agents", agents }
                },
                goals: new() {
                    removeEnemies
                },
                sensors: new() {
                    seeEnemiesSensor
                },
                actions: new() {
                    goToNearestEnemy,
                    killNearbyEnemy
                }
            );
            return agent;
        }

        private static void SeeEnemiesSensorHandler(Agent agent) {
            if (agent.State["agents"] is List<Agent> agents) {
                foreach (var agent2 in agents) {
                    if (agent == agent2) continue;
                    if (agent.State["position"] is Vector2 pos1 && agent2.State["position"] is Vector2 pos2 && InDistance(pos1, pos2, 5f) && agent.State["faction"] != agent2.State["faction"]) {
                        agent.State["canSeeEnemies"] = true;
                        return;
                    }
                }
            }
            agent.State["canSeeEnemies"] = false;
        }

        private static bool InDistance(Vector2 pos1, Vector2 pos2, float maxDistance) {
            var distance = Math.Sqrt(Math.Pow(Math.Abs(pos2.X - pos1.X), 2) + Math.Pow(Math.Abs(pos2.Y - pos1.Y), 2));
            if (distance <= maxDistance) return true;
            return false;
        }

        private static void EnemyProximitySensorHandler(Agent agent) {
            if (agent.State["agents"] is List<Agent> agents) {
                foreach (var agent2 in agents) {
                    if (agent == agent2) continue;
                    if (agent.State["position"] is Vector2 pos1 && agent2.State["position"] is Vector2 pos2 && InDistance(pos1, pos2, 1f) && agent.State["faction"] != agent2.State["faction"]) {
                        agent.State["nearEnemy"] = true;
                        return;
                    }
                }
            }
        }

        private static ExecutionStatus KillNearbyEnemyExecutor(Agent agent, Action action) {
            if (agent.State["agents"] is List<Agent> agents) {
                foreach (var agent2 in agents) {
                    if (agent == agent2) continue;
                    if (agent.State["position"] is Vector2 pos1 && agent2.State["position"] is Vector2 pos2 && InDistance(pos1, pos2, 1f) && agent.State["faction"] != agent2.State["faction"] && agent2.State["hp"] is int hp) {
                        hp--;
                        agent2.State["hp"] = hp;
                        if (hp <= 0) return ExecutionStatus.Succeeded;
                        return ExecutionStatus.Failed;
                    }
                }
            }
            return ExecutionStatus.Failed;
        }

        private static ExecutionStatus GoToNearestEnemyExecutor(Agent agent, Action action) {
            if (agent.State["agents"] is List<Agent> agents) {
                foreach (var agent2 in agents) {
                    if (agent == agent2) continue;
                    if (agent.State["position"] is Vector2 pos1 && agent2.State["position"] is Vector2 pos2 && InDistance(pos1, pos2, 5f) && agent.State["faction"] != agent2.State["faction"]) {
                        var xSign = Math.Sign(pos2.X - pos1.X);
                        var ySign = Math.Sign(pos2.Y - pos1.Y);
                        if (xSign != 0) pos1.X += xSign;
                        else pos1.Y += ySign;
                        agent.State["position"] = pos1;
                        if (InDistance(pos1, pos2, 1f)) return ExecutionStatus.Succeeded;
                        return ExecutionStatus.Failed;
                    }
                }
            }
            return ExecutionStatus.Failed;
        }
    }
}
