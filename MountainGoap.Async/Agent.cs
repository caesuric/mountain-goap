// <copyright file="Agent.cs" company="Chris Muller">
// Copyright (c) Chris Muller. All rights reserved.
// </copyright>

namespace MountainGoap.Async {
    using System;
    using System.Collections.Concurrent;
    using System.Collections.Generic;
    using System.Threading;

    /// <summary>
    /// GOAP agent.
    /// </summary>
    public class Agent {
        /// <summary>
        /// Name of the agent.
        /// </summary>
        public readonly string Name;

        /// <summary>
        /// Initializes a new instance of the <see cref="Agent"/> class.
        /// </summary>
        /// <param name="name">Name of the agent.</param>
        /// <param name="state">Initial agent state.</param>
        /// <param name="memory">Initial agent memory.</param>
        /// <param name="goals">Initial agent goals.</param>
        /// <param name="actions">Actions available to the agent.</param>
        /// <param name="sensors">Sensors available to the agent.</param>
        /// <param name="costMaximum">Maximum cost of an allowable plan.</param>
        /// <param name="stepMaximum">Maximum steps in an allowable plan.</param>
        public Agent(string? name = null, ConcurrentDictionary<string, object?>? state = null, Dictionary<string, object?>? memory = null, List<BaseGoal>? goals = null, List<Action>? actions = null, List<Sensor>? sensors = null, float costMaximum = float.MaxValue, int stepMaximum = int.MaxValue) {
            Name = name ?? $"Agent {Guid.NewGuid()}";
            if (state != null) State = state;
            if (memory != null) Memory = memory;
            if (goals != null) Goals = goals;
            if (actions != null) Actions = actions;
            if (sensors != null) Sensors = sensors;
            CostMaximum = costMaximum;
            StepMaximum = stepMaximum;
        }

        /// <summary>
        /// Event that fires when the agent executes a step of work.
        /// </summary>
        public static event AgentStepEvent OnAgentStep = (agent) => Task.CompletedTask;

        /// <summary>
        /// Event that fires when an action sequence completes.
        /// </summary>
        public static event AgentActionSequenceCompletedEvent OnAgentActionSequenceCompleted = (agent) => Task.CompletedTask;

        /// <summary>
        /// Event that fires when planning begins.
        /// </summary>
        public static event PlanningStartedEvent OnPlanningStarted = (agent) => Task.CompletedTask;

        /// <summary>
        /// Event that fires when planning for a single goal starts.
        /// </summary>
        public static event PlanningStartedForSingleGoalEvent OnPlanningStartedForSingleGoal = (agent, goal) => Task.CompletedTask;

        /// <summary>
        /// Event that fires when planning for a single goal finishes.
        /// </summary>
        public static event PlanningFinishedForSingleGoalEvent OnPlanningFinishedForSingleGoal = (agent, goal, utility) => Task.CompletedTask;

        /// <summary>
        /// Event that fires when planning finishes.
        /// </summary>
        public static event PlanningFinishedEvent OnPlanningFinished = (agent, goal, utility) => Task.CompletedTask;

        /// <summary>
        /// Event that fires when a new plan is finalized for the agent.
        /// </summary>
        public static event PlanUpdatedEvent OnPlanUpdated = (agent, actionList) => Task.CompletedTask;

        /// <summary>
        /// Event that fires when the pathfinder evaluates a single node in the action graph.
        /// </summary>
        public static event EvaluatedActionNodeEvent OnEvaluatedActionNode = (node, nodes) => Task.CompletedTask;

        /// <summary>
        /// Gets the chains of actions currently being performed by the agent.
        /// </summary>
        public List<List<Action>> CurrentActionSequences { get; } = new();

        /// <summary>
        /// Gets or sets the current world state from the agent perspective.
        /// </summary>
        public ConcurrentDictionary<string, object?> State { get; set; } = new();

        /// <summary>
        /// Gets or sets the memory storage object for the agent.
        /// </summary>
        public Dictionary<string, object?> Memory { get; set; } = new();

        /// <summary>
        /// Gets or sets the list of active goals for the agent.
        /// </summary>
        public List<BaseGoal> Goals { get; set; } = new();

        /// <summary>
        /// Gets or sets the actions available to the agent.
        /// </summary>
        public List<Action> Actions { get; set; } = new();

        /// <summary>
        /// Gets or sets the sensors available to the agent.
        /// </summary>
        public List<Sensor> Sensors { get; set; } = new();

        /// <summary>
        /// Gets or sets the plan cost maximum for the agent.
        /// </summary>
        public float CostMaximum { get; set; }

        /// <summary>
        /// Gets or sets the step maximum for the agent.
        /// </summary>
        public int StepMaximum { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the agent is currently executing one or more actions.
        /// </summary>
        public bool IsBusy { get; set; } = false;

        /// <summary>
        /// Gets or sets a value indicating whether the agent is currently planning.
        /// </summary>
        public bool IsPlanning { get; set; } = false;

        /// <summary>
        /// You should call this every time your game state updates.
        /// </summary>
        /// <param name="mode">Mode to be used for executing the step of work.</param>
        /// <returns>Async Plan.</returns>
        public async Task StepAsync(StepMode mode = StepMode.Default) {
            await OnAgentStep(this);
            foreach (var sensor in Sensors) sensor.Run(this);
            if (mode == StepMode.Default) {
                await InnerStepAsync();
                return;
            }
            if (!IsBusy) await Planner.PlanAsync(this, CostMaximum, StepMaximum);
            if (mode == StepMode.OneAction) await ExecuteAsync();
            else if (mode == StepMode.AllActions) while (IsBusy) await ExecuteAsync();
        }

        /// <summary>
        /// Clears the current action sequences (also known as plans).
        /// </summary>
        public void ClearPlan() {
            CurrentActionSequences.Clear();
        }

        /// <summary>
        /// Makes a plan asynchronously.
        /// </summary>
        /// <returns>Async Plan.</returns>
        public async Task PlanAsync() {
            if (!IsBusy && !IsPlanning) {
                IsPlanning = true;
                await Planner.PlanAsync(this, CostMaximum, StepMaximum);
                IsPlanning = false;
            }
        }

        /// <summary>
        /// Executes the current plan.
        /// </summary>
        /// <returns>Async Plan.</returns>
        public Task ExecutePlanAsync() {
            if (!IsPlanning) return ExecuteAsync();
            return Task.CompletedTask;
        }

        /// <summary>
        /// Triggers OnPlanningStarted event.
        /// </summary>
        /// <param name="agent">Agent that started planning.</param>
        /// <returns>Async Plan.</returns>
        internal static async Task TriggerOnPlanningStarted(Agent agent) {
            await OnPlanningStarted(agent);
        }

        /// <summary>
        /// Triggers OnPlanningStartedForSingleGoal event.
        /// </summary>
        /// <param name="agent">Agent that started planning.</param>
        /// <param name="goal">Goal for which planning was started.</param>
        /// <returns>Async Plan.</returns>
        internal static async Task TriggerOnPlanningStartedForSingleGoal(Agent agent, BaseGoal goal) {
            await OnPlanningStartedForSingleGoal(agent, goal);
        }

        /// <summary>
        /// Triggers OnPlanningFinishedForSingleGoal event.
        /// </summary>
        /// <param name="agent">Agent that finished planning.</param>
        /// <param name="goal">Goal for which planning was completed.</param>
        /// <param name="utility">Utility of the plan.</param>
        /// <returns>Async Plan.</returns>
        internal static async Task TriggerOnPlanningFinishedForSingleGoal(Agent agent, BaseGoal goal, float utility) {
            await OnPlanningFinishedForSingleGoal(agent, goal, utility);
        }

        /// <summary>
        /// Triggers OnPlanningFinished event.
        /// </summary>
        /// <param name="agent">Agent that finished planning.</param>
        /// <param name="goal">Goal that was selected.</param>
        /// <param name="utility">Utility of the plan.</param>
        /// <returns>Async Plan.</returns>
        internal static async Task TriggerOnPlanningFinished(Agent agent, BaseGoal? goal, float utility) {
            await OnPlanningFinished(agent, goal, utility);
        }

        /// <summary>
        /// Triggers OnPlanUpdated event.
        /// </summary>
        /// <param name="agent">Agent for which the plan was updated.</param>
        /// <param name="actionList">New action list for the agent.</param>
        /// <returns>Async Plan.</returns>
        internal static async Task TriggerOnPlanUpdated(Agent agent, List<Action> actionList) {
            await OnPlanUpdated(agent, actionList);
        }

        /// <summary>
        /// Triggers OnEvaluatedActionNode event.
        /// </summary>
        /// <param name="node">Action node being evaluated.</param>
        /// <param name="nodes">List of nodes in the path that led to this point.</param>
        /// <returns>Async Plan.</returns>
        internal static async Task TriggerOnEvaluatedActionNode(ActionNode node, ConcurrentDictionary<ActionNode, ActionNode> nodes) {
            await OnEvaluatedActionNode(node, nodes);
        }

        /// <summary>
        /// Executes an asynchronous step of agent work.
        /// </summary>
        private async Task InnerStepAsync() {
            if (!IsBusy && !IsPlanning) {
                IsPlanning = true;
                await Planner.PlanAsync(this, CostMaximum, StepMaximum);
                IsPlanning = false;
            }
            else if (!IsPlanning) await ExecuteAsync();
        }

        /// <summary>
        /// Executes the current action sequences.
        /// </summary>
        private async Task ExecuteAsync() {
            if (CurrentActionSequences.Count > 0) {
                List<List<Action>> cullableSequences = new();
                foreach (var sequence in CurrentActionSequences) {
                    if (sequence.Count > 0) {
                        var executionStatus = await sequence[0].ExecuteAsync(this);
                        if (executionStatus != ExecutionStatus.Executing) sequence.RemoveAt(0);
                    }
                    else cullableSequences.Add(sequence);
                }
                foreach (var sequence in cullableSequences) {
                    CurrentActionSequences.Remove(sequence);
                    await OnAgentActionSequenceCompleted(this);
                }
            }
            else IsBusy = false;
        }
    }
}
