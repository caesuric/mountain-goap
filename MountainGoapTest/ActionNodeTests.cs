namespace MountainGoapTest {
    using System.Collections.Generic;

    public class ActionNodeTests {
        [Fact]
        public void ItHandlesNullStateValuesCorrectly() {
            var agent = new Agent(
                state: new Dictionary<string, object> {
                    { "key", null }
                },
                goals: new List<BaseGoal> {
                    new Goal(
                        desiredState: new Dictionary<string, object> {
                            { "key", "non-null value" }
                        }
                    )
                },
                actions: new List<Action> {
                    new Action(
                        preconditions: new Dictionary<string, object> {
                            { "key", "non-null value" }
                        },
                        postconditions: new Dictionary<string, object> {
                            { "key", null }
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
                state: new Dictionary<string, object> {
                    { "key", "new value" }
                },
                goals: new List<BaseGoal> {
                    new Goal(
                        desiredState: new Dictionary<string, object> {
                            { "key", "non-null value" }
                        }
                    )
                },
                actions: new List<Action> {
                    new Action(
                        preconditions: new Dictionary<string, object> {
                            { "key", "non-null value" }
                        },
                        postconditions: new Dictionary<string, object> {
                            { "key", "new value" }
                        }
                    )
                }
            );
            agent.Step(StepMode.OneAction);
            Assert.Equal("new value", (string)agent.State["key"]);
        }
    }
}
