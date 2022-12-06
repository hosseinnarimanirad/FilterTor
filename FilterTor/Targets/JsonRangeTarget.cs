namespace FilterTor.Targets;


public class JsonRangeTarget : JsonTargetBase
{
    public string MinValue { get; set; }

    public string MaxValue { get; set; }

    public JsonRangeTarget(string minValue, string maxValue)
    {
        TargetType = TargetType.Range;

        MinValue = minValue;

        MaxValue = maxValue;
    }

    public override string ToString()
    {
        return $"TargetType: {TargetType}; MinValue: {MinValue}; MaxValue: {MaxValue}";
    }
}