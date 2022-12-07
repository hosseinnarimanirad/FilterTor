namespace GridEngineCore.Targets;


public class JsonCollectionPropertyTarget : JsonTargetBase
{
    public string Entity { get; set; }

    public string Collection { get; set; }

    public JsonCollectionPropertyTarget(string entity, string collection)
    {
        TargetType = TargetType.CollectionProperty;

        Entity = entity;

        Collection = collection;
    }

    public override string? ToString()
    {
        return $"TargetType: {TargetType}; Entity: {Entity}; Collection: {Collection}";
    }
}
