namespace MountainGoapTest {
    using System.Collections.Concurrent;
    using System.Collections.Generic;

    public class PermutationSelectorGeneratorTests {
        [Fact]
        public void ItSelectsFromACollection() {
            var collection = new List<int> { 1, 2, 3 };
            var selector = PermutationSelectorGenerators.SelectFromCollection(collection);
            List<object> permutations = selector(new ConcurrentDictionary<string, object?>());
            Assert.Equal(3, permutations.Count);
        }

        [Fact]
        public void ItSelectsFromACollectionInState() {
            var collection = new List<int> { 1, 2, 3 };
            var selector = PermutationSelectorGenerators.SelectFromCollectionInState<int>("collection");
            List<object> permutations = selector(new ConcurrentDictionary<string, object?> { { "collection", collection } });
            Assert.Equal(3, permutations.Count);
        }

        [Fact]
        public void ItSelectsFromAnIntegerRange() {
            var selector = PermutationSelectorGenerators.SelectFromIntegerRange(1, 4);
            List<object> permutations = selector(new ConcurrentDictionary<string, object?>());
            Assert.Equal(3, permutations.Count);
        }
    }
}
