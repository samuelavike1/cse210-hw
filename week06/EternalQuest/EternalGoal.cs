public class EternalGoal : Goal
{
    public EternalGoal(string shortName, string description, int points)
        : base(shortName, description, points)
    {
    }

    public override int RecordEvent()
    {
        // Eternal goals never finish, always give points
        return GetPoints();
    }

    public override bool IsComplete()
    {
        return false; // Eternal, never complete
    }

    public override string GetStringRepresentation()
    {
        // Type : name|description|points
        return $"EternalGoal:{GetShortName()}|{GetDescription()}|{GetPoints()}";
    }
}
