namespace FilterTor;

using FilterTor.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using FilterTor.Conditions;
using FilterTor.Factory;
using FilterTor.Resolvers;

public abstract class ListCondition<TEntity> : ILeafCondition where TEntity : class
{
    public CategoryType Category => CategoryType.List;

    public required string Entity { get; init; }

    public DurationType? Duration { get; init; }

    protected readonly JsonListCondition _jsonListCondition;

    private readonly IEntityResolver<TEntity> _entityResolver;

    ////۱۳۹۸.۱۲.۱۴
    ////در این حالت هنگام استعلام به مشکل می‌خوریم!
    ////public Expression<Func<T, bool>> Predicate { get; set; }

    //public Func<T, bool> Predicate { get; set; }

    //public Func<IEnumerable<T>, object> ExtractValue { get; set; }

    public abstract bool IsPassed(ConditionParameter<TEntity>? parameter);


    public bool IsPassed(IConditionParameter parameter)
    {
        return this.IsPassed(parameter as ConditionParameter<TEntity>);
    }


    protected abstract JsonConditionBase GetDurationJsonCondition(string targetValue);

    protected abstract List<TEntity> GetFilteredList(ConditionParameter<TEntity> parameter, ICondition? filter);

    public abstract double CalculateMeasureValue(ConditionParameter<TEntity> parameter);

    /// <summary>
    /// Merge duration condition with Filter condition and returns the result
    /// </summary>
    /// <param name="value"></param>
    /// <returns></returns>
    private ICondition? GetFinalFilterCondition(DateTime? value)
    {
        var targetValue = FilterTor.Models.Duration.GetDurationString(this.Duration, value);

        JsonConditionBase? durationCondition = null;

        if (!string.IsNullOrWhiteSpace(targetValue))
        {
            durationCondition = GetDurationJsonCondition(targetValue);
        }

        return ConditionFactory.Merge<TEntity>(durationCondition, _jsonListCondition.Filter, _entityResolver);
    }

    public List<TEntity> ApplyFilterAndGetList(ConditionParameter<TEntity> parameter)
    {
        var condition = this.GetFinalFilterCondition(parameter.Date);

        return GetFilteredList(parameter, condition);
    }

}
