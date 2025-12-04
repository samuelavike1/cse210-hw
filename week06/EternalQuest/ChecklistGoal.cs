public class ChecklistGoal : Goal
{
    private int _amountCompleted;
    private int _target;
    private int _bonus;

    // Normal constructor (starts at 0 completed)
    public ChecklistGoal(string shortName, string description, int points, int target, int bonus)
        : this(shortName, description, points, 0, target, bonus)
    {
    }

    // Constructor used when loading from file
    public ChecklistGoal(string shortName, string description, int points,
                         int amountCompleted, int target, int bonus)
        : base(shortName, description, points)
    {
        _amountCompleted = amountCompleted;
        _target = target;
        _bonus = bonus;
    }

    public override int RecordEvent()
    {
        _amountCompleted++;

        int totalPoints = GetPoints();

        // On the completion moment, add bonus
        if (_amountCompleted == _target)
        {
            totalPoints += _bonus;
        }

        return totalPoints;
    }

    public override bool IsComplete()
    {
        return _amountCompleted >= _target;
    }

    public override string GetDetailsString()
    {
        string checkbox = IsComplete() ? "[X]" : "[ ]";
        return $"{checkbox} {GetShortName()} ({GetDescription()}) — Completed {_amountCompleted}/{_target}";
    }

    public override string GetStringRepresentation()
    {
        // Type : name|description|points|amountCompleted|target|bonus
        return $"ChecklistGoal:{GetShortName()}|{GetDescription()}|{GetPoints()}|{_amountCompleted}|{_target}|{_bonus}";
    }
}
