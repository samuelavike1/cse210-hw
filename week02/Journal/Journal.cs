public class Journal
{
    public List<Entry> _entries;

    public Journal()
    {
        _entries = new List<Entry>();
    }

    public void AddEntry(Entry newEntry)
    {
        _entries.Add(newEntry);
    }

    public void DisplayAll()
    {
        if (_entries.Count == 0)
        {
            Console.WriteLine("No entries yet. Choose '1' to write your first entry.");
            return;
        }

        foreach (var e in _entries)
        {
            e.Display();
        }
    }

    public void SaveToFile(string file)
    {
        try
        {
            using var writer = new StreamWriter(file);
            foreach (var e in _entries)
            {
                writer.WriteLine(e.ToLine());
            }
            Console.WriteLine($"Journal saved to \"{file}\".");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error saving file: {ex.Message}");
        }
    }

    public void LoadFromFile(string file)
    {
        try
        {
            if (!File.Exists(file))
            {
                Console.WriteLine("File not found.");
                return;
            }

            var lines = File.ReadAllLines(file);
            _entries.Clear(); // replace current entries

            foreach (var line in lines)
            {
                if (string.IsNullOrWhiteSpace(line)) continue;
                _entries.Add(Entry.FromLine(line));
            }

            Console.WriteLine($"Loaded {_entries.Count} entr{(_entries.Count == 1 ? "y" : "ies")} from \"{file}\".");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error loading file: {ex.Message}");
        }
    }
}