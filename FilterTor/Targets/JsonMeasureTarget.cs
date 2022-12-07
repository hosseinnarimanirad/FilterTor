namespace GridEngineCore.Targets;


public class JsonMeasureTarget : JsonTargetBase
{
    public string Entity { get; set; }

    public string Measure { get; set; }

    public JsonMeasureTarget(string entity, string measure)
    {
        Entity = entity;

        Measure = measure;
    }

    public override string ToString()
    {
        return $"TargetType: {TargetType}; Entity: {Entity}; Measure: {Measure}";
    }
}
