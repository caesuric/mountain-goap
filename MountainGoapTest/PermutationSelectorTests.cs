namespace MountainGoapTest {
    using System.Collections.Generic;

    public class PermutationSelectorTests {
        [Fact]
        public void ItSelectsFromADynamicallyGeneratedCollectionInState() {
            var collection = new List<int> { 1, 2, 3 };
            var selector = PermutationSelectorGenerators.SelectFromCollectionInState<int>("collection");
            var agent = new Agent(
                name: "sample agent",
                state: new Dictionary<string, object?> {
                    { "collection", collection },
                    { "goalAchieved", false }
                },
                goals: new() {
                    new Goal(
                        name: "sample goal",
                        desiredState: new Dictionary<string, object?> {
                            { "goalAchieved", true }
                        }
                    )
                },
                actions: new() {
                    new(
                        name: "sample action",
                        cost: 1f,
                        preconditions: new() {
                            { "goalAchieved", false }
                        },
                        postconditions: new() {
                            { "goalAchieved", true }
                        },
                        executor: (agent, action) => { return ExecutionStatus.Succeeded; }
                    )
                },
                sensors: new() {
                    new(
                        (agent) => {
                            if (agent.State["collection"] is List<int> collection) {
                                collection.Add(4);
                            }
                        },
                        name: "sample sensor"
                    )
                }
            );
            List<object> permutations = selector(agent.State);
            Assert.Equal(3, permutations.Count);
            agent.Step(StepMode.OneAction);
            permutations = selector(agent.State);
            Assert.Equal(4, permutations.Count);
            agent.Step(StepMode.OneAction);
            permutations = selector(agent.State);
            Assert.Equal(5, permutations.Count);
        }
    }
}
