// <copyright file="Action.cs" company="Chris Muller">
// Copyright (c) Chris Muller. All rights reserved.
// </copyright>
namespace MountainGoap {
    using System.Reflection;

    /// <summary>
    /// Represents an action in a GOAP system.
    /// </summary>
    public class Action {
        /// <summary>
        /// Name of the action.
        /// </summary>
        public readonly string Name;

        /// <summary>
        /// Cost of the action.
        /// </summary>
        public readonly float Cost;

        /// <summary>
        /// The permutation selector callback for the action.
        /// </summary>
        private readonly Dictionary<string, PermutationSelectorCallback> permutationSelectors;

        /// <summary>
        /// The executor callback for the action.
        /// </summary>
        private readonly ExecutorCallback executor;

        /// <summary>
        /// Parameters to be passed to the action.
        /// </summary>
        private readonly Dictionary<string, object> parameters = new();

        /// <summary>
        /// Preconditions for the action. These things are required for the action to execute.
        /// </summary>
        private readonly Dictionary<string, object> preconditions = new();

        /// <summary>
        /// Postconditions for the actions. These will be set when the action has executed.
        /// </summary>
        private readonly Dictionary<string, object> postconditions = new();

        /// <summary>
        /// Initializes a new instance of the <see cref="Action"/> class.
        /// </summary>
        /// <param name="name">Name for the action, for eventing and logging purposes.</param>
        /// <param name="permutationSelectors">The permutation selector callback for the action's parameters.</param>
        /// <param name="executor">The executor callback for the action.</param>
        /// <param name="cost">Cost of the action.</param>
        /// <param name="preconditions">Preconditions required in the world state in order for the action to occur.</param>
        /// <param name="postconditions">Postconditions applied after the action is successfully executed.</param>
        public Action(string? name = null, Dictionary<string, PermutationSelectorCallback>? permutationSelectors = null, ExecutorCallback? executor = null, float cost = 1f, Dictionary<string, object>? preconditions = null, Dictionary<string, object>? postconditions = null) {
            if (permutationSelectors == null) this.permutationSelectors = new();
            else this.permutationSelectors = permutationSelectors;
            if (executor == null) this.executor = DefaultExecutorCallback;
            else this.executor = executor;
            Name = name ?? $"Action {Guid.NewGuid()} ({this.executor.GetMethodInfo().Name})";
            Cost = cost;
            if (preconditions != null) this.preconditions = preconditions;
            if (postconditions != null) this.postconditions = postconditions;
        }

        /// <summary>
        /// Gets or sets the execution status of the action.
        /// </summary>
        internal ExecutionStatus ExecutionStatus { get; set; } = ExecutionStatus.NotYetExecuted;

        /// <summary>
        /// Sets a parameter to the action.
        /// </summary>
        /// <param name="key">Key to be set.</param>
        /// <param name="value">Value to be set.</param>
        public void SetParameter(string key, object value) {
            parameters[key] = value;
        }

        /// <summary>
        /// Gets a parameter to the action.
        /// </summary>
        /// <param name="key">Key for the value to be retrieved.</param>
        /// <returns>The value stored at the key specified.</returns>
        public object? GetParameter(string key) {
            if (parameters.ContainsKey(key)) return parameters[key];
            return null;
        }

        /// <summary>
        /// Executes a step of work for the agent.
        /// </summary>
        /// <param name="agent">Agent executing the action.</param>
        internal void Execute(Agent agent) {
            if (IsPossible(agent.State)) {
                var newState = executor(agent, this);
                if (newState == ExecutionStatus.Succeeded) ApplyEffects(agent.State);
                ExecutionStatus = newState;
            }
        }

        /// <summary>
        /// Determines whether or not an action is possible.
        /// </summary>
        /// <param name="state">The current world state.</param>
        /// <returns>True if the action is possible, otherwise false.</returns>
        internal bool IsPossible(Dictionary<string, object> state) {
            foreach (var kvp in preconditions) {
                if (!state.ContainsKey(kvp.Key)) return false;
                if (!state[kvp.Key].Equals(kvp.Value)) return false;
            }
            return true;
        }

        /// <summary>
        /// Gets all permutations of parameters possible for an action.
        /// </summary>
        /// <param name="agent">Agent for which the action would be performed.</param>
        /// <returns>A list of possible parameter dictionaries that could be used.</returns>
        internal List<Dictionary<string, object>> GetPermutations(Agent agent) {
            List<Dictionary<string, object>> combinedOutputs = new();
            Dictionary<string, List<object>> outputs = new();
            foreach (var kvp in permutationSelectors) outputs[kvp.Key] = kvp.Value(agent.State);
            var parameters = outputs.Keys.ToList();
            List<int> indices = new();
            List<int> counts = new();
            foreach (var parameter in parameters) {
                indices.Add(0);
                counts.Add(outputs[parameter].Count);
            }
            while (true) {
                var singleOutput = new Dictionary<string, object>();
                for (int i = 0; i < indices.Count; i++) singleOutput[parameters[i]] = outputs[parameters[i]][indices[i]];
                combinedOutputs.Add(singleOutput);
                if (IndicesAtMaximum(indices, counts)) return combinedOutputs;
                IncrementIndices(indices, counts);
            }
        }

        /// <summary>
        /// Applies the effects of the action.
        /// </summary>
        /// <param name="state">World state to which to apply effects.</param>
        internal void ApplyEffects(Dictionary<string, object> state) {
            foreach (var kvp in postconditions) state[kvp.Key] = kvp.Value;
        }

        private static bool IndicesAtMaximum(List<int> indices, List<int> counts) {
            for (int i = 0; i < indices.Count; i++) if (indices[i] < counts[i] - 1) return false;
            return true;
        }

        private static void IncrementIndices(List<int> indices, List<int> counts) {
            if (IndicesAtMaximum(indices, counts)) return;
            for (int i = 0; i < indices.Count; i++) {
                if (indices[i] == counts[i] - 1) indices[i] = 0;
                else {
                    indices[i]++;
                    return;
                }
            }
        }

        /// <summary>
        /// Default executor callback to be used if no callback is passed in.
        /// </summary>
        /// <param name="agent">Agent executing the action.</param>
        /// <param name="action">Action to be executed.</param>
        /// <returns>A Failed status, since the action cannot execute without a callback.</returns>
        private static ExecutionStatus DefaultExecutorCallback(Agent agent, Action action) {
            return ExecutionStatus.Failed;
        }
    }
}