namespace GridEngineCore.Targets;


public class JsonPropertyTarget : JsonTargetBase
{
    public string Entity { get; set; }

    public string Property { get; set; }

    public JsonPropertyTarget(string entity, string property)
    {
        TargetType = TargetType.Property;

        Entity = entity;

        Property = property;
    }

    public override string ToString()
    {
        return $"TargetType: {TargetType}; Entity: {Entity}; Property: {Property}";
    }
}
