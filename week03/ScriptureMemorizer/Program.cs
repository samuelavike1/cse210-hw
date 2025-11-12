
// EXCEEDS REQUIREMENTS:
// - Loads a library of scriptures from "scriptures.txt" and chooses one at random
// - Difficulty levels (Easy/Normal/Hard/Insane) control how many words hide per round
// - Hides only visible words (no wasted picks)
// - Shows progress indicator (Hidden % and counts)
// - "hint" command reveals one random hidden word
// - "new" command switches to another random scripture while keeping difficulty


class Program
{
    static void Main()
    {
        // 1) Load library
        string libPath = "scriptures.txt";
        var library = ScriptureLibrary.LoadFromFile(libPath);

        // Fallback if file is missing/empty (still exceeds requirements gracefully)
        if (library.Count == 0)
        {
            var r1 = new Reference("John", 3, 16);
            var t1 = "For God so loved the world, that he gave his only begotten Son, " +
                     "that whosoever believeth in him should not perish, but have everlasting life.";
            library.Add(new Scripture(r1, t1));

            var r2 = new Reference("Proverbs", 3, 5, 6);
            var t2 = "Trust in the Lord with all thine heart; and lean not unto thine own understanding. " +
                     "In all thy ways acknowledge him, and he shall direct thy paths.";
            library.Add(new Scripture(r2, t2));
        }

        // 2) Choose difficulty
        Difficulty difficulty = AskDifficulty();
        int wordsPerRound = (int)difficulty;

        // 3) Pick a random scripture
        Scripture scripture = ScriptureLibrary.PickRandom(library);
        if (scripture == null)
        {
            Console.WriteLine("No scriptures available. Exiting.");
            return;
        }

        // 4) Main loop
        while (true)
        {
            Console.Clear();
            PrintHeader(difficulty, wordsPerRound);
            PrintScripture(scripture);

            // progress
            int hidden = scripture.HiddenWordCount();
            int total = scripture.TotalWordCount();
            double pct = total == 0 ? 100 : (hidden * 100.0 / total);
            Console.WriteLine($"\nHidden: {pct:0.#}% ({hidden}/{total})");

            Console.WriteLine("\nPress Enter to hide words");
            Console.WriteLine("Type  'hint' to reveal one word, 'new' for a new scripture, or 'quit' to exit.");
            Console.Write("> ");

            string input = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(input))
            {
                input = input.Trim().ToLowerInvariant();
                if (input == "quit") break;
                if (input == "new")
                {
                    scripture = ScriptureLibrary.PickRandom(library);
                    continue;
                }
                if (input == "hint")
                {
                    bool revealed = scripture.RevealOneHiddenWord();
                    if (!revealed)
                    {
                        Console.WriteLine("No hidden words to reveal.");
                        Console.ReadKey();
                    }
                    continue;
                }
            }

            // Enter was pressed (or empty input) → hide more words
            scripture.HideRandomWords(wordsPerRound);

            if (scripture.IsCompletelyHidden())
            {
                Console.Clear();
                PrintHeader(difficulty, wordsPerRound);
                PrintScripture(scripture);
                Console.WriteLine("\n(All words hidden. Well done!)");
                break;
            }
        }
    }

    static Difficulty AskDifficulty()
    {
        while (true)
        {
            Console.Clear();
            Console.WriteLine("Select difficulty:");
            Console.WriteLine("1) Easy   (hide 1 word/round)");
            Console.WriteLine("2) Normal (hide 3 words/round)");
            Console.WriteLine("3) Hard   (hide 5 words/round)");
            Console.WriteLine("4) Insane (hide 8 words/round)");
            Console.Write("Choice: ");

            string input = Console.ReadLine();
            switch (input)
            {
                case "1": return Difficulty.Easy;
                case "2": return Difficulty.Normal;
                case "3": return Difficulty.Hard;
                case "4": return Difficulty.Insane;
                default:
                    Console.WriteLine("Invalid choice. Press any key to try again...");
                    Console.ReadKey();
                    break;
            }
        }
    }

    static void PrintHeader(Difficulty difficulty, int wordsPerRound)
    {
        Console.WriteLine("=== Scripture Memorizer ===");
        Console.WriteLine($"Difficulty: {difficulty}  (Hiding {wordsPerRound} words per round)");
        Console.WriteLine("----------------------------\n");
    }

    static void PrintScripture(Scripture scripture)
    {
        Console.WriteLine(scripture.GetDisplayText());
    }
}
