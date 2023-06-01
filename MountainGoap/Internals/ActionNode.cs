// <copyright file="ActionNode.cs" company="Chris Muller">
// Copyright (c) Chris Muller. All rights reserved.
// </copyright>

namespace MountainGoap {
    using Priority_Queue;
    using System.Collections.Generic;

    /// <summary>
    /// Represents an action node in an action graph.
    /// </summary>
    internal class ActionNode : FastPriorityQueueNode {
        /// <summary>
        /// The state of the world for this action node.
        /// </summary>
        internal Dictionary<string, object> State = new();

        /// <summary>
        /// Parameters to be passed to the action.
        /// </summary>
        internal Dictionary<string, object> Parameters = new();

        /// <summary>
        /// The action to be executed when the world is in the defined <see cref="State"/>.
        /// </summary>
        internal Action? Action;

        /// <summary>
        /// Initializes a new instance of the <see cref="ActionNode"/> class.
        /// </summary>
        /// <param name="action">Action to be assigned to the node.</param>
        /// <param name="state">State to be assigned to the node.</param>
        /// <param name="parameters">Paramters to be passed to the action in the node.</param>
        internal ActionNode(Action? action, Dictionary<string, object> state, Dictionary<string, object> parameters) {
            if (action != null) Action = action.Copy();
            State = state.Copy();
            Parameters = parameters.Copy();
            if (Action != null) Action.SetParameters(Parameters);
        }

        public static bool operator ==(ActionNode? node1, ActionNode? node2) {
            if (node1 is null) return node2 is null;
            if (node2 is null) return node1 is null;
            if (node1.Action == null || node2.Action == null) return (node1.Action == node2.Action) && node1.StateMatches(node2);
            return node1.Action.Equals(node2.Action) && node1.StateMatches(node2);
        }

        public static bool operator !=(ActionNode? node1, ActionNode? node2) {
            if (node1 is null) return node2 is not null;
            if (node2 is null) return node1 is not null;
            if (node1.Action is not null) return !node1.Action.Equals(node2.Action) || !node1.StateMatches(node2);
            return node2.Action is null;
        }

        /// <inheritdoc/>
        public override bool Equals(object? o) {
            if (o is not ActionNode item) return false;
            return this == item;
        }

        /// <inheritdoc/>
        public override int GetHashCode() {
            var hashCode = 629302477;
            if (Action != null) hashCode = (hashCode * -1521134295) + EqualityComparer<Action>.Default.GetHashCode(Action);
            else hashCode *= -1521134295;
            hashCode = (hashCode * -1521134295) + EqualityComparer<Dictionary<string, object>>.Default.GetHashCode(State);
            return hashCode;
        }

        /// <summary>
        /// Cost to traverse this node.
        /// </summary>
        /// <returns>The cost of the action to be executed.</returns>
        internal float Cost() {
            if (Action == null) return float.MaxValue;
            return Action.GetCost();
        }

        private bool StateMatches(ActionNode otherNode) {
            foreach (var kvp in State) {
                if (!otherNode.State.ContainsKey(kvp.Key)) return false;
                if (otherNode.State[kvp.Key] == null && otherNode.State[kvp.Key] != kvp.Value) return false;
                if (otherNode.State[kvp.Key] == null && otherNode.State[kvp.Key] == kvp.Value) continue;
                if (!otherNode.State[kvp.Key].Equals(kvp.Value)) return false;
            }
            foreach (var kvp in otherNode.State) {
                if (!State.ContainsKey(kvp.Key)) return false;
                if (State[kvp.Key] == null && State[kvp.Key] != kvp.Value) return false;
                if (State[kvp.Key] == null && State[kvp.Key] == kvp.Value) continue;
                if (!State[kvp.Key].Equals(kvp.Value)) return false;
            }
            return true;
        }
    }
}