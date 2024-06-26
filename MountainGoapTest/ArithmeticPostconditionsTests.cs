using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MountainGoapTest
{
    public class ArithmeticPostconditionsTests
    {
        [Fact]
        public void MinimalExampleTest()
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

            List<MountainGoap.Action> actions = new() {
                new MountainGoap.Action(
                    name: "Action1",
                    executor: (Agent agent, MountainGoap.Action action) => {
                        return ExecutionStatus.Succeeded;
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

            agent.Step(StepMode.OneAction);
            Assert.Equal(10, agent.State["i"]);
            agent.Step(StepMode.OneAction);
            Assert.Equal(20, agent.State["i"]);
        }
    }
}
