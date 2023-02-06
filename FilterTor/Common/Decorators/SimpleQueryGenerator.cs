using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FilterTor.Decorators;

public class SimpleQueryGenerator<T> : IQueryGenerator<T>
{
    private readonly IQueryable<T> _query;
     
    public SimpleQueryGenerator(IQueryable<T> query)
    {
        _query = query; 
    }

    public IQueryable<T> Query()
    {
        return _query;
    } 
}
