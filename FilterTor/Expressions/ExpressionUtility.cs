using FilterTor.Helpers;
using FilterTor.Targets; 
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace FilterTor.Expressions
{
    public static class ExpressionUtility
    {
        public static Expression<T> Compose<T>(this Expression<T> first, Expression<T> second, Func<Expression, Expression, Expression> merge)
        {
            // build parameter map (from parameters of second to parameters of first)
            var map = first.Parameters.Select((f, i) => new { f, s = second.Parameters[i] }).ToDictionary(p => p.s, p => p.f);

            // replace parameters in the second lambda expression with parameters from the first
            var secondBody = ExpressionParameterRebinder.ReplaceParameters(map, second.Body);

            // apply composition of lambda expression bodies to parameters from the first expression 
            return Expression.Lambda<T>(merge(first.Body, secondBody), first.Parameters);
        }

        public static Expression<Func<T, bool>> And<T>(List<Expression<Func<T, bool>>> list)
        {
            var result = list[0];

            for (int i = 0; i < list.Count - 1; i++)
            {
                result = result.And(list[i + 1]);
            }

            return result;
        }

        public static Expression<Func<T, bool>> Or<T>(List<Expression<Func<T, bool>>> list)
        {
            var result = list[0];

            for (int i = 0; i < list.Count - 1; i++)
            {
                result = result.Or(list[i + 1]);
            }

            return result;
        }

        public static Expression<Func<T, bool>> And<T>(this Expression<Func<T, bool>> first, Expression<Func<T, bool>> second)
        {
            return first.Compose(second, Expression.And);
        }

        public static Expression<Func<T, bool>> Or<T>(this Expression<Func<T, bool>> first, Expression<Func<T, bool>> second)
        {
            return first.Compose(second, Expression.Or);
        }

        public static Expression<Func<T, bool>> Not<T>(this Expression<Func<T, bool>> predicate)
        {
            return Expression.Lambda<Func<T, bool>>(Expression.Not(predicate.Body), predicate.Parameters);
        }

        public static Expression<Func<TX, TY>> ComposeExp<TX, TY, TZ>(this Expression<Func<TZ, TY>> outer, Expression<Func<TX, TZ>> inner)
        {
            return Expression.Lambda<Func<TX, TY>>(
                ParameterReplacer.Replace(outer.Body, outer.Parameters[0], inner.Body),
                inner.Parameters[0]);
        }

        public static Expression<Func<T, bool>> Compare<T, TProp>(Expression<Func<T, TProp>> fieldExtractor, string value, Func<string, TProp> converter, Operation operation)
        {
            if (fieldExtractor == null || converter == null)
                return t => false;

            var param = fieldExtractor.Parameters.First();

            Expression expression = null;

            //Expression constantExpression = Expression.Constant(converter(value), typeof(TProp));

            switch (operation)
            {
                case Operation.EqualsTo:
                    expression = Expression.Equal(fieldExtractor.Body, Expression.Constant(converter(value), typeof(TProp)));
                    break;

                case Operation.StartsWith:
                case Operation.EndsWith:
                    return FilterStringProperty(fieldExtractor, converter(value), operation.GetName());

                case Operation.NotEqualTo:
                    expression = Expression.NotEqual(fieldExtractor.Body, Expression.Constant(converter(value), typeof(TProp)));
                    break;

                case Operation.GreaterThan:
                    expression = Expression.GreaterThan(fieldExtractor.Body, Expression.Constant(converter(value), typeof(TProp)));
                    break;

                case Operation.GreaterThanOrEqualTo:
                    expression = Expression.GreaterThanOrEqual(fieldExtractor.Body, Expression.Constant(converter(value), typeof(TProp)));
                    break;

                case Operation.LessThan:
                    expression = Expression.LessThan(fieldExtractor.Body, Expression.Constant(converter(value), typeof(TProp)));
                    break;

                case Operation.LessThanOrEqualTo:
                    expression = Expression.LessThanOrEqual(fieldExtractor.Body, Expression.Constant(converter(value), typeof(TProp)));
                    break;

                case Operation.Between:
                    var minMax = value.ToString().Split(",").ToList();

                    var e1 = Expression.GreaterThanOrEqual(fieldExtractor.Body, Expression.Constant(converter(minMax[0]), typeof(TProp)));

                    var e2 = Expression.LessThanOrEqual(fieldExtractor.Body, Expression.Constant(converter(minMax[1]), typeof(TProp)));

                    expression = Expression.And(e1, e2);

                    break;

                case Operation.In:

                    var values1 = value.Split(',').ToList();

                    var predicates1 = values1.Select(i =>
                        Expression.Lambda<Func<T, bool>>(
                            Expression.Equal(fieldExtractor.Body, Expression.Constant(converter(i), typeof(TProp))),
                            param))?.ToList(); //BatchProductFilters.GetFilter(i, condition.PropName, condition.OperationName))?.ToList();

                    return Or(predicates1);

                case Operation.NotIn:

                    var values2 = value.Split(',').ToList();

                    var predicates2 = values2.Select(i =>
                        Expression.Lambda<Func<T, bool>>(
                            Expression.NotEqual(fieldExtractor.Body, Expression.Constant(converter(i), typeof(TProp))),
                            param))?.ToList(); //BatchProductFilters.GetFilter(i, condition.PropName, condition.OperationName))?.ToList();

                    return And(predicates2);

                case Operation.Contains:
                    var array = converter(value) as List<object>;

                    var predicates3 = array.Select(i => Expression.Lambda<Func<T, bool>>(
                                          Expression.Call(fieldExtractor.Body,
                                          "Contains",
                                          null,
                                          Expression.Constant(i)), fieldExtractor.Parameters)).ToList();

                    return And(predicates3);

                // 1400.05.27
                // این کد کار کرد اما تبدیل به کوئری روی اس‌کیو‌ال نمی شد
                //case Operation.NotContains:
                //    var array2 = converter(value);

                //    var array3 = array2 as IEnumerable;

                //    List<Expression<Func<T, bool>>> predicates5 = new List<Expression<Func<T, bool>>>();

                //    foreach (var item in array3)
                //    {
                //        predicates5.Add(Expression.Lambda<Func<T, bool>>(
                //                          Expression.Call(fieldExtractor.Body,
                //                          "Contains",
                //                          null,
                //                          Expression.Constant(item)), fieldExtractor.Parameters));
                //    }

                //    return ExpressionUtility.Not(ExpressionUtility.And(predicates5));

                default:
                    throw new NotImplementedException("ExpressionUtility -> Compare");
            }

            return Expression.Lambda<Func<T, bool>>(expression, param);
        }

        public static Expression<Func<T, bool>> Compare<T, TProp>(Expression<Func<T, TProp>> fieldExtractor, JsonTargetBase target, Func<string, TProp> converter, Operation operation)
        {
            var param = fieldExtractor.Parameters.First();

            Expression expression = null;

            Expression? targetExpression = null;

            switch (target)
            {
                case JsonConstantTarget jsonConstantTarget:
                    targetExpression = Expression.Constant(converter(jsonConstantTarget.Value), typeof(TProp));
                    break;

                default:
                    break;
            }

            switch (operation)
            {
                case Operation.EqualsTo:
                    expression = Expression.Equal(fieldExtractor.Body, targetExpression);
                    break;

                case Operation.StartsWith:
                case Operation.EndsWith:
                    expression = Expression.Call(fieldExtractor.Body, operation.GetName(), null, targetExpression);
                    break;

                case Operation.NotEqualTo:
                    expression = Expression.NotEqual(fieldExtractor.Body, targetExpression);
                    break;

                case Operation.GreaterThan:
                    expression = Expression.GreaterThan(fieldExtractor.Body, targetExpression);
                    break;

                case Operation.GreaterThanOrEqualTo:
                    expression = Expression.GreaterThanOrEqual(fieldExtractor.Body, targetExpression);
                    break;

                case Operation.LessThan:
                    expression = Expression.LessThan(fieldExtractor.Body, targetExpression);
                    break;

                case Operation.LessThanOrEqualTo:
                    expression = Expression.LessThanOrEqual(fieldExtractor.Body, targetExpression);
                    break;

                case Operation.Between:
                    var minValue = (target as JsonRangeTarget).MinValue;
                    var maxValue = (target as JsonRangeTarget).MaxValue;

                    var e1 = Expression.GreaterThanOrEqual(fieldExtractor.Body, Expression.Constant(converter(minValue), typeof(TProp)));

                    var e2 = Expression.LessThanOrEqual(fieldExtractor.Body, Expression.Constant(converter(maxValue), typeof(TProp)));

                    expression = Expression.And(e1, e2);

                    break;

                case Operation.In:
                    var predicates1 = (target as JsonArrayTarget)?.Values.Select(i =>
                        Expression.Lambda<Func<T, bool>>(
                            Expression.Equal(fieldExtractor.Body, Expression.Constant(converter(i), typeof(TProp))),
                            param))?.ToList();

                    return Or(predicates1);

                case Operation.NotIn:
                    var predicates2 = (target as JsonArrayTarget)?.Values.Select(i =>
                        Expression.Lambda<Func<T, bool>>(
                            Expression.NotEqual(fieldExtractor.Body, Expression.Constant(converter(i), typeof(TProp))),
                            param))?.ToList();

                    return And(predicates2);

                case Operation.Contains:
                    var predicates3 = (target as JsonArrayTarget)?.Values.Select(i => Expression.Lambda<Func<T, bool>>(
                                          Expression.Call(fieldExtractor.Body,
                                          "Contains",
                                          null,
                                          Expression.Constant(i)), fieldExtractor.Parameters)).ToList();

                    return And(predicates3);

                default:
                    throw new NotImplementedException("ExpressionUtility -> Compare");
            }

            return Expression.Lambda<Func<T, bool>>(expression, param);
        }



        public static Expression<Func<T, bool>> FilterStringProperty<T, TProp>(Expression<Func<T, TProp>> expression, TProp filter, string method)
        {
            return Expression.Lambda<Func<T, bool>>(
                Expression.Call(expression.Body,
                    method,
                    null,
                    Expression.Constant(filter)),
                expression.Parameters);
        }


        public static Func<T, bool> Compare<T, TProp>(Func<T, TProp> fieldExtractor, string value, Func<string, TProp> converter, Operation operation) where TProp : IComparable
        {
            if (fieldExtractor == null || converter == null)
                return t => false;

            //100.CompareTo(1) -- 1
            //1.CompareTo(100) -- -1

            switch (operation)
            {
                case Operation.EqualsTo:
                    return t => fieldExtractor(t)?.CompareTo(converter(value)) == 0;

                case Operation.Contains:
                    return t => fieldExtractor(t)?.ToString().Contains(converter(value)?.ToString()) == true;

                case Operation.StartsWith:
                    return t => fieldExtractor(t)?.ToString().StartsWith(converter(value)?.ToString()) == true;

                case Operation.EndsWith:
                    return t => fieldExtractor(t)?.ToString().EndsWith(converter(value)?.ToString()) == true;

                case Operation.NotEqualTo:
                    return t => fieldExtractor(t)?.CompareTo(converter(value)) != 0;

                case Operation.GreaterThan:
                    return t => fieldExtractor(t)?.CompareTo(converter(value)) > 0;

                case Operation.GreaterThanOrEqualTo:
                    return t => fieldExtractor(t)?.CompareTo(converter(value)) >= 0;

                case Operation.LessThan:
                    return t => fieldExtractor(t)?.CompareTo(converter(value)) < 0;

                case Operation.LessThanOrEqualTo:
                    return t => fieldExtractor(t)?.CompareTo(converter(value)) <= 0;

                case Operation.Between:
                    var minMax = value.ToString().Split(",").ToList();

                    return t => fieldExtractor(t)?.CompareTo(converter(minMax[0])) >= 0 &&
                                fieldExtractor(t)?.CompareTo(converter(minMax[1])) <= 0;


                case Operation.In:
                    var values1 = value.Split(',')?.ToList();

                    return t => values1?.Any(i => fieldExtractor(t).CompareTo(converter(i)) == 0) == true;

                case Operation.NotIn:
                    var values2 = value.Split(',')?.ToList();

                    return t => values2?.Any(i => fieldExtractor(t).CompareTo(converter(i)) == 0) != true;

                default:
                    throw new NotImplementedException("ExpressionUtility -> Compare");
            }
        }
    }

}
