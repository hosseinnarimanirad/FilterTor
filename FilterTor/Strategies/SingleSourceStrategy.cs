namespace FilterTor.Strategies;

using FilterTor.Conditions;
using FilterTor.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class SingleSourceStrategy<TEntity> : FilterTorStrategy<TEntity>
{
    public SingleSourceStrategy()
    {

    }

    public override Task<List<TEntity>> Filter(JsonConditionBase? jsonCondition, List<SortModel>? sorts, PagingModel? paging)
    {
        throw new NotImplementedException();
    }
}
