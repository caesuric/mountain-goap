﻿// <copyright file="CarDemo.cs" company="Chris Muller">
// Copyright (c) Chris Muller. All rights reserved.
// </copyright>

namespace Examples {
    using MountainGoap;
    using MountainGoapLogging;

    /// <summary>
    /// Simple goal to travel via walking or driving.
    /// </summary>
    internal static class CarDemo {
        /// <summary>
        /// Runs the demo.
        /// </summary>
        /// <returns>Async Task.</returns>
        internal static async Task Run() {
            _ = new DefaultLogger();
            var agent = new Agent(
                name: "Driving Agent",
                state: new() {
                    { "distanceTraveled", 0 },
                    { "inCar", false }
                },
                goals: new() {
                    new ComparativeGoal(
                        name: "Travel 50 miles",
                        desiredState: new() {
                            {
                                "distanceTraveled", new() {
                                    Operator = ComparisonOperator.GreaterThanOrEquals,
                                    Value = 50
                                }
                            }
                        })
                },
                actions: new() {
                    new(
                        name: "Walk",
                        cost: 50,
                        postconditions: new() {
                            { "distanceTraveled", 50 }
                        },
                        executor: TravelExecutor
                    ),
                    new(
                        name: "Drive",
                        cost: 5,
                        preconditions: new() {
                            { "inCar", true }
                        },
                        postconditions: new() {
                            { "distanceTraveled", 50 }
                        },
                        executor: TravelExecutor
                    ),
                    new(
                        name: "Get in Car",
                        cost: 1,
                        preconditions: new() {
                            { "inCar", false }
                        },
                        postconditions: new() {
                            { "inCar", true }
                        },
                        executor: GetInCarExecutor
                    )
                });
            while (agent.State["distanceTraveled"] is int distance && distance < 50) await agent.StepAsync();
        }

        private static Task<ExecutionStatus> TravelExecutor(Agent agent, Action action) {
            return Task.FromResult(ExecutionStatus.Succeeded);
        }

        private static Task<ExecutionStatus> GetInCarExecutor(Agent agent, Action action) {
            return Task.FromResult(ExecutionStatus.Succeeded);
        }
    }
}
