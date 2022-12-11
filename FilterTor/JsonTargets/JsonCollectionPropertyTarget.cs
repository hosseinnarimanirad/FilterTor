namespace GridEngineCore.Targets;


public class JsonCollectionPropertyTarget : JsonTargetBase
{
    public required string Entity { get; set; }

    public required string Collection { get; set; }

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
