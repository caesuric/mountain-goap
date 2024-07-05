// <copyright file="Agent.cs" company="Chris Muller">
// Copyright (c) Chris Muller. All rights reserved.
// </copyright>

namespace MountainGoap {
    using System;
    using System.Collections.Concurrent;
    using System.Collections.Generic;
    using System.Threading;

    /// <summary>
    /// GOAP agent.
    /// </summary>
    public class AgentAsync : IAgent
    {
        /// <summary>
        /// Name of the agent.
        /// </summary>
        public string Name { get; }

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
        public AgentAsync(string? name = null, ConcurrentDictionary<string, object?>? state = null, Dictionary<string, object?>? memory = null, List<BaseGoal>? goals = null, List<ActionAsync>? actions = null, List<SensorAsync>? sensors = null, float costMaximum = float.MaxValue, int stepMaximum = int.MaxValue) {
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
        public static event AgentStepAsyncEvent OnAgentStep = (agent) => Task.CompletedTask;

        /// <summary>
        /// Event that fires when an action sequence completes.
        /// </summary>
        public static event AgentActionSequenceCompletedAsyncEvent OnAgentActionSequenceCompleted = (agent) => Task.CompletedTask;

        /// <summary>
        /// Gets the chains of actions currently being performed by the agent.
        /// </summary>
        public List<List<ActionAsync>> CurrentActionSequences { get; } = new();

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
        public List<ActionAsync> Actions { get; set; } = new();

        public List<IAction> ActionList => Actions.OfType<IAction>().ToList();

        /// <summary>
        /// Gets or sets the sensors available to the agent.
        /// </summary>
        public List<SensorAsync> Sensors { get; set; } = new();

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
        
        public void AddCurrentActionSequences(List<IAction> actionList)
        {
            CurrentActionSequences.Add(actionList.OfType<ActionAsync>().ToList());
        }

        /// <summary>
        /// You should call this every time your game state updates.
        /// </summary>
        /// <param name="mode">Mode to be used for executing the step of work.</param>
        public async Task StepAsync(StepMode mode = StepMode.Default) {
            await OnAgentStep(this);
            foreach (var sensor in Sensors) 
                await sensor.RunAsync(this);
            
            if (mode == StepMode.Default) {
                await InnerStepAsync();
                return;
            }
            if (!IsBusy) await Task.Run(() => Planner.Plan(this, CostMaximum, StepMaximum));
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
                await Task.Run(() => Planner.Plan(this, CostMaximum, StepMaximum));
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
        /// Executes an asynchronous step of agent work.
        /// </summary>
        private async Task InnerStepAsync() {
            if (!IsBusy && !IsPlanning) {
                IsPlanning = true;
                await Task.Run(() => Planner.Plan(this, CostMaximum, StepMaximum));
                IsPlanning = false;
            }
            else if (!IsPlanning) await ExecuteAsync();
        }

        /// <summary>
        /// Executes the current action sequences.
        /// </summary>
        private async Task ExecuteAsync() {
            if (CurrentActionSequences.Count > 0) {
                List<List<ActionAsync>> cullableSequences = new();
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
