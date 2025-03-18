// <copyright file="HashIssue.cs" company="Chris Muller">
// Copyright (c) Chris Muller. All rights reserved.
// </copyright>

namespace Examples {
    using System.Collections.Concurrent;
    using MountainGoap;
    using MountainGoapLogging;

    /// <summary>
    /// Debug example.
    /// </summary>
    internal static class HashIssue {
        /// <summary>
        /// Runs the demo.
        /// </summary>
        internal static void Run() {
            _ = new DefaultLogger();
            Agent agent = new(
                name: "Happiness Agent",
                state: new() {
                    { "happiness", 0 },
                    { "health", false },
                },
                goals: new() {
                    new ExtremeGoal(
                        name: "Maximize Happiness",
                        desiredState: new() {
                            { "happiness", true }
                        })
                },
                actions: new() {
                    new(
                        name: "Seek Happiness",
                        executor: SeekHappinessAction,
                        preconditions: new() {
                            { "health", true }
                        },
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
                        preconditions: new() {
                            { "health", true }
                        },
                        arithmeticPostconditions: new() {
                            {
                                "happiness",
                                2
                            }
                        }
                    ),
                    new(
                        name: "Seek Health",
                        executor: SeekHealth,
                        postconditions: new() {
                            {
                                "health",
                                true
                            }
                        }
                    ),
                }
            );
            var state = new ConcurrentDictionary<string, object?>(agent.State);
            Console.WriteLine(new ActionNode(agent.Actions[0], new(), new()).GetHashCode());
            Console.WriteLine(new ActionNode(agent.Actions[1], state, new()).GetHashCode());
            var copy = agent.Actions[1].Copy();
            Console.WriteLine(new ActionNode(copy, state, new()).GetHashCode());
        }

        private static ExecutionStatus SeekHappinessAction(Agent agent, Action action) {
            Console.WriteLine("Seeking happiness.");
            return ExecutionStatus.Succeeded;
        }

        private static ExecutionStatus SeekGreaterHappinessAction(Agent agent, Action action) {
            Console.WriteLine("Seeking even greater happiness.");
            return ExecutionStatus.Succeeded;
        }

        private static ExecutionStatus SeekHealth(Agent agent, Action action) {
            Console.WriteLine("Seeking health.");
            return ExecutionStatus.Succeeded;
        }
    }
}
