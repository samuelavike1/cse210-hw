public static class ScriptureLibrary
{
    private static readonly Random _rand = new Random();

    // File format (| separated):
    // Single verse: Book|Chapter|Verse|Text
    // Range:        Book|Chapter|StartVerse|EndVerse|Text
    //
    // Example:
    // John|3|16|For God so loved the world...
    // Proverbs|3|5|6|Trust in the Lord...

    public static List<Scripture> LoadFromFile(string path)
    {
        var list = new List<Scripture>();
        if (!File.Exists(path)) return list;

        foreach (var line in File.ReadAllLines(path))
        {
            if (string.IsNullOrWhiteSpace(line) || line.TrimStart().StartsWith("#"))
                continue;

            var parts = line.Split('|');
            try
            {
                Scripture s = null;

                if (parts.Length == 4)
                {
                    // Book|Chapter|Verse|Text
                    string book = parts[0].Trim();
                    int chapter = int.Parse(parts[1]);
                    int verse = int.Parse(parts[2]);
                    string text = parts[3].Trim();

                    var r = new Reference(book, chapter, verse);
                    s = new Scripture(r, text);
                }
                else if (parts.Length == 5)
                {
                    // Book|Chapter|StartVerse|EndVerse|Text
                    string book = parts[0].Trim();
                    int chapter = int.Parse(parts[1]);
                    int startVerse = int.Parse(parts[2]);
                    int endVerse = int.Parse(parts[3]);
                    string text = parts[4].Trim();

                    var r = new Reference(book, chapter, startVerse, endVerse);
                    s = new Scripture(r, text);
                }

                if (s != null) list.Add(s);
            }
            catch
            {
                // Ignore malformed lines
            }
        }

        return list;
    }

    public static Scripture PickRandom(List<Scripture> scriptures)
    {
        if (scriptures.Count == 0) return null;
        int i = _rand.Next(scriptures.Count);
        return scriptures[i];
    }
}
