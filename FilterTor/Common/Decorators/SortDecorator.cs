namespace FilterTor.Decorators;

using FilterTor.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.InteropServices.ObjectiveC;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;

public class SortDecorator<T> : IQueryGenerator<T>
{
    private readonly List<SortModel> _sorts;

    private readonly ISortResolver<T> _sortResolver;

    private readonly IQueryGenerator<T> _queryGenerator;

    public SortDecorator(IQueryGenerator<T> queryGenerator, List<SortModel> sorts, ISortResolver<T> sortResolver)
    {
        this._sorts = sorts;

        this._sortResolver = sortResolver;

        this._queryGenerator = queryGenerator;
    }

    public IQueryable<T> Query(IQueryable<T> list)
    {
        IQueryable<T> iqueryable = _queryGenerator.Query(list);

        for (int i = 0; i < _sorts.Count; i++)
        {
            Expression<Func<T, object>> lambda = _sortResolver.Extract(_sorts[i]);

            var command = _sorts[i].SortDirection == System.ComponentModel.ListSortDirection.Descending
                                ? (i == 0 ? "OrderByDescending" : "ThenByDescending")
                                : (i == 0 ? "OrderBy" : "ThenBy");

            var resultExpression = Expression.Call(
                typeof(Queryable),
                command,
                new Type[] { typeof(T), lambda.ReturnType },
                iqueryable.Expression,
                Expression.Quote(lambda));

            iqueryable = iqueryable.Provider.CreateQuery<T>(resultExpression);
        }

        return iqueryable;
    }
}
