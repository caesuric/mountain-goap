// <copyright file="ActionNode.cs" company="Chris Muller">
// Copyright (c) Chris Muller. All rights reserved.
// </copyright>

namespace MountainGoap.Async {
    using System.Collections.Concurrent;
    using System.Collections.Generic;
    using Priority_Queue;

    /// <summary>
    /// Represents an action node in an action graph.
    /// </summary>
    public class ActionNode : FastPriorityQueueNode {
        /// <summary>
        /// Initializes a new instance of the <see cref="ActionNode"/> class.
        /// </summary>
        /// <param name="action">Action to be assigned to the node.</param>
        /// <param name="state">State to be assigned to the node.</param>
        /// <param name="parameters">Paramters to be passed to the action in the node.</param>
        internal ActionNode(Action? action, ConcurrentDictionary<string, object?> state, Dictionary<string, object?> parameters) {
            if (action != null) Action = action.Copy();
            State = state.Copy();
            Parameters = parameters.Copy();
            Action?.SetParameters(Parameters);
        }

        /// <summary>
        /// Gets or sets the state of the world for this action node.
        /// </summary>
        public ConcurrentDictionary<string, object?> State { get; set; }

        /// <summary>
        /// Gets or sets parameters to be passed to the action.
        /// </summary>
        public Dictionary<string, object?> Parameters { get; set; }

        /// <summary>
        /// Gets or sets the action to be executed when the world is in the defined <see cref="State"/>.
        /// </summary>
        public Action? Action { get; set; }

#pragma warning disable S3875 // "operator==" should not be overloaded on reference types
        /// <summary>
        /// Overrides the equality operator on ActionNodes.
        /// </summary>
        /// <param name="node1">First node to be compared.</param>
        /// <param name="node2">Second node to be compared.</param>
        /// <returns>True if equal, otherwise false.</returns>
        public static bool operator ==(ActionNode? node1, ActionNode? node2) {
            if (node1 is null) return node2 is null;
            if (node2 is null) return node1 is null;
            if (node1.Action == null || node2.Action == null) return (node1.Action == node2.Action) && node1.StateMatches(node2);
            return node1.Action.Equals(node2.Action) && node1.StateMatches(node2);
        }
#pragma warning restore S3875 // "operator==" should not be overloaded on reference types

        /// <summary>
        /// Overrides the inequality operator on ActionNodes.
        /// </summary>
        /// <param name="node1">First node to be compared.</param>
        /// <param name="node2">Second node to be compared.</param>
        /// <returns>True if unequal, otherwise false.</returns>
        public static bool operator !=(ActionNode? node1, ActionNode? node2) {
            if (node1 is null) return node2 is not null;
            if (node2 is null) return node1 is not null;
            if (node1.Action is not null) return !node1.Action.Equals(node2.Action) || !node1.StateMatches(node2);
            return node2.Action is null;
        }

        /// <inheritdoc/>
        public override bool Equals(object? obj) {
            if (obj is not ActionNode item) return false;
            return this == item;
        }

        /// <inheritdoc/>
        public override int GetHashCode() {
            var hashCode = 629302477;
            if (Action != null) hashCode = (hashCode * -1521134295) + EqualityComparer<Action>.Default.GetHashCode(Action);
            else hashCode *= -1521134295;
            hashCode = (hashCode * -1521134295) + EqualityComparer<ConcurrentDictionary<string, object?>>.Default.GetHashCode(State);
            return hashCode;
        }

        /// <summary>
        /// Cost to traverse this node.
        /// </summary>
        /// <param name="currentState">Current state after previous node is executed.</param>
        /// <returns>The cost of the action to be executed.</returns>
        internal async Task<float> CostAsync(ConcurrentDictionary<string, object?> currentState) {
            if (Action == null) return float.MaxValue;
            return await Action.GetCostAsync(currentState);
        }

        private bool StateMatches(ActionNode otherNode) {
            foreach (var kvp in State) {
                if (!otherNode.State.ContainsKey(kvp.Key)) return false;
                if (otherNode.State[kvp.Key] == null && otherNode.State[kvp.Key] != kvp.Value) return false;
                if (otherNode.State[kvp.Key] == null && otherNode.State[kvp.Key] == kvp.Value) continue;
                if (otherNode.State[kvp.Key] is object obj && !obj.Equals(kvp.Value)) return false;
            }
            foreach (var kvp in otherNode.State) {
                if (!State.ContainsKey(kvp.Key)) return false;
                if (State[kvp.Key] == null && State[kvp.Key] != kvp.Value) return false;
                if (State[kvp.Key] == null && State[kvp.Key] == kvp.Value) continue;
                if (otherNode.State[kvp.Key] is object obj && !obj.Equals(kvp.Value)) return false;
            }
            return true;
        }
    }
}