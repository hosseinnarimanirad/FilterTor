using GridEngineCore;
using GridEngineCore.Helpers;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using GridEngineCore.Conditions;
using GridEngineCore.Common.Entities;

namespace GridEngine.Factory
{
    public static class ConditionFactory
    {
        public static ICondition? Create<T>(string conditionJsonString, IEntityResolver<T> resolver) where T : class
        {
            if (string.IsNullOrWhiteSpace(conditionJsonString))
            {
                return null;
            }

            return Create<T>(JsonConditionBase.Deserialize(conditionJsonString, EngineCoreHelper.DefaultJsonSerializerOptions), resolver);
        }

        public static ICondition? Create<T>(JsonConditionBase? jsonCondition, IEntityResolver<T> resolver) where T : class
        {
            if (jsonCondition is null)
                return null;

            try
            {
                switch (jsonCondition.Category)
                {
                    case CategoryType.Compound:
                        return CreateCompoundCondition<T>(jsonCondition, resolver);

                    case CategoryType.Property:
                        return PropertyCondition<T>.Create(jsonCondition, resolver);

                    // todo: support other conditions
                    case CategoryType.CollectionProperty:
                    case CategoryType.Measure:
                    case CategoryType.List:
                        throw new NotImplementedException("ConditionFactory -> Create (NOT IMPLEMENTED)");
                    default:
                        throw new NotImplementedException("ConditionFactory -> Create");
                }
            }
            catch (Exception ex)
            {
                //Debug.WriteLine($"EXCEPTION-{ex.GetFullMessage()}");

                return null;
            }
        }

        public static ICondition? Merge<T>(JsonConditionBase? first, JsonConditionBase? second, IEntityResolver<T> resolver) where T : class
        {
            return ConditionFactory.Create<T>(EngineCoreHelper.Merge(first, second), resolver);
        }

        private static ICondition? CreateCompoundCondition<T>(JsonConditionBase jsonCondition, IEntityResolver<T> resolver) where T : class
        {
            var condition = jsonCondition as JsonCompoundCondition;

            if (condition == null)
            {
                throw new NotImplementedException();
            }

            if (condition.Conditions.Count == 1)
            {
                return Create<T>(condition.Conditions.First(), resolver);
            }

            //1399.02.16
            var compoundCondition = new CompoundCondition(condition.Conditions.Select(e => Create<T>(e, resolver))?.ToList(), condition.IsAndMode);

            //1399.02.16
            if (compoundCondition.Conditions.Any(c => c == null))
            {
                return null;
            }

            return compoundCondition;

            //1399.02.16
            //return new CompoundCondition(condition.Conditions.Select(c => Create(c))?.ToList(), condition.IsAndMode);// Create(condition.LeftCondition),
            //Create(condition.RightCondition),
            //condition.IsAndMode);
        }

        public static ICondition CreateCompoundCondition<T>(IEntityResolver<T> resolver, params string[] jsonConditions) where T : class
        {
            if (jsonConditions == null)
            {
                return null;
            }

            var conditions = jsonConditions.Select(e => Create<T>(e, resolver))?.ToList();

            return new CompoundCondition(conditions, true);
        }


        public static bool IsValidOrNull<T>(JsonConditionBase condition, IEntityResolver<T> resolver) where T : class
        {
            return condition == null || (ConditionFactory.Create<T>(condition, resolver) != null && condition.Validate());
        }

        public static bool IsNotValidOrNull<T>(JsonConditionBase condition, IEntityResolver<T> resolver) where T : class
        {
            return condition == null || ConditionFactory.Create<T>(condition, resolver) == null || !condition.Validate();
        }


        public static Expression<Func<TEntity, bool>>? GetExpression<TEntity>(ICondition condition) where TEntity : class
        {
            if (condition is null)
                return null;

            Expression<Func<TEntity, bool>>? whereClause = null;

            if (condition is CompoundCondition compoundCondition)
            {
                whereClause = compoundCondition.GetExpression<TEntity>();
            }
            else if (condition is PropertyCondition<TEntity> fundCondition)
            {
                whereClause = fundCondition.Predicate.Expression;
            }
            else
            {
                return null;
            }

            return whereClause;
        }

    }
}