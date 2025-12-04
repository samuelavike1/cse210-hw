using System;

// Exceeding Requirements / Creativity:
// I added a simple gamification system that calculates a player "Level"
// based on total score (every 1000 points is a new level). Each level has
// a title (e.g., "Novice Seeker", "Apprentice Adventurer", "Legendary Hero"),
// and whenever the user earns enough points to reach a new level, the
// program displays a special "LEVEL UP" message with the new title.

class Program
{
    static void Main(string[] args)
    {
        GoalManager manager = new GoalManager();
        manager.Start();
    }
}