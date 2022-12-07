using GridEngineCore;
using GridEngineCore.Common.Entities;
using GridEngineCore.Conditions;
using GridEngineCore.Models;
using System;
using System.Linq;
using System.Linq.Expressions;

namespace GridEngine
{
    public class PropertyCondition<TEntity> : ILeafCondition where TEntity : class
    {
        public required string Entity { get; init; }

        public required string Property { get; init; }

        public CategoryType Category => CategoryType.Property;

        public FuncExp<TEntity, bool> Predicate { get; protected set; }

        public required Func<TEntity, object> ExtractValue { get; set; }

        public IQueryable<TEntity> Filter(IQueryable<TEntity> input)
        {
            return input.Where(Predicate.Expression);
        }

        public bool IsPassed(ConditionParameter<TEntity>? parameter)
        {
            if (parameter == null)
                return false;

            // 1399.12.25
            // کامپایل‌کردن اکسپرشن یک عملیات زمان‌بر است
            return this.Predicate.Func(parameter.Value) == true;
        }

        public string Serialize()
        {
            return System.Text.Json.JsonSerializer.Serialize(this);
        }

        public bool IsPassed(IConditionParameter parameter)
        {
            return this.IsPassed(parameter as ConditionParameter<TEntity>);
        }

        public static PropertyCondition<T> Create<T>(JsonConditionBase jsonCondition, IEntityResolver<T> resolver) where T : class
        {
            var condition = jsonCondition as JsonPropertyCondition;

            if (condition == null)
                throw new NotImplementedException("PropertyCondition<TEntity> => Create");

            return new PropertyCondition<T>()
            {
                Entity = condition.Entity,
                Property = condition.Property,
                Predicate = new FuncExp<T, bool>(resolver.GetPropertyFilter(condition.Target, condition.Property, condition.Operation.Value)),
                ExtractValue = resolver.ExtractPropertyValue(condition.Property)// FundFilters.ExtractPropertyValue(property)
            };
        }
    }
}
