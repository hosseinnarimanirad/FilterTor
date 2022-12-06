namespace FilterTor.Targets;


public class JsonArrayTarget : JsonTargetBase
{
    public List<string> Values { get; set; }

    public JsonArrayTarget(List<string> values)
    {
        TargetType = TargetType.Array;

        Values = values;
    }

    public override string ToString()
    {
        return $"TargetType: {TargetType}; Values: {string.Join(',', Values)}";
    }
}
