namespace FilterTor.Targets;


public class JsonConstantTarget : JsonTargetBase
{
    public string Value { get; set; }

    public JsonConstantTarget(string value)
    {
        TargetType = TargetType.Constant;

        Value = value;
    }

    public override string ToString()
    {
        return $"TargetType: {TargetType}; Value: {Value}";
    }
}
