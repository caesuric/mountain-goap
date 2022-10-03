// <copyright file="Demo1.cs" company="Chris Muller">
// Copyright (c) Chris Muller. All rights reserved.
// </copyright>

namespace Examples {
    using MountainGoap;

    /// <summary>
    /// First demo for the examples.
    /// </summary>
    internal static class Demo1 {
        /// <summary>
        /// Runs the demo.
        /// </summary>
        internal static void Run() {
            Agent agent = new(
                state: new() {
                    { "happiness", 0 },
                    { "happinessRecentlyIncreased", false }
                },
                goals: new() {
                    new(
                        desiredState: new() {
                            { "happinessRecentlyIncreased", true }
                        }
                    )
                },
                sensors: new() {
                    new(EnnuiSensorHandler)
                },
                actions: new() {
                    new(
                        executor: SeekHappinessAction,
                        preconditions: new() {
                            { "happinessRecentlyIncreased", false }
                        },
                        postconditions: new() {
                            { "happinessRecentlyIncreased", true }
                        }
                    )
                }
            );
            while ((int)agent.State["happiness"] != 10) agent.Step();
        }

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

        private static void EnnuiSensorHandler(Agent agent) {
            agent.State["happinessRecentlyIncreased"] = false;
        }
    }
}
