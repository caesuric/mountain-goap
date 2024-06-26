using System.Threading.Tasks;

namespace MountainGoapTest {
    using System.Collections.Generic;

    public class AgentTests {
        [Fact]
        public async Task ItHandlesInitialNullStateValuesCorrectly() {
            var agent = new Agent(
                state: new() {
                    { "key", null }
                },
                goals: new List<BaseGoal> {
                    new Goal(
                        desiredState: new() {
                            { "key", "non-null value" }
                        }
                    )
                },
                actions: new List<Action> {
                    new Action(
                        preconditions: new() {
                            { "key", null }
                        },
                        postconditions: new() {
                            { "key", "non-null value" }
                        },
                        executor: (Agent agent, Action action) => {
                            return Task.FromResult(ExecutionStatus.Succeeded);
                        }
                    )
                }
            );
            await agent.StepAsync(StepMode.OneAction);
            Assert.NotNull(agent.State["key"]);
        }

        [Fact]
        public async Task ItHandlesNullGoalsCorrectly() {
            var agent = new Agent(
                state: new() {
                    { "key", "non-null value" }
                },
                goals: new List<BaseGoal> {
                    new Goal(
                        desiredState: new() {
                            { "key", null }
                        }
                    )
                },
                actions: new List<Action> {
                    new Action(
                        preconditions: new() {
                            { "key", "non-null value" }
                        },
                        postconditions: new() {
                            { "key", null }
                        },
                        executor: (Agent agent, Action action) => {
                            return Task.FromResult(ExecutionStatus.Succeeded);
                        }
                    )
                }
            );
            await agent.StepAsync(StepMode.OneAction);
            Assert.Null(agent.State["key"]);
        }

        [Fact]
        public async Task ItHandlesNonNullStateValuesCorrectly() {
            var agent = new Agent(
                state: new() {
                    { "key", "value" }
                },
                goals: new List<BaseGoal> {
                    new Goal(
                        desiredState: new() {
                            { "key", "new value" }
                        }
                    )
                },
                actions: new List<Action> {
                    new Action(
                        preconditions: new() {
                            { "key", "value" }
                        },
                        postconditions: new() {
                            { "key", "new value" }
                        },
                        executor: (Agent agent, Action action) => {
                            return Task.FromResult(ExecutionStatus.Succeeded);
                        }
                    )
                }
            );
            await agent.StepAsync(StepMode.OneAction);
            object? value = agent.State["key"];
            Assert.NotNull(value);
            if (value is not null) Assert.Equal("new value", (string)value);
        }

        [Fact]
        public async Task ItExecutesOneActionInOneActionStepMode() {
            var actionCount = 0;
            var agent = new Agent(
                state: new() {
                    { "key", "value" }
                },
                goals: new List<BaseGoal> {
                    new Goal(
                        desiredState: new() {
                            { "key", "new value" }
                        }
                    )
                },
                actions: new List<Action> {
                    new Action(
                        preconditions: new() {
                            { "key", "value" }
                        },
                        postconditions: new() {
                            { "key", "new value" }
                        },
                        executor: (Agent agent, Action action) => {
                            actionCount++;
                            return Task.FromResult(ExecutionStatus.Succeeded);
                        }
                    )
                }
            );
            await agent.StepAsync(StepMode.OneAction);
            Assert.Equal(1, actionCount);
        }

        [Fact]
        public async Task ItExecutesAllActionsInAllActionsStepMode() {
            var actionCount = 0;
            var agent = new Agent(
                state: new() {
                    { "key", "value" }
                },
                goals: new List<BaseGoal> {
                    new Goal(
                        desiredState: new() {
                            { "key", "new value" }
                        }
                    )
                },
                actions: new List<Action> {
                    new Action(
                        preconditions: new() {
                            { "key", "value" }
                        },
                        postconditions: new() {
                            { "key", "intermediate value" }
                        },
                        executor: (Agent agent, Action action) => {
                            actionCount++;
                            return Task.FromResult(ExecutionStatus.Succeeded);
                        }
                    ),
                    new Action(
                        preconditions: new() {
                            { "key", "intermediate value" }
                        },
                        postconditions: new() {
                            { "key", "new value" }
                        },
                        executor: (Agent agent, Action action) => {
                            actionCount++;
                            return Task.FromResult(ExecutionStatus.Succeeded);
                        }
                    )
                }
            );
            await agent.StepAsync(StepMode.AllActions);
            Assert.Equal(2, actionCount);
        }
    }
}
