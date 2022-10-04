// <copyright file="RpgEnemy.cs" company="Chris Muller">
// Copyright (c) Chris Muller. All rights reserved.
// </copyright>

namespace Examples {
    using MountainGoap;

    /// <summary>
    /// RPG enemy demo.
    /// </summary>
    internal static class RpgEnemy {
        /// <summary>
        /// Runs the demo.
        /// </summary>
        internal static void Run() {
            Goal eatFood = new(
                weight: 0.5f,
                desiredState: new() {
                    { "eatingFood", true }
                }
            );
            Goal removeEnemies = new(
                weight: 1f,
                desiredState: new() {
                    { "canSeeEnemies", false }
                }
            );
            Sensor visionSensor = new(VisionSensorHandler);
            Sensor foodProximitySensor = new(FoodProximitySensorHandler);
            Sensor enemyProximitySensor = new(EnemyProximitySensorHandler);
            Action lookForFood = new();
            Action goToFood = new();
            Action goToEnemy = new();
            Action killEnemy = new();
            object[,] map = new object[20, 20];  // fill map data for state

            Agent agent = new(
                state: new() {
                    { "canSeeFood", false },
                    { "eatingFood", false },
                    { "canSeeEnemies", false },
                    { "nearFood", false },
                    { "nearEnemy", false }
                },
                goals: new() {
                    eatFood,
                    removeEnemies
                },
                sensors: new() {
                    visionSensor
                },
                actions: new() {
                    lookForFood,
                    goToFood,
                    goToEnemy,
                    killEnemy
                }
            );
            for (int i = 0; i < 120; i++) {
                agent.Step();
                Thread.Sleep(1000);
            }
    }

#pragma warning disable IDE0060 // Remove unused parameter
        private static ExecutionStatus SeekHappinessAction(Agent agent, Action action) {
            int? happiness = agent.State["happiness"] as int?;
            if (happiness != null) {
                happiness++;
                agent.State["happiness"] = happiness;
                Console.WriteLine("Seeking happiness.");
                Console.WriteLine($"NEW HAPPINESS IS {happiness}");
            }
            return ExecutionStatus.Succeeded;
        }
#pragma warning restore IDE0060 // Remove unused parameter

        private static void EnnuiSensorHandler(Agent agent) {
            agent.State["happinessRecentlyIncreased"] = false;
        }
    }
}
