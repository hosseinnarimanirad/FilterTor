namespace GridEngine;


using GridEngineCore;
using GridEngineCore.Expressions;
using GridEngineCore.Extensions;
using System.Linq.Expressions;

// COMPOSITE DESIGN PATTERN:
// Composite
public class CompoundCondition : ICondition
{
    public bool IsAndMode { get; set; }

    public List<ICondition> Conditions { get; private set; }

    public string Type => "Compound";

    public CategoryType Category => CategoryType.Compound;

    public CompoundCondition(List<ICondition> conditions, bool isAndMode)
    {
        Conditions = conditions;

        this.IsAndMode = isAndMode;
    }

    public bool IsPassed(IConditionParameter parameter)
    {
        if (parameter == null || Conditions == null || Conditions.Count == 0)
        {
            return false;
        }

        if (IsAndMode)
        {
            return Conditions.All(c => c.IsPassed(parameter));
        }
        else //Or case
        {
            return Conditions.Any(c => c.IsPassed(parameter));
        }
    }

    #region GetExpressions

    //public Expression<Func<TEntity, bool>>? GetExpression<TEntity, TCondition>() where TCondition : PropertyCondition<TEntity>
    public Expression<Func<TEntity, bool>>? GetExpression<TEntity>() where TEntity : class
    {
        var expressions = new List<Expression<Func<TEntity, bool>>>();

        foreach (var condition in Conditions)
        {
            if (condition is CompoundCondition cCondition)
            {
                var temp = cCondition.GetExpression<TEntity>();

                if (temp == null)
                    continue;

                expressions.Add(temp);
            }
            else if (condition is PropertyCondition<TEntity> pCondition)
            {
                expressions.Add(pCondition.Predicate.Expression);
            }

            // todo: add code to support other condition types

            //else if (condition is MeasureCondition<TEntity> mCondition)
            //{
            //    expressions.Add(mCondition.Predicate.Expression);
            //}
            //else if (condition is CollectionPropertyCondition<TEntity> cCondition)
            //{
            //    expressions.Add(cCondition.Predicate.Expression);
            //}
        }

        if (expressions.Count == 0)
        {
            return null;
        }
        else if (expressions.Count == 1)
        {
            return expressions.First();
        }
        else
        {
            Expression<Func<TEntity, bool>> result;

            var param = expressions[0].Parameters.First();

            if (IsAndMode)
            {
                result = ExpressionUtility.And(expressions);
            }
            else
            {
                result = ExpressionUtility.Or(expressions);
            }

            return Expression.Lambda<Func<TEntity, bool>>(result.Body, param);
        }
    }

    #endregion


    #region GetSubTypes

    public List<string> GetPropertyAndMeasureNames<TEntity>() where TEntity : class //where TCondition : PropertyCondition<TEntity>
    {
        List<string> result = new List<string>();

        foreach (var condition in Conditions)
        {
            if (condition is CompoundCondition cCondition)
            {
                var subTypeNames = cCondition.GetPropertyAndMeasureNames<TEntity>();

                if (subTypeNames.IsNullOrEmpty())
                    continue;

                result.AddRange(subTypeNames);
            }

            // todo: add code to support other condition types
            else if (condition is PropertyCondition<TEntity> pCondition)
            {
                result.Add(pCondition.Property);
            }
        }

        return result;
    }

    #endregion


}