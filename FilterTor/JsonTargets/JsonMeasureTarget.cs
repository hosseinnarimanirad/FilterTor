namespace GridEngineCore.Targets;


public class JsonMeasureTarget : JsonTargetBase
{
    public required string Entity { get; set; }

    public required string Measure { get; set; }

    public JsonMeasureTarget(string entity, string measure)
    {
        this.TargetType = TargetType.Measure;

        Entity = entity;

        Measure = measure;
    }

    public override string ToString()
    {
        return $"TargetType: {TargetType}; Entity: {Entity}; Measure: {Measure}";
    }
}
