// <copyright file="Action.cs" company="Chris Muller">
// Copyright (c) Chris Muller. All rights reserved.
// </copyright>
namespace MountainGoap {
    using System;
    using System.Collections.Concurrent;
    using System.Collections.Generic;
    using System.Linq;
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
        private readonly float cost;

        /// <summary>
        /// The permutation selector callbacks for the action.
        /// </summary>
        private readonly Dictionary<string, PermutationSelectorCallback> permutationSelectors;

        /// <summary>
        /// The executor callback for the action.
        /// </summary>
        private readonly ExecutorCallback executor;

        /// <summary>
        /// The cost callback for the action.
        /// </summary>
        private readonly CostCallback costCallback;

        /// <summary>
        /// Preconditions for the action. These things are required for the action to execute.
        /// </summary>
        private readonly Dictionary<string, object?> preconditions = new();

        /// <summary>
        /// Comnparative preconditions for the action. Indicates that a value must be greater than or less than a certain value for the action to execute.
        /// </summary>
        private readonly Dictionary<string, ComparisonValuePair> comparativePreconditions = new();

        /// <summary>
        /// Postconditions for the action. These will be set when the action has executed.
        /// </summary>
        private readonly Dictionary<string, object?> postconditions = new();

        /// <summary>
        /// Arithmetic postconditions for the action. These will be added to the current value when the action has executed.
        /// </summary>
        private readonly Dictionary<string, object> arithmeticPostconditions = new();

        /// <summary>
        /// Parameter postconditions for the action. When the action has executed, the value of the parameter given in the key will be copied to the state with the name given in the value.
        /// </summary>
        private readonly Dictionary<string, string> parameterPostconditions = new();

        /// <summary>
        /// Parameters to be passed to the action.
        /// </summary>
        private Dictionary<string, object?> parameters = new();

        /// <summary>
        /// Initializes a new instance of the <see cref="Action"/> class.
        /// </summary>
        /// <param name="name">Name for the action, for eventing and logging purposes.</param>
        /// <param name="permutationSelectors">The permutation selector callback for the action's parameters.</param>
        /// <param name="executor">The executor callback for the action.</param>
        /// <param name="cost">Cost of the action.</param>
        /// <param name="costCallback">Callback for determining the cost of the action.</param>
        /// <param name="preconditions">Preconditions required in the world state in order for the action to occur.</param>
        /// <param name="comparativePreconditions">Preconditions indicating relative value requirements needed for the action to occur.</param>
        /// <param name="postconditions">Postconditions applied after the action is successfully executed.</param>
        /// <param name="arithmeticPostconditions">Arithmetic postconditions added to state after the action is successfully executed.</param>
        /// <param name="parameterPostconditions">Parameter postconditions copied to state after the action is successfully executed.</param>
        public Action(string? name = null, Dictionary<string, PermutationSelectorCallback>? permutationSelectors = null, ExecutorCallback? executor = null, float cost = 1f, CostCallback? costCallback = null, Dictionary<string, object?>? preconditions = null, Dictionary<string, ComparisonValuePair>? comparativePreconditions = null, Dictionary<string, object?>? postconditions = null, Dictionary<string, object>? arithmeticPostconditions = null, Dictionary<string, string>? parameterPostconditions = null) {
            if (permutationSelectors == null) this.permutationSelectors = new();
            else this.permutationSelectors = permutationSelectors;
            if (executor == null) this.executor = DefaultExecutorCallback;
            else this.executor = executor;
            Name = name ?? $"Action {Guid.NewGuid()} ({this.executor.GetMethodInfo().Name})";
            this.cost = cost;
            this.costCallback = costCallback ?? DefaultCostCallback;
            if (preconditions != null) this.preconditions = preconditions;
            if (comparativePreconditions != null) this.comparativePreconditions = comparativePreconditions;
            if (postconditions != null) this.postconditions = postconditions;
            if (arithmeticPostconditions != null) this.arithmeticPostconditions = arithmeticPostconditions;
            if (parameterPostconditions != null) this.parameterPostconditions = parameterPostconditions;
        }

        /// <summary>
        /// Event that triggers when an action begins executing.
        /// </summary>
        public static event BeginExecuteActionEvent OnBeginExecuteAction = (agent, action, parameters) => { };

        /// <summary>
        /// Event that triggers when an action finishes executing.
        /// </summary>
        public static event FinishExecuteActionEvent OnFinishExecuteAction = (agent, action, status, parameters) => { };

        /// <summary>
        /// Gets or sets the execution status of the action.
        /// </summary>
        internal ExecutionStatus ExecutionStatus { get; set; } = ExecutionStatus.NotYetExecuted;

        /// <summary>
        /// Makes a copy of the action.
        /// </summary>
        /// <returns>A copy of the action.</returns>
        public Action Copy() {
            var newAction = new Action(Name, permutationSelectors, executor, cost, costCallback, preconditions.Copy(), comparativePreconditions.Copy(), postconditions.Copy(), arithmeticPostconditions.CopyNonNullable(), parameterPostconditions.Copy()) {
                parameters = parameters.Copy()
            };
            return newAction;
        }

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
        /// Gets the cost of the action.
        /// </summary>
        /// <param name="currentState">State as it will be when cost is relevant.</param>
        /// <returns>The cost of the action.</returns>
        public float GetCost(ConcurrentDictionary<string, object?> currentState) {
            try {
                return costCallback(this, currentState);
            }
            catch {
                return float.MaxValue;
            }
        }

        /// <summary>
        /// Executes a step of work for the agent.
        /// </summary>
        /// <param name="agent">Agent executing the action.</param>
        /// <returns>The execution status of the action.</returns>
        internal ExecutionStatus Execute(Agent agent) {
            OnBeginExecuteAction(agent, this, parameters);
            if (IsPossible(agent.State)) {
                var newState = executor(agent, this);
                if (newState == ExecutionStatus.Succeeded) ApplyEffects(agent.State);
                ExecutionStatus = newState;
                OnFinishExecuteAction(agent, this, ExecutionStatus, parameters);
                return newState;
            }
            else {
                OnFinishExecuteAction(agent, this, ExecutionStatus.NotPossible, parameters);
                return ExecutionStatus.NotPossible;
            }
        }

        /// <summary>
        /// Determines whether or not an action is possible.
        /// </summary>
        /// <param name="state">The current world state.</param>
        /// <returns>True if the action is possible, otherwise false.</returns>
        internal bool IsPossible(ConcurrentDictionary<string, object?> state) {
            foreach (var kvp in preconditions) {
                if (!state.ContainsKey(kvp.Key)) return false;
                if (state[kvp.Key] == null && state[kvp.Key] != kvp.Value) return false;
                else if (state[kvp.Key] == null && state[kvp.Key] == kvp.Value) continue;
                if (state[kvp.Key] is object obj && !obj.Equals(kvp.Value)) return false;
            }
            foreach (var kvp in comparativePreconditions) {
                if (!state.ContainsKey(kvp.Key)) return false;
                if (state[kvp.Key] == null) return false;
                if (state[kvp.Key] is object obj && kvp.Value.Value is object obj2) {
                    if (kvp.Value.Operator == ComparisonOperator.LessThan && !Utils.IsLowerThan(obj, obj2)) return false;
                    else if (kvp.Value.Operator == ComparisonOperator.GreaterThan && !Utils.IsHigherThan(obj, obj2)) return false;
                    else if (kvp.Value.Operator == ComparisonOperator.LessThanOrEquals && !Utils.IsLowerThanOrEquals(obj, obj2)) return false;
                    else if (kvp.Value.Operator == ComparisonOperator.GreaterThanOrEquals && !Utils.IsHigherThanOrEquals(obj, obj2)) return false;
                }
                else return false;
            }
            return true;
        }

        /// <summary>
        /// Gets all permutations of parameters possible for an action.
        /// </summary>
        /// <param name="state">World state when the action would be performed.</param>
        /// <returns>A list of possible parameter dictionaries that could be used.</returns>
        internal List<Dictionary<string, object?>> GetPermutations(ConcurrentDictionary<string, object?> state) {
            List<Dictionary<string, object?>> combinedOutputs = new();
            Dictionary<string, List<object>> outputs = new();
            foreach (var kvp in permutationSelectors) outputs[kvp.Key] = kvp.Value(state);
            var permutationParameters = outputs.Keys.ToList();
            List<int> indices = new();
            List<int> counts = new();
            foreach (var parameter in permutationParameters) {
                indices.Add(0);
                if (outputs[parameter].Count == 0) return combinedOutputs;
                counts.Add(outputs[parameter].Count);
            }
            while (true) {
                var singleOutput = new Dictionary<string, object?>();
                for (int i = 0; i < indices.Count; i++) {
                    if (indices[i] >= outputs[permutationParameters[i]].Count) continue;
                    singleOutput[permutationParameters[i]] = outputs[permutationParameters[i]][indices[i]];
                }
                combinedOutputs.Add(singleOutput);
                if (IndicesAtMaximum(indices, counts)) return combinedOutputs;
                IncrementIndices(indices, counts);
            }
        }

        /// <summary>
        /// Applies the effects of the action.
        /// </summary>
        /// <param name="state">World state to which to apply effects.</param>
        internal void ApplyEffects(ConcurrentDictionary<string, object?> state) {
            foreach (var kvp in postconditions) state[kvp.Key] = kvp.Value;
            foreach (var kvp in arithmeticPostconditions) {
                if (!state.ContainsKey(kvp.Key)) continue;
                if (state[kvp.Key] is int stateInt && kvp.Value is int conditionInt) state[kvp.Key] = stateInt + conditionInt;
                else if (state[kvp.Key] is float stateFloat && kvp.Value is float conditionFloat) state[kvp.Key] = stateFloat + conditionFloat;
                else if (state[kvp.Key] is double stateDouble && kvp.Value is double conditionDouble) state[kvp.Key] = stateDouble + conditionDouble;
                else if (state[kvp.Key] is long stateLong && kvp.Value is long conditionLong) state[kvp.Key] = stateLong + conditionLong;
                else if (state[kvp.Key] is decimal stateDecimal && kvp.Value is decimal conditionDecimal) state[kvp.Key] = stateDecimal + conditionDecimal;
                else if (state[kvp.Key] is DateTime stateDateTime && kvp.Value is TimeSpan conditionTimeSpan) state[kvp.Key] = stateDateTime + conditionTimeSpan;
            }
            foreach (var kvp in parameterPostconditions) {
                if (!parameters.ContainsKey(kvp.Key)) continue;
                state[kvp.Value] = parameters[kvp.Key];
            }
        }

        /// <summary>
        /// Sets all parameters to the action.
        /// </summary>
        /// <param name="parameters">Dictionary of parameters to be passed to the action.</param>
        internal void SetParameters(Dictionary<string, object?> parameters) {
            this.parameters = parameters;
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

#pragma warning disable S1172 // Unused method parameters should be removed
        private static float DefaultCostCallback(Action action, ConcurrentDictionary<string, object?> currentState) {
            return action.cost;
        }
#pragma warning restore S1172 // Unused method parameters should be removed
    }
}