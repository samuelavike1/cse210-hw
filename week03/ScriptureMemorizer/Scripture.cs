public class Scripture
{
    private Reference _reference;
    private List<Word> _words;
    private static readonly Random _rand = new Random();

    public Scripture(Reference reference, string text)
    {
        _reference = reference;
        _words = new List<Word>();

        // Split by whitespace; punctuation stays attached to tokens
        string[] tokens = text.Split(' ', StringSplitOptions.RemoveEmptyEntries);
        foreach (var t in tokens)
        {
            _words.Add(new Word(t));
        }
    }

    // Hide N random words, choosing ONLY from currently visible words
    public void HideRandomWords(int numberToHide)
    {
        for (int i = 0; i < numberToHide; i++)
        {
            var visible = _words.Where(w => !w.IsHidden()).ToList();
            if (visible.Count == 0) break;

            int pick = _rand.Next(visible.Count);
            visible[pick].Hide();
        }
    }

    // Reveal one hidden word (for "hint")
    public bool RevealOneHiddenWord()
    {
        var hidden = _words.Where(w => w.IsHidden()).ToList();
        if (hidden.Count == 0) return false;

        int pick = _rand.Next(hidden.Count);
        hidden[pick].Show();
        return true;
    }

    public int TotalWordCount() => _words.Count;
    public int HiddenWordCount() => _words.Count(w => w.IsHidden());

    public bool IsCompletelyHidden() => _words.All(w => w.IsHidden());

    public string GetDisplayText()
    {
        string referenceText = _reference.GetDisplayText();
        string verse = string.Join(' ', _words.Select(w => w.GetDisplayText()));
        return $"{referenceText} — {verse}";
    }

}