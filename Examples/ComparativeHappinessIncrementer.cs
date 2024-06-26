// <copyright file="ComparativeHappinessIncrementer.cs" company="Chris Muller">
// Copyright (c) Chris Muller. All rights reserved.
// </copyright>

namespace Examples {
    using MountainGoap;
    using MountainGoapLogging;

    /// <summary>
    /// Simple goal to maximize happiness.
    /// </summary>
    internal static class ComparativeHappinessIncrementer {
        /// <summary>
        /// Runs the demo.
        /// </summary>
        /// <returns>Async Task</returns>
        internal static async Task Run() {
            _ = new DefaultLogger();
            Agent agent = new(
                name: "Happiness Agent",
                state: new() {
                    { "happiness", 0 },
                },
                goals: new() {
                    new ComparativeGoal(
                        name: "Maximize Happiness",
                        desiredState: new() {
                            { "happiness", new() { Operator = ComparisonOperator.GreaterThanOrEquals, Value = 10 } }
                        })
                },
                actions: new() {
                    new(
                        name: "Seek Happiness",
                        executor: SeekHappinessAction,
                        arithmeticPostconditions: new() {
                            {
                                "happiness",
                                1
                            }
                        }
                    ),
                    new(
                        name: "Seek Greater Happiness",
                        executor: SeekGreaterHappinessAction,
                        arithmeticPostconditions: new() {
                            {
                                "happiness",
                                2
                            }
                        }
                    )
                }
            );
            while (agent.State["happiness"] is int happiness && happiness < 10) {
                await agent.StepAsync();
                Console.WriteLine($"NEW HAPPINESS IS {agent.State["happiness"]}");
            }
        }

        private static Task<ExecutionStatus> SeekHappinessAction(Agent agent, Action action) {
            Console.WriteLine("Seeking happiness.");
            return Task.FromResult(ExecutionStatus.Succeeded);
        }

        private static Task<ExecutionStatus> SeekGreaterHappinessAction(Agent agent, Action action) {
            Console.WriteLine("Seeking even greater happiness.");
            return Task.FromResult(ExecutionStatus.Succeeded);
        }
    }
}
