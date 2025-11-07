public class PromptGenerator
{
    public List<string> _prompts;
    private Random _rand;

    public PromptGenerator()
    {
        _rand = new Random();
        _prompts = new List<string>
        {
            "Who was the most interesting person I interacted with today?",
            "What was the best part of my day?",
            "How did I see the hand of the Lord in my life today?",
            "What was the strongest emotion I felt today?",
            "If I had one thing I could do over today, what would it be?",
            "What is one small win I had today and why did it matter?",
            "What am I grateful for right now?"
        };
    }

    public string GetRandomPrompt()
    {
        if (_prompts == null || _prompts.Count == 0)
        {
            return "Write about anything on your mind today.";
        }

        int i = _rand.Next(0, _prompts.Count);
        return _prompts[i];
    }
}