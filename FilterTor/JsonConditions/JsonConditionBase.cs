namespace FilterTor.Conditions;

using FilterTor.Common;
using FilterTor.Common.Converters;
using FilterTor.Resolvers;
using FilterTor.Helpers;
using System.Text.Json;
using System.Text.Json.Serialization;
using FilterTor.Extensions;

[JsonConverter(typeof(ConditionJsonConverter))]
public class JsonConditionBase : IJsonEntity
{
    public required CategoryType Category { get; set; }

    public virtual string Serialize(JsonSerializerOptions? options)
    {
        if (!Validate())
            throw new NotImplementedException("JsonConditionBase > Serialize");

        return JsonSerializer.Serialize(this, GetType(), options ?? FilterTorHelper.DefaultJsonSerializerOptions);
    }

    public virtual bool Validate()
    {
        return Category != 0;
    }

    public virtual bool Validate<T>(IEntityResolver<T> resolver)
    {
        return resolver.Validate(this);
    }

    public static JsonConditionBase? Deserialize(string? jsonCondition)
    {
        return Deserialize(jsonCondition, FilterTorHelper.DefaultJsonSerializerOptions);
    }

    public static JsonConditionBase? Deserialize(string? jsonCondition, JsonSerializerOptions? options = null)
    {
        if (string.IsNullOrWhiteSpace(jsonCondition))
            return null;

        return FilterTorHelper.ParseCondition(jsonCondition, options ?? FilterTorHelper.DefaultJsonSerializerOptions);
    }

    private JsonConditionBase? FilterCondition(JsonConditionBase condition, Predicate<JsonConditionBase> predicate)
    {
        if (condition.Category == CategoryType.Compound)
        {
            var compoundCondition = condition as JsonCompoundCondition;

            var conditions = compoundCondition?.Conditions.Select(c => FilterCondition(c, predicate))?.Where(c => c is not null)?.ToList();

            if (conditions.IsNullOrEmpty())
                return null;

            return JsonCompoundCondition.Create(compoundCondition!.IsAndMode, conditions!);
        }
        else if (predicate(condition))
        {
            return condition;
        }

        return null;
    }

    public JsonConditionBase? ExtractCondition(Predicate<JsonConditionBase> predicate)
    {
        return FilterCondition(this, predicate);
    }

    public bool HasCondition(Predicate<JsonConditionBase> predicate)
    {
        if (Category == CategoryType.Compound)
        {
            var compoundCondition = this as JsonCompoundCondition;

            return compoundCondition!.Conditions.Any(c => c.HasCondition(predicate));
        }

        return predicate(this);
    }

    public virtual List<string> GetSubConditions()
    {
        throw new NotImplementedException();
    }
      
}
