namespace FilterTor.Decorators;

using FilterTor.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class PagingDecorator<T> : IQueryGenerator<T>
{
    private readonly PagingModel _paging;

    private readonly IQueryGenerator<T> _queryGenerator;

    public PagingDecorator(IQueryGenerator<T> queryGenerator, PagingModel paging)
    {
        _paging = paging;

        _queryGenerator = queryGenerator;
    }

    public IQueryable<T> Query()
    {
        return _queryGenerator.Query().Skip(_paging.PageSize * _paging.Page).Take(_paging.PageSize);
    }
}