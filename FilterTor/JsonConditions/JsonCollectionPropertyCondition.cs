namespace FilterTor.Conditions;

using FilterTor.Helpers;
using System.Collections.Generic;
using System.Text.Json.Serialization;


public class JsonCollectionPropertyCondition : JsonLeafCondition
{
    public required DurationType DurationType { get; set; }

    public JsonConditionBase? Filter { get; set; }

    public required string Collection { get; set; }

    public JsonCollectionPropertyCondition()
    {
        Category = CategoryType.CollectionProperty;
    }

    public override bool Validate()
    {
        return base.Validate()
                && Category == CategoryType.CollectionProperty

                // 1399.12.19
                // در این‌جا نمی‌شه خالی بودن/نبودن مقدار
                // تارگت رو بررسی کرد چون شاید در کلاس‌های
                // فرزند شرط از جنس اگزیست داشته باشیم که
                // نه عملگر دارند و نه تارگت
                //&& HasValidTarget()
                && (Filter == null || Filter.Validate());
    }

    protected bool HasNotNullTarget()
    {
        return Target is not null;
    }


    public override string ToString()
    {
        var filter = Filter == null ? string.Empty : $"با شرایط: [{Filter}]";

        var operation = Operation == 0 ? string.Empty : $"{Operation.GetDescription()} {Target}";

        return $" {filter} {operation} ";
    }

    public override List<string> GetSubConditions()
    {
        return new List<string>() { Collection.ToString() };
    }
}

