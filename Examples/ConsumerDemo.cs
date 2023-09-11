// <copyright file="ConsumerDemo.cs" company="Chris Muller">
// Copyright (c) Chris Muller. All rights reserved.
// </copyright>

namespace Examples {
    using MountainGoap;
    using MountainGoapLogging;

    /// <summary>
    /// Goal to create enough food to eat by working and grocery shopping.
    /// </summary>
    internal static class ConsumerDemo {
        /// <summary>
        /// Runs the demo.
        /// </summary>
        internal static void Run() {
            _ = new DefaultLogger();
            var locations = new List<string> { "home", "work", "store" };
            var agent = new Agent(
                name: "Consumer Agent",
                state: new() {
                    { "food", 4 },
                    { "energy", 100 },
                    { "money", 0 },
                    { "inCar", false },
                    { "location", "home" }
                },
                goals: new() {
                    //new ComparativeGoal(
                    //    name: "Get at least 5 food",
                    //    desiredState: new() {
                    //        {
                    //            "food", new() {
                    //                Operator = ComparisonOperator.GreaterThanOrEquals,
                    //                Value = 5
                    //            }
                    //        }
                    //    })
                    //new Goal(
                    //    name: "Get 5 food",
                    //    desiredState: new() {
                    //        { "food", 5 }
                    //    })
                    new ExtremeGoal(
                        name: "Get food",
                        desiredState: new() {
                            { "food", true }
                        })
                },
                actions: new() {
                    new(
                        name: "Walk",
                        cost: 10,
                        executor: GenericExecutor,
                        permutationSelectors: new() {
                            { "location", PermutationSelectorGenerators.SelectFromCollection(locations) }
                        },
                        comparativePreconditions: new() {
                            { "energy", new() { Operator = ComparisonOperator.GreaterThan, Value = 0 } }
                        },
                        arithmeticPostconditions: new() {
                            { "energy", -1 }
                        },
                        parameterPostconditions: new() {
                            { "location", "location" }
                        }
                    ),
                    new(
                        name: "Drive",
                        cost: 1,
                        preconditions: new() {
                            { "inCar", true }
                        },
                        comparativePreconditions: new() {
                            { "energy", new() { Operator = ComparisonOperator.GreaterThan, Value = 0 } }
                        },
                        executor: GenericExecutor,
                        permutationSelectors: new() {
                            { "location", PermutationSelectorGenerators.SelectFromCollection(locations) }
                        },
                        arithmeticPostconditions: new() {
                            { "energy", -1 }
                        },
                        parameterPostconditions: new() {
                            { "location", "location" }
                        }
                    ),
                    new(
                        name: "Get in Car",
                        cost: 1f,
                        preconditions: new() {
                            { "inCar", false }
                        },
                        comparativePreconditions: new() {
                            { "energy", new() { Operator = ComparisonOperator.GreaterThan, Value = 0 } }
                        },
                        postconditions: new() {
                            { "inCar", true }
                        },
                        arithmeticPostconditions: new() {
                            { "energy", -1 }
                        },
                        executor: GenericExecutor
                    ),
                    new(
                        name: "Work",
                        cost: 1f,
                        preconditions: new() {
                            { "location", "work" },
                        },
                        comparativePreconditions: new() {
                            { "energy", new() { Operator = ComparisonOperator.GreaterThan, Value = 0 } }
                        },
                        arithmeticPostconditions: new() {
                            { "energy", -1 },
                            { "money", 1 }
                        },
                        executor: GenericExecutor
                    ),
                    new(
                        name: "Shop",
                        cost: 1f,
                        preconditions: new() {
                            { "location", "store" }
                        },
                        comparativePreconditions: new() {
                            { "energy", new() { Operator = ComparisonOperator.GreaterThan, Value = 0 } },
                            { "money", new() { Operator = ComparisonOperator.GreaterThan, Value = 0 } }
                        },
                        arithmeticPostconditions: new() {
                            { "energy", -1 },
                            { "money", -1 },
                            { "food", 1 }
                        },
                        executor: GenericExecutor
                    )
                });
            while (agent.State["food"] is int food && food < 5) agent.Step();
        }

        private static ExecutionStatus GenericExecutor(Agent agent, Action action) {
            return ExecutionStatus.Succeeded;
        }
    }
}
