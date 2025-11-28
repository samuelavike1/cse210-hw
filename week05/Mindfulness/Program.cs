using System;

class Program
{
    static void Main(string[] args)
    {
        string choice = "";

        // Track how many times each activity is run (exceeding requirement).
        int breathingCount = 0;
        int reflectingCount = 0;
        int listingCount = 0;
        int gratitudeCount = 0;

        while (choice != "5")
        {
            Console.Clear();
            Console.WriteLine("Mindfulness Program");
            Console.WriteLine("-------------------");
            Console.WriteLine("1. Breathing Activity");
            Console.WriteLine("2. Reflecting Activity");
            Console.WriteLine("3. Listing Activity");
            Console.WriteLine("4. Gratitude Activity");
            Console.WriteLine("5. Quit");
            Console.Write("Select a choice from the menu: ");
            choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    BreathingActivity breathing = new BreathingActivity();
                    breathing.Run();
                    breathingCount++;
                    break;

                case "2":
                    ReflectingActivity reflecting = new ReflectingActivity();
                    reflecting.Run();
                    reflectingCount++;
                    break;

                case "3":
                    ListingActivity listing = new ListingActivity();
                    listing.Run();
                    listingCount++;
                    break;

                case "4":
                    GratitudeActivity gratitude = new GratitudeActivity();
                    gratitude.Run();
                    gratitudeCount++;
                    break;

                case "5":
                    Console.WriteLine();
                    Console.WriteLine("Thanks for using the Mindfulness Program!");
                    Console.WriteLine();
                    Console.WriteLine("Session summary:");
                    Console.WriteLine($"  Breathing activities run : {breathingCount}");
                    Console.WriteLine($"  Reflecting activities run: {reflectingCount}");
                    Console.WriteLine($"  Listing activities run   : {listingCount}");
                    Console.WriteLine($"  Gratitude activities run : {gratitudeCount}");
                    Console.WriteLine();
                    Console.WriteLine("A detailed log of your sessions is stored in:");
                    Console.WriteLine("  mindfulness_log.txt");
                    Console.WriteLine();
                    Console.WriteLine("Press ENTER to exit...");
                    Console.ReadLine();
                    break;

                default:
                    Console.WriteLine("Please choose a valid option (1–5).");
                    Console.WriteLine("Press ENTER to continue...");
                    Console.ReadLine();
                    break;
            }
        }
    }
}

/*
 * EXCEEDING REQUIREMENTS:
 * 1. Added a fourth activity: GratitudeActivity, which guides the user to
 *    write things they are thankful for.
 *
 * 2. Implemented per-session logging in the Activity base class:
 *    - Each activity writes an entry to "mindfulness_log.txt" with
 *      timestamp, activity name, duration, and extra details
 *      (e.g., breaths, number of items listed, questions asked).
 *
 * 3. ReflectingActivity ensures that reflection questions do not repeat
 *    until all questions have been used at least once during that session.
 *
 * 4. Program tracks how many times each activity is performed during
 *    the run of the program and shows a summary when the user quits.
 */
