using System;
using System.Collections.Generic;

public class ReflectingActivity : Activity
{
    private List<string> _prompts;
    private List<string> _questions;
    private Random _random = new Random();

    public ReflectingActivity()
        : base(
            "Reflecting",
            "This activity will help you reflect on times in your life when " +
            "you have shown strength and resilience. This will help you recognize " +
            "the power you have and how you can use it in other aspects of your life.")
    {
        _prompts = new List<string>
        {
            "Think of a time when you stood up for someone else.",
            "Think of a time when you did something really difficult.",
            "Think of a time when you helped someone in need.",
            "Think of a time when you did something truly selfless."
        };

        _questions = new List<string>
        {
            "Why was this experience meaningful to you?",
            "Have you ever done anything like this before?",
            "How did you get started?",
            "How did you feel when it was complete?",
            "What made this time different than other times when you were not as successful?",
            "What is your favorite thing about this experience?",
            "What could you learn from this experience that applies to other situations?",
            "What did you learn about yourself through this experience?",
            "How can you keep this experience in mind in the future?"
        };
    }

    public void Run()
    {
        DisplayStartingMessage();

        DisplayPrompt();
        Console.WriteLine("When you have something in mind, press enter to continue.");
        Console.ReadLine();

        Console.Write("Now ponder on each of the following questions as they relate to this experience.");
        Console.WriteLine(" You will have a few seconds to think after each question.");
        Console.Write("You may begin in: ");
        ShowCountDown(5);

        int questionsAsked = DisplayQuestions();

        LogSession($"QuestionsAsked={questionsAsked}");
        DisplayEndingMessage();
    }

    private string GetRandomPrompt()
    {
        int index = _random.Next(_prompts.Count);
        return _prompts[index];
    }

    private void DisplayPrompt()
    {
        string prompt = GetRandomPrompt();
        Console.WriteLine();
        Console.WriteLine("Consider the following prompt:");
        Console.WriteLine($" --- {prompt} ---");
        Console.WriteLine();
    }

    /// <summary>
    /// Shows questions without repeating any until all have been used at least once.
    /// This satisfies one of the "exceeding requirement" ideas.
    /// </summary>
    private int DisplayQuestions()
    {
        DateTime endTime = DateTime.Now.AddSeconds(Duration);
        List<string> remaining = new List<string>(_questions);
        int askedCount = 0;

        while (DateTime.Now < endTime)
        {
            if (remaining.Count == 0)
            {
                // All questions have been used once; reset for another round.
                remaining = new List<string>(_questions);
            }

            int index = _random.Next(remaining.Count);
            string question = remaining[index];
            remaining.RemoveAt(index);

            Console.Write($"> {question} ");
            ShowSpinner(8);  // 8 seconds to think
            Console.WriteLine();
            askedCount++;
        }

        return askedCount;
    }
}
