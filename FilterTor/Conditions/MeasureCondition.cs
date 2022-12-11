using FilterTor.Helpers;
using System;
using System.Collections.Generic;
using FilterTor.Conditions;
using FilterTor.Common.Entities;
using FilterTor.Factory;

namespace FilterTor;

public abstract class MeasureCondition<TEntity> : ILeafCondition where TEntity : class
{
    public CategoryType Category => CategoryType.Measure;

    public required string Entity { get; init; }
     
    public DurationType? Duration { get; set; }

    protected readonly JsonMeasureCondition _jsonMeasureCondition;

    private readonly IEntityResolver<TEntity> _entityResolver;

    public bool IsPassed(IConditionParameter parameter)
    {
        return this.IsPassed(parameter as ConditionParameter<TEntity>);
    }

    public abstract bool IsPassed(ConditionParameter<TEntity>? parameter);

    protected abstract JsonConditionBase GetDurationJsonCondition(string targetValue);

    protected abstract List<TEntity> GetFilteredList(ConditionParameter<TEntity> parameter, ICondition? filter);

    public abstract double CalculateMeasureValue(ConditionParameter<TEntity> parameter);

    private ICondition? GetFinalFilterCondition(DateTime? value)
    {
        var targetValue = FilterTor.Models.Duration.GetDurationString(this.Duration, value);

        JsonConditionBase? durationCondition = null;

        if (!string.IsNullOrWhiteSpace(targetValue))
        {
            durationCondition = GetDurationJsonCondition(targetValue);
        }

        return ConditionFactory.Merge<TEntity>(durationCondition, _jsonMeasureCondition.Filter, _entityResolver);
    }

    public List<TEntity> ApplyFilterAndGetList(ConditionParameter<TEntity> parameter)
    {
        var condition = this.GetFinalFilterCondition(parameter.Date);

        return GetFilteredList(parameter, condition);
    }
     
    ////1399.04.16
    ////مثلا وقتی فروش خالص گرفته می‌شه بعدش هدف فروش هم
    ////باید گرفته بشه و بعد نسبت این ها به عنوان مقدار
    ////نهایی این بخش وارد پله‌های نتیجه شرط شود.
    //public Func<ConditionParameter<TEntity>, double> ExtractSeconaryValue { get; set; }
      
  
}
