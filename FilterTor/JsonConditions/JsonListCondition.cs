namespace FilterTor.Conditions;

using FilterTor.Helpers; 
using System.Text.Json.Serialization;


public class JsonListCondition : JsonLeafCondition
{
    public required DurationType DurationType { get; set; }

    public JsonConditionBase? Filter { get; set; }
     
    public required string Measure { get; set; }
     
    public required string GroupBy { get; set; }


    public JsonListCondition()
    {
        Category = CategoryType.List;
    }

    public override bool Validate()
    {
        return base.Validate()
                && Category == CategoryType.List
                && DurationType != 0
                && (Filter == null || Filter.Validate())
                // 1399.11.28
                // در حالت شرط اگزیست این مقدار
                // می تواند خالی باشد
                //&& OperationName != 0
                ;
    }

    protected bool HasNotNullTarget()
    {
        //return !string.IsNullOrWhiteSpace(AggregateTargetValue);
        return Target is not null;
    }

    // 1399.12.19
    // در این‌جا نمی‌شه خالی بودن/نبودن مقدار
    // تارگت رو بررسی کرد چون شاید در کلاس‌های
    // فرزند شرط از جنس اگزیست داشته باشیم که
    // نه عملگر دارند و نه تارگت
    //public override bool HasValidTarget(/*bool acceptNullOrEmptyTarget*/)
    //{
    //    // 1399.11.28
    //    // بسته به سنجه انتخابی ممکن است 
    //    // مقدار نیاز نباشد برای تارگت
    //    // مثلا اگه سنجه از جنس اگزیست باشد
    //    //if (!acceptNullOrEmptyTarget && string.IsNullOrWhiteSpace(AggregateTargetValue))
    //    //{
    //    //    return false;
    //    //}

    //    //return Filter == null || Filter.HasValidTarget(acceptNullOrEmptyTarget);

    //    return !string.IsNullOrWhiteSpace(AggregateTargetValue);
    //}

    public override string ToString()
    {
        var filter = Filter == null ? string.Empty : $"با شرایط: [{Filter}]";

        var operation = Operation == 0 ? string.Empty : $"{Operation.GetDescription()} {Target}";

        return $" بازه زمانی {DurationType.GetDescription()} {filter} {operation} ";
    }

    public override List<string> GetSubConditions()
    {
        var conditions = new List<string>
        {
            Measure,
            GroupBy
        };
         
        if (Filter is not null)
        {
            conditions.AddRange(Filter.GetSubConditions());
        }

        return conditions.Distinct().ToList();
    }
}
