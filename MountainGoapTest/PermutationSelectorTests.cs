using System.Threading.Tasks;

namespace MountainGoapTest {
    using System.Collections.Generic;

    public class PermutationSelectorTests {
        [Fact]
        public async Task ItSelectsFromADynamicallyGeneratedCollectionInState() {
            var collection = new List<int> { 1, 2, 3 };
            var selector = PermutationSelectorGenerators.SelectFromCollectionInState<int>("collection");
            var agent = new Agent(
                name: "sample agent",
                state: new() {
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
                        executor: (agent, action) => Task.FromResult(ExecutionStatus.Succeeded))
                },
                sensors: new() {
                    new(
                        (agent) => {
                            if (agent.State["collection"] is List<int> collection) {
                                collection.Add(4);
                            }
                            return Task.CompletedTask;
                        },
                        name: "sample sensor"
                    )
                }
            );
            List<object> permutations = await selector(agent.State);
            Assert.Equal(3, permutations.Count);
            await agent.StepAsync(StepMode.OneAction);
            permutations = await selector(agent.State);
            Assert.Equal(4, permutations.Count);
            await agent.StepAsync(StepMode.OneAction);
            permutations = await selector(agent.State);
            Assert.Equal(5, permutations.Count);
        }
    }
}
