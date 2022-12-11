namespace GridEngineCore.Targets;


public class JsonConstantTarget : JsonTargetBase
{
    public required string Value { get; set; }

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
