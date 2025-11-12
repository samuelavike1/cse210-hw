using System.Text;

public class Word
{
    private string _text;
    private bool _isHidden;

    public Word(string text)
    {
        _text = text;
        _isHidden = false;
    }

    public void Hide() { _isHidden = true; }
    public void Show() { _isHidden = false; }
    public bool IsHidden() => _isHidden;


    public string GetDisplayText()
    {
        if (!_isHidden) return _text;

        var sb = new StringBuilder(_text.Length);
        foreach (char c in _text)
        {
            if (char.IsLetterOrDigit(c)) sb.Append('_');
            else sb.Append(c);
        }
        return sb.ToString();
    }
}