using System;
using System.IO;
using System.Threading;

public class Activity
{
    private string _name;
    private string _description;
    private int _duration;   // seconds

    protected int Duration => _duration;
    protected string ActivityName => _name;

    public Activity(string name, string description)
    {
        _name = name;
        _description = description;
    }

    public void DisplayStartingMessage()
    {
        Console.Clear();
        Console.WriteLine($"Welcome to the {_name} Activity.");
        Console.WriteLine();
        Console.WriteLine(_description);
        Console.WriteLine();

        Console.Write("How long, in seconds, would you like your session? ");
        while (true)
        {
            string input = Console.ReadLine();
            if (int.TryParse(input, out int seconds) && seconds > 0)
            {
                _duration = seconds;
                break;
            }

            Console.Write("Please enter a positive whole number of seconds: ");
        }

        Console.WriteLine();
        Console.WriteLine("Get ready to begin...");
        ShowSpinner(3);
        Console.WriteLine();
    }

    public void DisplayEndingMessage()
    {
        Console.WriteLine();
        Console.WriteLine("Well done! You have completed the activity.");
        ShowSpinner(3);

        Console.WriteLine();
        Console.WriteLine($"You have completed the {_name} activity for {_duration} seconds.");
        ShowSpinner(3);
    }

    public void ShowSpinner(int seconds)
    {
        DateTime endTime = DateTime.Now.AddSeconds(seconds);
        char[] sequence = new char[] { '|', '/', '-', '\\' };
        int index = 0;

        while (DateTime.Now < endTime)
        {
            char c = sequence[index];
            Console.Write(c);
            Thread.Sleep(250);
            Console.Write("\b \b");   // erase character
            index = (index + 1) % sequence.Length;
        }
    }

    public void ShowCountDown(int seconds)
    {
        for (int i = seconds; i > 0; i--)
        {
            Console.Write(i);
            Thread.Sleep(1000);
            Console.Write("\b \b");
        }
        Console.WriteLine();
    }

    /// Logs the session to a simple text file (exceeding requirement).

    protected void LogSession(string extraDetails = "")
    {
        string path = "mindfulness_log.txt";
        string line =
            $"{DateTime.Now:yyyy-MM-dd HH:mm:ss}\t{ActivityName}\t{Duration}\t{extraDetails}";
        try
        {
            File.AppendAllLines(path, new[] { line });
        }
        catch
        {
            // If logging fails, silently ignore it for now.
        }
    }
}
