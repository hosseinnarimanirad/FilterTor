namespace GridEngineCore.Decorators;

using GridEngineCore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

public interface ISortResolver<T>
{
    Expression<Func<T, object>> Extract(SortModel model);
}
