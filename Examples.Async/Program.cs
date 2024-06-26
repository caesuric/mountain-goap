// <copyright file="Program.cs" company="Chris Muller">
// Copyright (c) Chris Muller. All rights reserved.
// </copyright>

 namespace Examples.Async {
    using System.CommandLine;

    /// <summary>
    /// Runs MountainGoap.Async Demos.
    /// </summary>
    public static class Program {
        /// <summary>
        /// The main program entry point.
        /// </summary>
        /// <param name="args">Arguments to the application.</param>
        /// <returns>An exit code.</returns>
        public static async Task<int> Main(string[] args) {
            var happinessIncrementerCommand = new Command("happiness", "Run the happiness incrementer demo.");
            happinessIncrementerCommand.SetHandler(RunHappinessIncrementer);
            var rpgCommand = new Command("rpg", "Run the RPG enemy demo.");
            rpgCommand.SetHandler(RunRpgEnemyDemo);
            var arithmeticHappinessIncrementerCommand = new Command("arithmeticHappiness", "Run the arithmetic happiness incrementer demo.");
            arithmeticHappinessIncrementerCommand.SetHandler(RunArithmeticHappinessIncrementer);
            var extremeHappinessIncrementerCommand = new Command("extremeHappiness", "Run the extreme happiness incrementer demo.");
            extremeHappinessIncrementerCommand.SetHandler(RunExtremeHappinessIncrementer);
            var comparativeHappinessIncrementerCommand = new Command("comparativeHappiness", "Run the comparative happiness incrementer demo.");
            comparativeHappinessIncrementerCommand.SetHandler(RunComparativeHappinessIncrementer);
            var carCommand = new Command("car", "Run the car demo.");
            carCommand.SetHandler(RunCarDemo);
            var consumerCommand = new Command("consumer", "Run the consumer demo.");
            consumerCommand.SetHandler(RunConsumerDemo);
            var cmd = new RootCommand {
                happinessIncrementerCommand,
                rpgCommand,
                arithmeticHappinessIncrementerCommand,
                extremeHappinessIncrementerCommand,
                comparativeHappinessIncrementerCommand,
                carCommand,
                consumerCommand
            };
            return await cmd.InvokeAsync(args);
        }

        private static async Task RunHappinessIncrementer() {
            await HappinessIncrementer.Run();
        }

        private static async Task RunRpgEnemyDemo() {
            await RpgExample.Run();
        }

        private static async Task RunArithmeticHappinessIncrementer() {
            await ArithmeticHappinessIncrementer.Run();
        }

        private static async Task RunExtremeHappinessIncrementer() {
            await ExtremeHappinessIncrementer.Run();
        }

        private static async Task RunComparativeHappinessIncrementer() {
            await ComparativeHappinessIncrementer.Run();
        }

        private static async Task RunCarDemo() {
            await CarDemo.Run();
        }

        private static async Task RunConsumerDemo() {
            await ConsumerDemo.Run();
        }
    }
}
