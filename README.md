<img src="https://github.com/caesuric/mountain-goap/raw/main/logo.png" alt="Mountain GOAP logo" title="Mountain GOAP" align="right" height="180" />

# Mountain GOAP

Generic C# GOAP (Goal Oriented Action Planning) library for creating AI agents to be used in games. GOAP is a type of an AI system for games popularized by [the F.E.A.R. AI paper](https://alumni.media.mit.edu/~jorkin/gdc2006_orkin_jeff_fear.pdf). GOAP agents use A* pathfinding to plan paths through a series of sequential actions, creating action sequences that allow the agent to achieve its goals.

Mountain GOAP favors composition over inheritance, allowing you to create agents from a series of callbacks. In addition, Mountain GOAP's agents support multiple weighted goals and will attempt to find the greatest utility among a series of goals.

1. [Quickstart](#quickstart)
    1. [Using distributable](#using-distributable)
    2. [Using distributable in Unity](#using-distributable-in-unity)
    3. [Using NuGet package](#using-nuget-package)
    4. [Using as a Unity package](#using-as-a-unity-package)
    5. [Using the code directly](#using-the-code-directly)
    6. [Using the library after installation](#using-the-library-after-installation)
2. [Concepts & API](#concepts--api)
    1. [Agents](#agents)
        1. [Agent state](#agent-state)
    2. [Goals](#goals)
    3. [Actions](#actions)
    4. [Sensors](#sensors)
    5. [Future features - permutation selectors](#future-feature---permutation-selectors)
3. [Examples](#examples)
4. [Project structure](#project-structure)
5. [Roadmap](#roadmap)
6. [Other open source GOAP projects](#other-open-source-goap-projects)

## Quickstart

### Using distributable

Download the [release](https://github.com/caesuric/mountain-goap/releases), unzip, and include the DLL in your project. In Visual Studio, you can do this by right-clicking on "Dependencies" in the Solution Explorer, then clicking "Add COM Reference," clicking "Browse," and browsing to the DLL.

### Using distributable in Unity

Download the [release](https://github.com/caesuric/mountain-goap/releases), unzip to a folder, and drag the folder into your Unity project.

### Using NuGet package

If you are not using Unity, you can download and use MountainGoap as a NuGet package.

Right click your package and click "Manage NuGet Packages," then search for "MountainGoap" and install the package.

### Using as a Unity package

In the works.

### Using the code directly

Clone the repo and copy the code in the MountainGoap folder to your repo.

### Using the library after installation

No matter which method of installation you use, you can access MountainGoap by using the `MountainGoap` namespace as a prefix to the library classes, or by including the following line in your code:

```csharp
using MountainGoap;
```

## Concepts & API

### Agents

**Agents** are indivdual entities that act within your game or simulated world. The simplest example of instantiating an agent is this:
`Agent agent = new Agent();`

In practice, you will want to pass the agent constructor various things it needs to make a functional agent. Read on to understand what kinds of objects you should pass your agents.

When you want your agent to act, just call the following:
`agent.Step();`

What kind of timeframe is represented by a "step" will vary based on your engine. In a turn based game, a step might be one turn. In a realtime engine like Unity, you might call `agent.Step()` on every `Update()` cycle.

#### Agent state

The agent stores a dictionary of objects called its **state**. This state can include anything, but simple values work best with [goals](#goals) and [actions](#actions). If you need to reference complex game state, however, this is not a problem -- [sensors](#sensors), covered below, can be used to translate complex values like map states into simpler ones, like booleans. More on that below.

State can be passed into the agent constructor, like so:
```csharp
Agent agent = new Agent(
    state: new Dictionary<string, object> {
        { "nearOtherAgent", false },
        { "otherAgents", new List<Agent>() }
    }
);
```

### Goals

**Goals** dictate the state values that the agent is trying to achieve. Goals have relatively simple constructors, taking just a dictionary of keys and values the agent wants to see in its state and a weight that indicates how important the goal is. The higher the weight, the more important the goal.

Goals can be passed into the agent constructor, like so:
```csharp
Goal goal = new Goal(
    desiredState: new Dictionary<string, object> {
        { "nearOtherAgent", true }
    },
    weight: 2f
);
Agent agent = new Agent(
    goals: new List<Goal> {
        goal
    }
);
```

### Actions

**Actions** dictate arbitrary code the agent can execute to affect the world and achieve its goals. Each action, when it runs, will execute the code passed to it, which is called the action **executor**. Actions can also have **preconditions**, state values required before the agent is allowed to execute the action, and **postconditions**, which are values the state is expected to hold if the action is successful. Finally, each action has a **cost**, which is used in calculating the best plan for the agent.

Actions return an `ExecutionStatus` enum to say if they succeeded or not. If they succeed, the postconditions will automatically be set to the values passed to the action constructor.

Actions can be passed into the agent constructor, like so:
```csharp
Action giveHugAction = new Action(
    executor: (Agent agent, Action action) => {
        Console.WriteLine("hugged someone");
        return ExecutionStatus.Succeeded
    },
    preconditions: new Dictionary<string, object> {
        { "nearOtherAgent", true }
    },
    postConditions: new Dictionary<string, object> {
        { "wasHugged", true }
    },
    cost: 0.5f
);
Agent agent = new Agent(
    actions: new List<Action> {
        giveHugAction
    }
);
```

### Sensors

**Sensors** allow an agent to distill information into their state, often derived from other state values. Sensors execute on every `Step()` call, and use a **sensor handler** to execute code. Sensors can be passed into the agent constructor, like so:
```csharp
Sensor agentProximitySensor = new Sensor(
    (Agent agent) => {
        if (AgentNearOtherAgent(agent)) agent.State["nearOtherAgent"] = true;
        else agent.State["nearOtherAgent"] = false;
    }
);
Agent agent = new Agent(
    sensors: new List<Sensor> {
        agentProximitySensor
    }
);
```

### Future Feature - Permutation Selectors

Finally, actions can be constructed with **permutation selectors**, which will instantiate multiple copies of the action with different parameters for purposes such as target selection. The library comes with some default permutation selectors, or you can write your own as callbacks. For instance, if you want an action to be evaluated separately with each member of a list as a potential parameter, you would construct the action as so:

```csharp
Action myAction = new Action(
    permutationSelectors: new Dictionary<string, PermutationSelectorCallback> {
        { "target1", PermutationSelectorGenerators.SelectFromCollectionInState<Agent>("otherAgents") },
        { "target2", PermutationSelectorGenerators.SelectFromCollectionInState<Agent>("otherAgents") }
    },
    executor: (Agent agent, Action action) => {
        Console.WriteLine(action.GetParameter("target1").ToString());
        Console.WriteLine(action.GetParameter("target2").ToString());
    }
);
```

The code above will create an action that when evaluated for execution in an agent plan will be considered once for every pair combination of elements in the "otherAgents" collection of the agent state, one for `target1`, and one for `target2`. Note that while this feature has many potential uses down the road, it is not particularly helpful in agent planning until the utility of an action can be calculated via a custom callback function that can be based on action parameters.

## Examples

[Examples documentation](./Examples/examples.md).

1. [Happiness Maximizer Example](./Examples/HappinessIncrementer.cs)
2. [RPG Example](./Examples//RpgExample/RpgExample.cs)

## Project Structure

TO DO

## Roadmap

* Custom action cost override based on parameters
* Examples - general and Unity
* Tests

## Other open source GOAP projects

* [ReGoap](https://github.com/luxkun/ReGoap) - C# GOAP library with more direct Unity support, providing Unity Components that can be attached to GameObjects.
