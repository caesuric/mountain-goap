using System.Threading.Tasks;

namespace MountainGoapTest.Async {
    using System.Collections.Concurrent;
    using System.Collections.Generic;

    public class ActionContinuationTests {
        [Fact]
        public async Task ItCanContinueActions() {
            var timesExecuted = 0;
            var agent = new Agent(
                state: new() {
                    { "key", false },
                    { "progress", 0 }
                },
                goals: new List<BaseGoal> {
                    new Goal(
                        desiredState: new() {
                            { "key", true }
                        }
                    )
                },
                actions: new List<Action> {
                    new Action(
                        preconditions: new() {
                            { "key", false }
                        },
                        postconditions: new() {
                            { "key", true }
                        },
                        executor: (Agent agent, Action action) => {
                            timesExecuted++;
                            if (agent.State["progress"] is int progress && progress < 3) {
                                agent.State["progress"] = progress + 1;
                                return Task.FromResult(ExecutionStatus.Executing);
                            }
                            else return Task.FromResult(ExecutionStatus.Succeeded);
                        }
                    )
                }
            );
            await agent.StepAsync(StepMode.OneAction);
            if (agent.State["key"] is bool value) Assert.False(value);
            else Assert.False(true);
            await agent.StepAsync(StepMode.OneAction);
            if (agent.State["key"] is bool value2) Assert.False(value2);
            else Assert.False(true);
            await agent.StepAsync(StepMode.OneAction);
            if (agent.State["key"] is bool value3) Assert.False(value3);
            else Assert.False(true);
            await agent.StepAsync(StepMode.OneAction);
            if (agent.State["key"] is bool value4) Assert.True(value4);
            else Assert.False(true);
            Assert.Equal(4, timesExecuted);
        }
    }
}
