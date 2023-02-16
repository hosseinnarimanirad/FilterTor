using FilterTor.Helpers;
using FilterTor.Targets;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
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
            return first.Compose(second, Expression.AndAlso);
        }

        public static Expression<Func<T, bool>> Or<T>(this Expression<Func<T, bool>> first, Expression<Func<T, bool>> second)
        {
            return first.Compose(second, Expression.Or);
        }

        public static Expression<Func<T, bool>> Not<T>(this Expression<Func<T, bool>> predicate)
        {
            return Expression.Lambda<Func<T, bool>>(Expression.Not(predicate.Body), predicate.Parameters);
        }

        //public static Expression<Func<T, bool>> In<T, TProp>(this Expression<Func<T, bool>> predicate, IEnumerable<TProp> values)
        //{
        //    //return values.Select(i =>
        //    //           Expression.Lambda<Func<T, bool>>(
        //    //               Expression.Equal(fieldExtractor.Body, Expression.Constant(converter(i), typeof(TProp))),
        //    //               param))?.ToList();


        //    return Expression.Call(typeof(Enumerable), "Contains", null, targetExpression);
        //}

        public static Expression In<T, TProp>(this Expression<Func<T, TProp>> predicate, IEnumerable<TProp> values)
        {
            // try #1
            //var right = Expression.Constant(values, typeof(IEnumerable));

            //var p = (PropertyInfo)((MemberExpression)predicate.Body).Member;
            //var pe = Expression.Parameter(typeof(T), "p");
            //var left = Expression.Property(pe, p);
            //var call = Expression.Call(typeof(Enumerable), "Contains", new[] { p.PropertyType }, right, left);
            //return Expression.Lambda<Func<T, bool>>(call, pe);

            var right = Expression.Constant(values, typeof(IEnumerable<TProp>));

            var p = (PropertyInfo)((MemberExpression)predicate.Body).Member;
            var param = predicate.Parameters.First();
            var left = Expression.Property(param, p);
            return Expression.Call(typeof(Enumerable), "Contains", new Type[] { typeof(TProp) }, right, left);

            // try #2
            //var predicates3 = values.Select(i => Expression.Lambda<Func<T, bool>>(
            //                             Expression.Call(
            //                                predicate.Body,
            //                                "Contains",
            //                                null,
            //                                Expression.Constant(i)), 
            //                                predicate.Parameters)).ToList();

            // try #3
            //return Expression.Call(Expression.Constant(values, typeof(IEnumerable)), "Contains", null, predicate.Body);
        }

        public static Expression<Func<TX, TY>> ComposeExp<TX, TY, TZ>(this Expression<Func<TZ, TY>> outer, Expression<Func<TX, TZ>> inner)
        {
            return Expression.Lambda<Func<TX, TY>>(
                ParameterReplacer.Replace(outer.Body, outer.Parameters[0], inner.Body),
                inner.Parameters[0]);
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

                case JsonArrayTarget jsonArrayTarget:
                case JsonCollectionPropertyTarget jsonCollectionPropertyTarget:
                case JsonMeasureTarget jsonMeasureTarget:
                case JsonPropertyTarget jsonPropertyTarget:
                case JsonRangeTarget jsonRangeTarget:
                default:
                    break;
            }

            switch (operation)
            {
                case Operation.EqualsTo:
                    expression = Expression.Equal(fieldExtractor.Body, targetExpression!);
                    break;

                case Operation.StartsWith:
                case Operation.EndsWith:
                    expression = Expression.Call(fieldExtractor.Body, operation.GetName(), null, targetExpression!);
                    break;

                case Operation.NotEqualTo:
                    expression = Expression.NotEqual(fieldExtractor.Body, targetExpression!);
                    break;

                case Operation.GreaterThan:
                    expression = Expression.GreaterThan(fieldExtractor.Body, targetExpression!);
                    break;

                case Operation.GreaterThanOrEqualTo:
                    expression = Expression.GreaterThanOrEqual(fieldExtractor.Body, targetExpression!);
                    break;

                case Operation.LessThan:
                    expression = Expression.LessThan(fieldExtractor.Body, targetExpression!);
                    break;

                case Operation.LessThanOrEqualTo:
                    expression = Expression.LessThanOrEqual(fieldExtractor.Body, targetExpression!);
                    break;

                case Operation.Between:
                    var minValue = (target as JsonRangeTarget)!.MinValue;
                    var maxValue = (target as JsonRangeTarget)!.MaxValue;

                    var e1 = Expression.GreaterThanOrEqual(fieldExtractor.Body, Expression.Constant(converter(minValue), typeof(TProp)));

                    var e2 = Expression.LessThanOrEqual(fieldExtractor.Body, Expression.Constant(converter(maxValue), typeof(TProp)));

                    expression = Expression.AndAlso(e1, e2);

                    break;

                case Operation.In:
                    //var predicates1 = (target as JsonArrayTarget)?.Values.Select(i =>
                    //    Expression.Lambda<Func<T, bool>>(
                    //        Expression.Equal(fieldExtractor.Body, Expression.Constant(converter(i), typeof(TProp))),
                    //        param))?.ToList();

                    //return Or(predicates1);
                    expression = In(fieldExtractor, (target as JsonArrayTarget)!.Values.Select(i => converter(i)));
                    break;

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


        public static Expression<Func<T, bool>> Compare<T, TProp>(Expression<Func<T, IEnumerable<TProp>>> fieldExtractor, JsonTargetBase target, Func<string, TProp> converter, Operation operation)
        {
            var param = fieldExtractor.Parameters.First();

            Expression? expression = null;

            Expression? targetExpression = null;

            switch (target)
            {
                case JsonConstantTarget jsonConstantTarget:
                    targetExpression = Expression.Constant(converter(jsonConstantTarget.Value), typeof(TProp));
                    break;

                case JsonArrayTarget jsonArrayTarget:

                case JsonCollectionPropertyTarget jsonCollectionPropertyTarget:

                default:
                    throw new NotImplementedException("ExpressionUtility > Compare");
            }

            switch (operation)
            {
                //case Operation.Between:
                //case Operation.In:
                //case Operation.NotIn:
                //case Operation.Contains:

                case Operation.IncludeAll:

                    break;
                case Operation.IncludeAny:
                case Operation.ExcludeAll:
                case Operation.ExcludeAny:
                    break;

                default:
                    throw new NotImplementedException("ExpressionUtility -> Compare");
            }

            return Expression.Lambda<Func<T, bool>>(expression, param);



            //var customerParam = Expression.Parameter(typeof(Customer), "c");
            //var groupsParam = Expression.Parameter(typeof(List<CustomerGroup>), "groups");
            //var groupParam = Expression.Parameter(typeof(CustomerGroup), "g");
            //var anyCall = Expression.Call(
            //    Expression.Property(customerParam, "CustomerGroups"),
            //    typeof(Enumerable).GetMethod("Any"),
            //    Expression.Lambda<Func<CustomerGroup, bool>>(
            //        Expression.Call(
            //            Expression.Property(groupsParam, "Contains"),
            //            groupParam
            //        ),
            //        groupParam
            //    )
            //);
            //var expression = Expression.Lambda<Func<List<CustomerGroup>, Customer, bool>>(
            //    anyCall,
            //    groupsParam,
            //    customerParam
            //);
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
