public class GratitudeActivity : Activity
{
    public GratitudeActivity()
        : base(
            "Gratitude",
            "This activity invites you to focus on gratitude by writing down " +
            "things you are thankful for today.")
    {
    }

    public void Run()
    {
        DisplayStartingMessage();

        Console.WriteLine("Think about the good things in your life.");
        Console.WriteLine("Type things you are grateful for. Press ENTER after each one.");
        Console.WriteLine("(Leave a line blank to finish early.)");
        Console.WriteLine();

        List<string> items = new List<string>();
        DateTime endTime = DateTime.Now.AddSeconds(Duration);

        while (DateTime.Now < endTime)
        {
            Console.Write("Grateful for: ");
            string line = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(line))
            {
                break; // user finished early
            }
            items.Add(line.Trim());
        }

        Console.WriteLine();
        Console.WriteLine($"You wrote {items.Count} gratitude items. ♥");
        LogSession($"GratitudeItems={items.Count}");
        DisplayEndingMessage();
    }
}
