public class Entry
{
    public string _date;
    public string _promtpText;
    public string _entryText;

    public Entry(string date, string promptText, string entryText)
    {
        _date = date;
        _promtpText = promptText;
        _entryText = entryText;
    }

    public void Display()
    {
        Console.WriteLine($"[{_date}]");
        Console.WriteLine($"Prompt : {_promtpText}");
        Console.WriteLine($"Entry  : {_entryText}");
        Console.WriteLine(new string('-', 60));
    }


    // Using a custom delimiter to avoid CSV quoting rules right now.
    private const string SEP = "~|~";

    public string ToLine()
    {
        // Preserve line safely by replacing any accidental newlines
        string safeText = _entryText?.Replace("\r", " ").Replace("\n", " ") ?? "";
        string safePrompt = _promtpText?.Replace("\r", " ").Replace("\n", " ") ?? "";
        return $"{_date}{SEP}{safePrompt}{SEP}{safeText}";
    }

    public static Entry FromLine(string line)
    {
        var parts = line.Split(SEP, StringSplitOptions.None);
        // Expect exactly 3 parts: date, prompt, entry
        string date = parts.Length > 0 ? parts[0] : "";
        string prompt = parts.Length > 1 ? parts[1] : "";
        string text = parts.Length > 2 ? parts[2] : "";
        return new Entry(date, prompt, text);
    }
}