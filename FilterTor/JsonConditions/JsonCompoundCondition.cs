namespace GridEngineCore.Conditions;

using GridEngineCore.Extensions;
using System.Text.Json;
using System.Text.Json.Serialization;


public class JsonCompoundCondition : JsonConditionBase
{
    public required List<JsonConditionBase> Conditions { get; set; }

    public required bool IsAndMode { get; set; }

    public JsonCompoundCondition()
    {
        Category = CategoryType.Compound;
    }

    public override bool Validate()
    {
        return base.Validate() &&
                //Entity == EntityType.Compound &&
                Conditions?.Count > 0 &&
                Conditions.All(c => c.Validate());
    }

    public override string Serialize(JsonSerializerOptions? options)
    {
        // 1399.10.28
        Category = CategoryType.Compound;
        //this.Entity = EntityType.Compound;

        return base.Serialize(options);
    }

    public static JsonCompoundCondition Create(bool isAndMode, List<JsonConditionBase> conditions)
    {
        if (conditions.IsNullOrEmpty())
            throw new NotImplementedException();

        return new JsonCompoundCondition()
        {
            //Entity = EntityType.Compound,

            Category = CategoryType.Compound,

            IsAndMode = isAndMode,

            Conditions = conditions.Where(i => i != null)?.ToList(),
        };
    }

    // 1399.11.04
    // این متد به شرط رو می‌گیره و اون شرط رو اگه ترکیبی نباشه 
    // در یک بدنه شرط ترکیبی قرار می‌دهد. این متد برای هماهنگی
    // بیش‌تر با کامپوننت مورد استفاده در سمت فرانت نوشته شده 
    // است.
    public static JsonConditionBase? EnforceCompound(JsonConditionBase jsonConditionBase)
    {
        if (jsonConditionBase is null)
        {
            return null;
        }
        else if (jsonConditionBase.Category == CategoryType.Compound)
        {
            return jsonConditionBase;
        }
        else
        {
            return Create(true, new List<JsonConditionBase>() { jsonConditionBase });
        }
    }

    public class JsonCompundConditionMock : JsonConditionBase
    {
        public List<object> Conditions { get; set; }

        [JsonIgnore]
        public List<JsonConditionBase> ConditionItems { get; set; }

        public bool IsAndMode { get; set; }

        public override bool Validate()
        {
            return Category == CategoryType.Compound && base.Validate();
        }

        public static JsonCompoundCondition? DeserializeMock(System.Text.Json.Nodes.JsonNode jsonNode, JsonSerializerOptions options)
        {
            var result = jsonNode.Deserialize<JsonCompundConditionMock>()!;

            if (result == null)
                return null;

            result.ConditionItems = new List<JsonConditionBase>();

            for (int i = 0; i < result.Conditions.Count; i++)
            {
                var item = Deserialize(result.Conditions[i].ToString()!, options);

                if (item != null)
                    result.ConditionItems.Add(item);
            }

            return Create(result.IsAndMode, result.ConditionItems);
        }
    }

    public override string ToString()
    {
        var AndOr = IsAndMode ? " و " : " یا ";

        return $" {AndOr} ({string.Join(" ; ", Conditions.Select(c => c.ToString()))})";
    }
}
