# Mountain GOAP Examples

## Happiness Incrementer

An extremely barebones example that increments the agent's happiness on each step. The agent acts until its happiness value reaches 10.

## RPG Example

A more complex example with a "player character," enemies, and food. The scenario takes place on a 20x20 grid. Each character in the scenario has a viewing radius of 5 squares. The player character has 10 HP and will attack any enemy in sight. Enemies have 2 HP and will also attack the player character, but if the player character is not in sight, they will seek out and eat food found on the map. The scenario will run for 2 minutes or until terminated.

This scenario allows you to see a basic example of how different goals interact with each other, with the enemy goal of killing the player character taking priority but falling back to food-related action sequences if no player is in sight.
