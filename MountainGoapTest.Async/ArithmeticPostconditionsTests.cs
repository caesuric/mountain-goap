using System.Collections.Generic;
using System.Threading.Tasks;

namespace MountainGoapTest.Async
{
    public class ArithmeticPostconditionsTests
    {
        [Fact]
        public async Task MinimalExampleTest()
        {

            List<BaseGoal> goals = new() {
                new ComparativeGoal(
                    name: "Goal1",
                    desiredState: new() {
                        { "i", new ComparisonValuePair {
                            Value = 100,
                            Operator = ComparisonOperator.GreaterThan
                        } }
                    },
                    weight: 1f
                ),
            };

            List<MountainGoap.Async.Action> actions = new() {
                new MountainGoap.Async.Action(
                    name: "Action1",
                    executor: (Agent agent, MountainGoap.Async.Action action) => {
                        return Task.FromResult(ExecutionStatus.Succeeded);
                    },
                    arithmeticPostconditions: new Dictionary<string, object> {
                        { "i", 10 }
                    },
                    cost: 0.5f
                ),
            };

            Agent agent = new(
                goals: goals,
                actions: actions,
                state: new() {
                    { "i", 0 }
                }
            );

            await agent.StepAsync(StepMode.OneAction);
            Assert.Equal(10, agent.State["i"]);
            await agent.StepAsync(StepMode.OneAction);
            Assert.Equal(20, agent.State["i"]);
        }
    }
}
