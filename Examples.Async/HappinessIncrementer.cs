// <copyright file="HappinessIncrementer.cs" company="Chris Muller">
// Copyright (c) Chris Muller. All rights reserved.
// </copyright>

namespace Examples.Async {
    using MountainGoap.Async;
    using MountainGoapLogging.Async;

    /// <summary>
    /// Simple goal to maximize happiness.
    /// </summary>
    internal static class HappinessIncrementer {
        /// <summary>
        /// Runs the demo.
        /// </summary>
        /// <returns>Async Task.</returns>
        internal static async Task Run() {
            _ = new DefaultLogger();
            Agent agent = new(
                name: "Happiness Agent",
                state: new() {
                    { "happiness", 0 },
                    { "happinessRecentlyIncreased", false }
                },
                goals: new() {
                    new Goal(
                        name: "Maximize Happiness",
                        desiredState: new() {
                            { "happinessRecentlyIncreased", true }
                        })
                },
                sensors: new() {
                    new(EnnuiSensorHandler, "Ennui Sensor")
                },
                actions: new() {
                    new(
                        name: "Seek Happiness",
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
            while (agent.State["happiness"] is int happiness && happiness != 10) await agent.StepAsync();
        }

        private static Task<ExecutionStatus> SeekHappinessAction(Agent agent, Action action) {
            int? happiness = agent.State["happiness"] as int?;
            if (happiness != null) {
                happiness++;
                agent.State["happiness"] = happiness;
                Console.WriteLine("Seeking happiness.");
                Console.WriteLine($"NEW HAPPINESS IS {happiness}");
            }
            return Task.FromResult(ExecutionStatus.Succeeded);
        }

        private static Task EnnuiSensorHandler(Agent agent) {
            agent.State["happinessRecentlyIncreased"] = false;
            return Task.CompletedTask;
        }
    }
}