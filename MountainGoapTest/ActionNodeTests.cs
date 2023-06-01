namespace MountainGoapTest {
    using System.Collections.Generic;

    public class AgentTests {
        [Fact]
        public void ItHandlesInitialNullStateValuesCorrectly() {
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
                            return ExecutionStatus.Succeeded;
                        }
                    )
                }
            );
            agent.Step(StepMode.OneAction);
            Assert.NotNull(agent.State["key"]);
        }

        [Fact]
        public void ItHandlesNullGoalsCorrectly() {
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
                            return ExecutionStatus.Succeeded;
                        }
                    )
                }
            );
            agent.Step(StepMode.OneAction);
            Assert.Null(agent.State["key"]);
        }

        [Fact]
        public void ItHandlesNonNullStateValuesCorrectly() {
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
                            return ExecutionStatus.Succeeded;
                        }
                    )
                }
            );
            agent.Step(StepMode.OneAction);
            Assert.Equal("new value", (string)agent.State["key"]);
        }

        [Fact]
        public void ItExecutesOneActionInOneActionStepMode() {
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
                            return ExecutionStatus.Succeeded;
                        }
                    )
                }
            );
            agent.Step(StepMode.OneAction);
            Assert.Equal(1, actionCount);
        }
    }
}
