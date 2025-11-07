using System;

// W02 Project: Journal Program
// Exceeds requirements (brief notes):
// - Added extra prompts above the minimum.
// - Robust file handling with clear messages.
// - Safe custom separator and newline cleaning in saved data.
// If you used any web page for help, paste links here.


class Program
{
    static void Main(string[] args)
    {
        var journal = new Journal();
        var promptGen = new PromptGenerator();

        while (true)
        {
            Console.WriteLine();
            Console.WriteLine("Journal Menu");
            Console.WriteLine("1. Write a new entry");
            Console.WriteLine("2. Display the journal");
            Console.WriteLine("3. Save the journal to a file");
            Console.WriteLine("4. Load the journal from a file");
            Console.WriteLine("5. Quit");
            Console.Write("Choose an option (1–5): ");

            string choice = Console.ReadLine()?.Trim() ?? "";

            switch (choice)
            {
                case "1":
                    WriteEntry(journal, promptGen);
                    break;

                case "2":
                    journal.DisplayAll();
                    break;

                case "3":
                    Console.Write("Enter filename to save (e.g., journal.txt): ");
                    string saveName = (Console.ReadLine() ?? "").Trim();
                    if (!string.IsNullOrWhiteSpace(saveName))
                        journal.SaveToFile(saveName);
                    else
                        Console.WriteLine("Save canceled (no filename).");
                    break;

                case "4":
                    Console.Write("Enter filename to load (e.g., journal.txt): ");
                    string loadName = (Console.ReadLine() ?? "").Trim();
                    if (!string.IsNullOrWhiteSpace(loadName))
                        journal.LoadFromFile(loadName);
                    else
                        Console.WriteLine("Load canceled (no filename).");
                    break;

                case "5":
                    Console.WriteLine("Goodbye!");
                    return;

                default:
                    Console.WriteLine("Please choose a valid option (1–5).");
                    break;
            }
        }
    }

    private static void WriteEntry(Journal journal, PromptGenerator promptGen)
    {
        string prompt = promptGen.GetRandomPrompt();
        Console.WriteLine();
        Console.WriteLine($"Today's date: {DateTime.Now.ToShortDateString()}");
        Console.WriteLine($"Prompt: {prompt}");
        Console.WriteLine("Write your response (single paragraph).");
        Console.Write("> ");
        string text = Console.ReadLine() ?? "";

        var entry = new Entry(DateTime.Now.ToShortDateString(), prompt, text);
        journal.AddEntry(entry);
        Console.WriteLine("Entry added!");
    }
}