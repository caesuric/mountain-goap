// <copyright file="RpgExample.cs" company="Chris Muller">
// Copyright (c) Chris Muller. All rights reserved.
// </copyright>

namespace Examples.Async {
    using System.Numerics;
    using MountainGoap.Async;
    using MountainGoapLogging.Async;

    /// <summary>
    /// RPG example demo.
    /// </summary>
    internal static class RpgExample {
        /// <summary>
        /// Maximum X value of the world grid.
        /// </summary>
        internal static readonly int MaxX = 20;

        /// <summary>
        /// Maximum Y value of the world grid.
        /// </summary>
        internal static readonly int MaxY = 20;

        /// <summary>
        /// Runs the demo.
        /// </summary>
        /// <returns>Async Task.</returns>
        internal static async Task Run() {
            _ = new DefaultLogger(logToConsole: false, loggingFile: "rpg-example.log");
            Random random = new();
            List<Agent> agents = new();
            List<Vector2> foodPositions = new();
            var player = RpgCharacterFactory.Create(agents);
            player.State["faction"] = "player";
            agents.Add(player);
            for (int i = 0; i < 20; i++) foodPositions.Add(new Vector2(random.Next(0, MaxX), random.Next(0, MaxY)));
            for (int i = 0; i < 10; i++) {
                var monster = RpgMonsterFactory.Create(agents, foodPositions);
                monster.State["position"] = new Vector2(random.Next(0, MaxX), random.Next(0, MaxY));
                agents.Add(monster);
            }
            for (int i = 0; i < 600; i++) {
                foreach (var agent in agents) await agent.StepAsync(mode: StepMode.OneAction);
                ProcessDeaths(agents);
                PrintGrid(agents, foodPositions);
                await Task.Delay(200);
            }
        }

        private static void PrintGrid(List<Agent> agents, List<Vector2> foodPositions) {
            Console.SetCursorPosition(0, 0);
            string[,] grid = new string[MaxX, MaxY];
            for (int x = 0; x < MaxX; x++) {
                for (int y = 0; y < MaxY; y++) {
                    grid[x, y] = " ";
                }
            }
            foreach (var position in foodPositions) grid[(int)position.X, (int)position.Y] = "f";
            agents.ForEach((agent) => {
                if (agent.State["position"] is Vector2 position && agent.State["faction"] is string faction) {
                    if (faction == "player") grid[(int)position.X, (int)position.Y] = "@";
                    else grid[(int)position.X, (int)position.Y] = "g";
                }
            });
            for (int x = 0; x < MaxX; x++) {
                for (int y = 0; y < MaxY; y++) Console.Write(grid[x, y]);
                Console.WriteLine();
            }
        }

        private static void ProcessDeaths(List<Agent> agents) {
            List<Agent> cullList = new();
            foreach (var agent in agents) if (agent.State["hp"] is int hp && hp <= 0) cullList.Add(agent);
            foreach (var agent in cullList) agents.Remove(agent);
        }
    }
}
