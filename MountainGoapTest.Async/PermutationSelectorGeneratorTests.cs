using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MountainGoapTest.Async {
    public class PermutationSelectorGeneratorTests {
        [Fact]
        public async Task ItSelectsFromACollection() {
            var collection = new List<int> { 1, 2, 3 };
            var selector = PermutationSelectorGenerators.SelectFromCollection(collection);
            List<object> permutations = await selector(new ConcurrentDictionary<string, object?>());
            Assert.Equal(3, permutations.Count);
        }

        [Fact]
        public async Task ItSelectsFromACollectionInState() {
            var collection = new List<int> { 1, 2, 3 };
            var selector = PermutationSelectorGenerators.SelectFromCollectionInState<int>("collection");
            List<object> permutations = await selector(new ConcurrentDictionary<string, object?> { { "collection", collection } });
            Assert.Equal(3, permutations.Count);
        }

        [Fact]
        public async Task ItSelectsFromAnIntegerRange() {
            var selector = PermutationSelectorGenerators.SelectFromIntegerRange(1, 4);
            List<object> permutations = await selector(new ConcurrentDictionary<string, object?>());
            Assert.Equal(3, permutations.Count);
        }
    }
}
