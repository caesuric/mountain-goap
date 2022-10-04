// <copyright file="Program.cs" company="Chris Muller">
// Copyright (c) Chris Muller. All rights reserved.
// </copyright>

 namespace Examples {
    using System.CommandLine;

    /// <summary>
    /// Runs MountainGoap Demos.
    /// </summary>
    public class Program {
        /// <summary>
        /// The main program entry point.
        /// </summary>
        /// <param name="args">Arguments to the application.</param>
        /// <returns>An exit code.</returns>
        public static async Task<int> Main(string[] args) {
            var happinessIncrementerCommand = new Command("happiness", "Run the happiness incrementer demo.");
            happinessIncrementerCommand.SetHandler(() => {
                RunHappinessIncrementer();
            });
            var rpgCommand = new Command("rpg", "Run the RPG enemy demo.");
            happinessIncrementerCommand.SetHandler(() => {
                RunRpgEnemyDemo();
            });
            var cmd = new RootCommand {
                happinessIncrementerCommand
            };
            return await cmd.InvokeAsync(args);
        }

        private static void RunHappinessIncrementer() {
            HappinessIncrementer.Run();
        }

        private static void RunRpgEnemyDemo() {
            RpgEnemy.Run();
        }
    }
}
