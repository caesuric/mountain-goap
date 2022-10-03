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
            var demo1Command = new Command("demo1", "Run the first demo.");
            demo1Command.SetHandler(() => {
                RunDemo1();
            });
            var cmd = new RootCommand {
                demo1Command
            };
            return await cmd.InvokeAsync(args);
        }

        private static void RunDemo1() {
            Demo1.Run();
        }
    }
}
