using System;
using System.Collections.Generic;
using System.IO;

public class GoalManager
{
    private readonly List<Goal> _goals;
    private int _score;

    public GoalManager()
    {
        _goals = new List<Goal>();
        _score = 0;
    }

    public void Start()
    {
        bool running = true;

        while (running)
        {
            Console.WriteLine();
            Console.WriteLine($"You have {_score} points.\n");
            Console.WriteLine("Menu Options:");
            Console.WriteLine(" 1. Display Player Info");
            Console.WriteLine(" 2. List Goal Names");
            Console.WriteLine(" 3. List Goal Details");
            Console.WriteLine(" 4. Create New Goal");
            Console.WriteLine(" 5. Record Event");
            Console.WriteLine(" 6. Save Goals");
            Console.WriteLine(" 7. Load Goals");
            Console.WriteLine(" 8. Quit");
            Console.Write("Select a choice from the menu: ");

            string choice = Console.ReadLine() ?? "";

            switch (choice)
            {
                case "1":
                    DisplayPlayerInfo();
                    break;
                case "2":
                    ListGoalNames();
                    break;
                case "3":
                    ListGoalDetails();
                    break;
                case "4":
                    CreateGoal();
                    break;
                case "5":
                    RecordEvent();
                    break;
                case "6":
                    SaveGoals();
                    break;
                case "7":
                    LoadGoals();
                    break;
                case "8":
                    running = false;
                    break;
                default:
                    Console.WriteLine("Invalid option, try again.");
                    break;
            }
        }
    }

    public void DisplayPlayerInfo()
    {
        int level = GetLevel();
        string title = GetTitleForLevel(level);

        Console.WriteLine();
        Console.WriteLine($"Your current score is: {_score}");
        Console.WriteLine($"Level {level} - {title}");
        Console.WriteLine();
    }

    public void ListGoalNames()
    {
        Console.WriteLine("\nYour Goals:");
        for (int i = 0; i < _goals.Count; i++)
        {
            Console.WriteLine($"{i + 1}. {_goals[i].GetShortName()}");
        }
        if (_goals.Count == 0)
        {
            Console.WriteLine("  (no goals yet)");
        }
    }

    public void ListGoalDetails()
    {
        Console.WriteLine("\nGoal Details:");
        if (_goals.Count == 0)
        {
            Console.WriteLine("  (no goals yet)");
            return;
        }

        foreach (Goal goal in _goals)
        {
            Console.WriteLine(goal.GetDetailsString());
        }
    }

    public void CreateGoal()
    {
        Console.WriteLine("\nThe types of Goals are:");
        Console.WriteLine(" 1. Simple Goal");
        Console.WriteLine(" 2. Eternal Goal");
        Console.WriteLine(" 3. Checklist Goal");
        Console.Write("Which type of goal would you like to create? ");

        string typeChoice = Console.ReadLine() ?? "";

        Console.Write("What is the name of your goal? ");
        string name = Console.ReadLine() ?? "";

        Console.Write("What is a short description of it? ");
        string description = Console.ReadLine() ?? "";

        Console.Write("What is the amount of points associated with this goal? ");
        int points = int.Parse(Console.ReadLine() ?? "0");

        switch (typeChoice)
        {
            case "1":
                _goals.Add(new SimpleGoal(name, description, points));
                break;

            case "2":
                _goals.Add(new EternalGoal(name, description, points));
                break;

            case "3":
                Console.Write("How many times does this goal need to be accomplished for a bonus? ");
                int target = int.Parse(Console.ReadLine() ?? "0");

                Console.Write("What is the bonus for accomplishing this goal that many times? ");
                int bonus = int.Parse(Console.ReadLine() ?? "0");

                _goals.Add(new ChecklistGoal(name, description, points, target, bonus));
                break;

            default:
                Console.WriteLine("Unknown goal type.");
                break;
        }
    }

    public void RecordEvent()
    {
        if (_goals.Count == 0)
        {
            Console.WriteLine("\nYou have no goals to record yet.");
            return;
        }

        Console.WriteLine("\nThe goals are:");
        for (int i = 0; i < _goals.Count; i++)
        {
            Console.WriteLine($"{i + 1}. {_goals[i].GetShortName()}");
        }

        Console.Write("Which goal did you accomplish? ");
        int index = int.Parse(Console.ReadLine() ?? "0") - 1;

        if (index < 0 || index >= _goals.Count)
        {
            Console.WriteLine("Invalid goal selection.");
            return;
        }

        int oldLevel = GetLevel();

        Goal goal = _goals[index];
        int earned = goal.RecordEvent();
        _score += earned;

        Console.WriteLine($"\nCongratulations! You earned {earned} points.");
        Console.WriteLine($"You now have {_score} points.");

        int newLevel = GetLevel();
        if (newLevel > oldLevel)
        {
            string title = GetTitleForLevel(newLevel);
            Console.WriteLine($"\n LEVEL UP! You are now Level {newLevel}: {title} ");
        }
    }


    public void SaveGoals()
    {
        Console.Write("\nWhat is the filename for the goal file? ");
        string filename = Console.ReadLine() ?? "goals.txt";

        using (StreamWriter writer = new StreamWriter(filename))
        {
            writer.WriteLine(_score);

            foreach (Goal goal in _goals)
            {
                writer.WriteLine(goal.GetStringRepresentation());
            }
        }

        Console.WriteLine("Goals saved successfully.");
    }

    public void LoadGoals()
    {
        Console.Write("\nWhat is the filename for the goal file? ");
        string filename = Console.ReadLine() ?? "goals.txt";

        if (!File.Exists(filename))
        {
            Console.WriteLine("File not found.");
            return;
        }

        string[] lines = File.ReadAllLines(filename);
        if (lines.Length == 0)
        {
            Console.WriteLine("File is empty.");
            return;
        }

        _goals.Clear();
        _score = int.Parse(lines[0]);

        for (int i = 1; i < lines.Length; i++)
        {
            if (string.IsNullOrWhiteSpace(lines[i])) continue;

            string[] typeAndData = lines[i].Split(':');
            if (typeAndData.Length != 2) continue;

            string type = typeAndData[0];
            string[] parts = typeAndData[1].Split('|');

            if (type == "SimpleGoal")
            {
                string name = parts[0];
                string desc = parts[1];
                int points = int.Parse(parts[2]);
                bool isComplete = bool.Parse(parts[3]);

                _goals.Add(new SimpleGoal(name, desc, points, isComplete));
            }
            else if (type == "EternalGoal")
            {
                string name = parts[0];
                string desc = parts[1];
                int points = int.Parse(parts[2]);

                _goals.Add(new EternalGoal(name, desc, points));
            }
            else if (type == "ChecklistGoal")
            {
                string name = parts[0];
                string desc = parts[1];
                int points = int.Parse(parts[2]);
                int amountCompleted = int.Parse(parts[3]);
                int target = int.Parse(parts[4]);
                int bonus = int.Parse(parts[5]);

                _goals.Add(new ChecklistGoal(name, desc, points, amountCompleted, target, bonus));
            }
        }

        Console.WriteLine("Goals loaded successfully.");
    }

    private int GetLevel()
    {
        // Every 1000 points is a new level
        return (_score / 1000) + 1;
    }

    private string GetTitleForLevel(int level)
    {
        if (level <= 1) return "Novice Seeker";
        if (level == 2) return "Apprentice Adventurer";
        if (level == 3) return "Faithful Knight";
        if (level == 4) return "Eternal Champion";
        return "Legendary Hero";
    }
}
