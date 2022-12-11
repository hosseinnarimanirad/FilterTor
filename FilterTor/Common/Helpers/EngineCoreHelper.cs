namespace FilterTor.Helpers;

using FilterTor.Models;
using System.Text.Json.Nodes;
using System.Text.Json; 
using FilterTor.Conditions;
using FilterTor.Targets;
using FilterTor.Extensions;

public static class FilterTorHelper
{
    private readonly static JsonNodeOptions _nodeOptions = new JsonNodeOptions() { PropertyNameCaseInsensitive = true };

    public readonly static JsonSerializerOptions DefaultJsonSerializerOptions = new JsonSerializerOptions()
    {
        PropertyNameCaseInsensitive = true,
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase
    };



    public static List<EnumInfo> GetAllDurationTypes()
    {
        return EnumHelper.Parse<DurationType>();
    }

    public static List<EnumInfo> GetAllTargetTypes()
    {
        return EnumHelper.Parse<TargetType>();
    }


    public static List<EnumInfo> GetAllOperationControlTypes()
    {
        return EnumHelper.Parse<OperationControlType>();
    }

    public static List<EnumInfo> GetAllOperationTypes()
    {
        return EnumHelper.Parse<Operation>();
    }

    public static List<EnumInfo> GetAllRangeType()
    {
        return EnumHelper.Parse<RangeType>();
    }

    public static List<string> GetOperations(OperationControlType controlType)
    {
        switch (controlType)
        {
            case OperationControlType.SingleEnum:
                return new List<string>()
                {
                    Operation.EqualsTo.GetName(),
                    Operation.NotEqualTo.GetName(),
                };

            case OperationControlType.MultiEnum:
                return new List<string>()
                {
                    Operation.In.GetName(),
                    Operation.NotIn.GetName(),
                };

            case OperationControlType.Numeric:
                return new List<string>()
                {
                    Operation.EqualsTo.GetName(),
                    Operation.GreaterThan.GetName(),
                    Operation.GreaterThanOrEqualTo.GetName(),
                    Operation.LessThan.GetName(),
                    Operation.LessThanOrEqualTo.GetName(),
                    //Operation.NotEqualTo.GetName(),
                };

            case OperationControlType.RangeNumeric:
                return new List<string>() { Operation.Between.GetName() };

            case OperationControlType.AutoComplete:
                return new List<string>()
                {
                    Operation.EqualsTo.GetName(),
                    Operation.NotEqualTo.GetName()
                };

            case OperationControlType.Checkbox:
                return new List<string>() { Operation.EqualsTo.GetName() };

            case OperationControlType.DateTime:
                return new List<string>()
                {
                    Operation.EqualsTo.GetName(),
                    Operation.GreaterThan.GetName(),
                    Operation.GreaterThanOrEqualTo.GetName(),
                    Operation.LessThan.GetName(),
                    Operation.LessThanOrEqualTo.GetName(),
                    //Operation.NotEqualTo.GetName(),
                };

            case OperationControlType.Textbox:
                return new List<string>()
                {
                    Operation.EqualsTo.GetName(),

                    Operation.StartsWith.GetName(),
                    Operation.EndsWith.GetName(),
                };

            case OperationControlType.MultiEnumForArray:
                return new List<string>()
                {
                    //Operation.IncludeAll.GetName(),
                    Operation.IncludeAny.GetName(),
                    Operation.ExcludeAll.GetName(),
                    //Operation.ExcludeAny.GetName(),
                };

            case OperationControlType.SingleEnumForArray:
                return new List<string>()
                {
                    //Operation.IncludeAll.GetName(),
                    Operation.IncludeAny.GetName(),
                    //Operation.ExcludeAll.GetName(),
                    Operation.ExcludeAny.GetName(),
                };

            default:
                throw new NotImplementedException();
        }
    }

    public static ShamsiYearMonthWeek GetShamsiInfo(DurationType type, DateTime dateTime)
    {
        ShamsiYearMonthWeek result = new ShamsiYearMonthWeek();

        if (type == DurationType.Yearly)
        {
            result.ShamsiYear = DateTimeExtensions.GetPersianYear(dateTime);
        }

        switch (type)
        {
            case DurationType.Weekly:
                result.ShamsiYear = DateTimeExtensions.GetPersianYear(dateTime);
                result.ShamsiMonth = DateTimeExtensions.GetPersianMonth(dateTime);
                result.ShamsiWeek = DateTimeExtensions.GetPersianWeekOfYear(dateTime);
                break;
            case DurationType.Monthly:
                result.ShamsiYear = DateTimeExtensions.GetPersianYear(dateTime);
                result.ShamsiMonth = DateTimeExtensions.GetPersianMonth(dateTime);
                break;
            case DurationType.Yearly:
                result.ShamsiYear = DateTimeExtensions.GetPersianYear(dateTime);
                break;
            default:
                throw new NotImplementedException("PolicyHelper.cs > GetShamsiInfo");
        }

        return result;
    }



    public static JsonConditionBase? ParseCondition(ref Utf8JsonReader reader, JsonSerializerOptions options)
    {
        var node = JsonNode.Parse(ref reader, _nodeOptions);

        return ParseCondition(node, options ?? DefaultJsonSerializerOptions);
    }

    public static JsonConditionBase? ParseCondition(string jsonCondition, JsonSerializerOptions options)
    {
        var node = JsonNode.Parse(jsonCondition, _nodeOptions);

        return ParseCondition(node, options ?? DefaultJsonSerializerOptions);
    }

    private static JsonConditionBase? ParseCondition(JsonNode? node, JsonSerializerOptions options)
    {
        if (node is null) return null;

        //var entityType = GetEnum<EntityType>(node, nameof(JsonConditionBase.Entity));

        var category = GetEnum<CategoryType>(node, nameof(JsonConditionBase.Category));

        switch (category)
        {
            case CategoryType.Compound:
                var compound = JsonCompoundCondition.JsonCompundConditionMock.DeserializeMock(node, options);
                return compound?.Conditions.Count == 1 ? compound.Conditions.First() : compound;

            case CategoryType.Property:
                return JsonSerializer.Deserialize<JsonPropertyCondition>(node, options);

            case CategoryType.CollectionProperty:
                return JsonSerializer.Deserialize<JsonCollectionPropertyCondition>(node, options);

            case CategoryType.Measure:
                return JsonSerializer.Deserialize<JsonMeasureCondition>(node, options);

            case CategoryType.List:
                return JsonSerializer.Deserialize<JsonListCondition>(node, options);

            default:
                throw new NotImplementedException("FilterTorHelper > ParseCondition");
        }
    }

    public static JsonConditionBase? Merge(JsonConditionBase? first, JsonConditionBase? second)
    {
        var conditions = new List<JsonConditionBase>();

        if (first is not null)
            conditions.Add(first);

        if (second is not null)
            conditions.Add(second);

        if (conditions.Count == 2)
        {
            return JsonCompoundCondition.Create(true, conditions);
        }
        else if (conditions.Count == 1)
        {
            return conditions.First();
        }
        else
        {
            return null;
        }
    }


    public static JsonTargetBase? ParseTarget(ref Utf8JsonReader reader, JsonSerializerOptions options)
    {
        var node = JsonNode.Parse(ref reader, _nodeOptions);

        return ParseTarget(node, options ?? DefaultJsonSerializerOptions);
    }

    private static JsonTargetBase? ParseTarget(JsonNode? node, JsonSerializerOptions options)
    {
        if (node is null) return null;

        var target = GetEnum<TargetType>(node, nameof(JsonTargetBase.TargetType));

        switch (target)
        {
             
            case TargetType.CollectionProperty:
                break;
            case TargetType.Measure:
                break;
            default:
                break;
        }

        switch (target)
        {
            case TargetType.Constant:
                return JsonSerializer.Deserialize<JsonConstantTarget>(node.ToJsonString(), options);

            case TargetType.Range:
                return JsonSerializer.Deserialize<JsonRangeTarget>(node.ToJsonString(), options);

            case TargetType.Array:
                return JsonSerializer.Deserialize<JsonArrayTarget>(node.ToJsonString(), options);

            case TargetType.Property:
                return JsonSerializer.Deserialize<JsonPropertyTarget>(node.ToJsonString(), options);

            case TargetType.CollectionProperty:
                return JsonSerializer.Deserialize<JsonCollectionPropertyTarget>(node.ToJsonString(), options);

            case TargetType.Measure:
                return JsonSerializer.Deserialize<JsonMeasureTarget>(node.ToJsonString(), options);

            default:
                throw new NotImplementedException("FilterTorHelper > ParseTarget");
        }
    }

    private static T GetEnum<T>(this JsonNode node, string propertyName) where T : struct
    { 
        return Enum.Parse<T>(node[propertyName]?.ToString()!, ignoreCase: true);
    }

}
