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
        /// <param name="name">Name of the character.</param>
        /// <returns>An RPG character agent.</returns>
        internal static Agent Create(List<Agent> agents, string name = "Player") {
            Goal removeEnemies = new(
                name: "Remove Enemies",
                weight: 1f,
                desiredState: new() {
                    { "canSeeEnemies", false }
                }
            );
            Sensor seeEnemiesSensor = new(SeeEnemiesSensorHandler, "Enemy Sight Sensor");
            Sensor enemyProximitySensor = new(EnemyProximitySensorHandler, "Enemy Proximity Sensor");
            Action goToNearestEnemy = new(
                name: "Go To Nearest Enemy",
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
                name: "Kill Nearby Enemy",
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
                name: name,
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
                var agent2 = RpgUtils.GetEnemyInRange(agent, agents, 5f);
                if (agent2 != null) agent.State["canSeeEnemies"] = true;
                else agent.State["canSeeEnemies"] = false;
            }
        }

        private static void EnemyProximitySensorHandler(Agent agent) {
            if (agent.State["agents"] is List<Agent> agents) {
                var agent2 = RpgUtils.GetEnemyInRange(agent, agents, 1f);
                if (agent2 != null) agent.State["nearEnemy"] = true;
                else agent.State["nearEnemy"] = false;
            }
        }

        private static ExecutionStatus KillNearbyEnemyExecutor(Agent agent, Action action) {
            if (agent.State["agents"] is List<Agent> agents) {
                var agent2 = RpgUtils.GetEnemyInRange(agent, agents, 1f);
                if (agent2 != null && agent2.State["hp"] is int hp) {
                    hp--;
                    agent2.State["hp"] = hp;
                    if (hp <= 0) return ExecutionStatus.Succeeded;
                }
            }
            return ExecutionStatus.Failed;
        }

        private static ExecutionStatus GoToNearestEnemyExecutor(Agent agent, Action action) {
            if (agent.State["agents"] is List<Agent> agents) {
                var agent2 = RpgUtils.GetEnemyInRange(agent, agents, 5f);
                if (agent2 != null && agent.State["position"] is Vector2 pos1 && agent2.State["position"] is Vector2 pos2) {
                    agent.State["position"] = RpgUtils.MoveTowardsOtherPosition(pos1, pos2);
                    if (RpgUtils.InDistance(pos1, pos2, 1f)) return ExecutionStatus.Succeeded;
                }
            }
            return ExecutionStatus.Failed;
        }
    }
}
